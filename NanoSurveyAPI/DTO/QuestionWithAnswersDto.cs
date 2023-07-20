namespace NanoSurveyAPI.DTO
{
    public class QuestionWithAnswersDto
    {
        public string QuestionText { get; set; } = null!;
        public List<string> Answers { get; set; } = null!;
    }
}
