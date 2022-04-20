using enMessage.Model;
using enMessage.Shared.ViewModels;

namespace enMessage.Shared.Mappers
{
    public static class ChatMapper
    {
        public static Chat GetChat(string chatName, DateTime lastInteraction)
        {
            return new Chat()
            {
                ChatName = chatName,
                LastInteraction = lastInteraction,
                Users = new List<User>(),
                Messages = new List<Message>(),
                Roles = new List<Role>()
            };
        }

        //manually check for null valls?
        public static ChatViewModel GetChatViewModel(Chat item)
        {
            return new ChatViewModel()
            {
                ID = item.ID,
                ChatName = item.ChatName,
                LastInteraction = item.LastInteraction,
                Users = item.Users.Select(u => UserMapper.GetAsFriend(u)).ToList(),
                Messages = item.Messages.Select(m => MessageMapper.GetMessageViewModel(m)).ToList(),
                //Roles = item.Roles.Select(r => RoleMapper.GetRoleViewModel(r)).ToList()
            };
        }

        public static ChatViewModel GetSimpleView(Chat item)
        {
            return new ChatViewModel()
            {
                ID = item.ID,
                ChatName = item.ChatName,
                LastInteraction = item.LastInteraction
            };
        }
    }
}
