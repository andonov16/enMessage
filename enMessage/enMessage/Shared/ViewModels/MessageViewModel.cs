namespace enMessage.Shared.ViewModels
{
    public class MessageViewModel:BaseViewModel
    {
        public ChatViewModel SentIn { get; set; }
        public UserViewModel SentBy { get; set; }
        public DateTime SentOn { get; set; }
        public string DataType { get; set; }
        public string Content { get; set; }
    }
}
