using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NanoSurveyAPI.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }

        [MaxLength(500)]
        public string AnswerText { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; } = null!;

        public List<Result> Results { get; set; } = new List<Result>();
    }
}
