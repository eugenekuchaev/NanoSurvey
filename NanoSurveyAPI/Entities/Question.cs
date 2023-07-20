using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NanoSurveyAPI.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }

        [MaxLength(500)]
        public string QuestionText { get; set; } = null!;

        public int SurveyId { get; set; }

        public Survey Survey { get; set; } = null!;

        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
