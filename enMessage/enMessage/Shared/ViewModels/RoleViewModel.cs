namespace enMessage.Shared.ViewModels
{
    public class RoleViewModel:BaseViewModel
    {
        public UserViewModel Holder { get; set; }
        public ChatViewModel ChatRoom { get; set; }
        public string RoleInChat { get; set; }
    }
}
