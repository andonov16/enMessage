namespace enMessage.Shared.ViewModels
{
    public class ChatViewModel:BaseViewModel
    {
        public string ChatName { get; set; }
        public DateTime LastInteraction { get; set; }
        public virtual ICollection<UserViewModel> Users { get; set; }
        public virtual ICollection<MessageViewModel> Messages { get; set; }
        public virtual ICollection<RoleViewModel> Roles { get; set; }
    }
}
