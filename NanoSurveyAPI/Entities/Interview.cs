namespace NanoSurveyAPI.Entities
{
    public class Interview
    {
        public int InterviewId { get; set; }

        public DateTime InterviewDate { get; set; }

        public bool IsFinished { get; set; }    

        public int SurveyId { get; set; }

        public Survey Survey { get; set; } = null!;

        public List<Result> Results { get; set; } = new List<Result>();
    }
}
