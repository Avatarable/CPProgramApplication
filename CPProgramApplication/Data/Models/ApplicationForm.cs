using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CPProgramApplication.Data.Models
{
    public class ApplicationForm
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string ProgramId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IDNUmber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public List<CustomQuestionAnswer> QuestionAnswers { get; set; }
    }
}
