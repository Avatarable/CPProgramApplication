using CPProgramApplication.Data.Enums;
using Newtonsoft.Json;

namespace CPProgramApplication.Data.Models
{
    public class CustomQuestion
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<string> Choices { get; set; }
        public int MaxChoicesAllowed { get; set; }
        public bool EnableOtherOption { get; set; }
    }
}
