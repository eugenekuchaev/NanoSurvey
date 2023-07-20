namespace NanoSurveyAPI.Entities
{
    public class Result
    {
        public int ResultId { get; set; }

        public int SurveyId { get; set; }   

        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public int InterviewId { get; set; }

        public Survey Survey { get; set; } = null!;

        public Question Question { get; set; } = null!;

        public Answer Answer { get; set; } = null!;

        public Interview Interview { get; set; } = null!;
    }
}
