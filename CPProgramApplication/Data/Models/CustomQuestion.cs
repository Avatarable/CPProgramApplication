using CPProgramApplication.Data.Enums;

namespace CPProgramApplication.Data.Models
{
    public class CustomQuestion
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
    }
}
