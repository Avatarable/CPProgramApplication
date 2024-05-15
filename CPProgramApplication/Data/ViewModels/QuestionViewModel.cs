using CPProgramApplication.Data.Enums;

namespace CPProgramApplication.Data.ViewModels
{
    public class QuestionViewModel
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<string>? Choices { get; set; }
        public int? MaxChoicesAllowed { get; set; }
        public bool? EnableOtherOption { get; set; }
    }
}
