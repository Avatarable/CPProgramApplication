using Newtonsoft.Json;

namespace CPProgramApplication.Data.Models
{
    public class ProgramModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CustomQuestion> Questions { get; set; }
    }
}
