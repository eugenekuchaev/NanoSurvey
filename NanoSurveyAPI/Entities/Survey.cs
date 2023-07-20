using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NanoSurveyAPI.Entities
{
    public class Survey
    {
        public int SurveyId { get; set; }

        [MaxLength(250)]
        public string SurveyTitle { get; set; } = null!;

        [MaxLength(1000)]
        public string? SurveyDescription { get; set; }

        public bool IsActive { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();

        public List<Interview> Interviews { get; set; } = new List<Interview>();
    }
}
