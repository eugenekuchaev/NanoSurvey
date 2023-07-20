using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NanoSurveyAPI.Data;
using NanoSurveyAPI.DTO;
using NanoSurveyAPI.Entities;

namespace NanoSurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly DataContext _context;

        public SurveyController(DataContext dataContext)    
        {
            _context = dataContext;
        }

        [HttpGet("surveys/{surveyId}/questions/{questionId}")]
        public async Task<ActionResult<QuestionWithAnswersDto>> GetQuestion(int surveyId, int questionId)
        {
            var question = await _context.Questions
                .Include(a => a.Answers)
                .Where(s => s.SurveyId == surveyId && s.QuestionId == questionId)
                .FirstOrDefaultAsync();

            if (question == null)
            {
                return NotFound("There's no question with this ID");
            }

            return Ok(new QuestionWithAnswersDto
            {
                QuestionText = question.QuestionText,
                Answers = question.Answers.Select(a => a.AnswerText).ToList()
            });
        }

        [HttpPost("save-result")]
        public async Task<ActionResult<SaveResultResponseDto>> SaveResult(ResultDto resultDto)
        {
            var interview = await GetOrCreateInterview(resultDto);

            var result = new Result
            {
                SurveyId = resultDto.SurveyId,
                QuestionId = resultDto.QuestionId,
                AnswerId = resultDto.AnswerId,
                InterviewId = interview.InterviewId
            };

            _context.Results.Add(result);

            if (!await HasNextQuestion(resultDto))
            {
                interview.IsFinished = true;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            await _context.SaveChangesAsync();

            return new SaveResultResponseDto
            {
                NextQuestionId = resultDto.QuestionId + 1,
                InterviewId = interview.InterviewId
            };
        }

        private async Task<Interview> GetOrCreateInterview(ResultDto resultDto)
        {
            var interview = await _context.Interviews
                .FirstOrDefaultAsync(i => i.InterviewId == resultDto.InterviewId);

            if (interview is null)
            {
                interview = new Interview
                {
                    InterviewDate = DateTime.UtcNow,
                    SurveyId = resultDto.SurveyId
                };

                _context.Interviews.Add(interview);
                await _context.SaveChangesAsync();

                return interview;
            }

            return interview;
        }

        private async Task<bool> HasNextQuestion(ResultDto resultDto)
        {
            return await _context.Questions
                .Where(q => q.SurveyId == resultDto.SurveyId)
                .AnyAsync(x => x.QuestionId == resultDto.QuestionId + 1);
        }
    }
}