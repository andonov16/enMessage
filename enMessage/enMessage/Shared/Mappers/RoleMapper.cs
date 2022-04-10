using enMessage.Model;
using enMessage.Shared.ViewModels;

namespace enMessage.Shared.Mappers
{
    public static class RoleMapper
    {
        public static Role GetRole(User holder, Chat chatRoom, string roleINChat)
        {
            return new Role()
            {
                Holder = holder,
                ChatRoom = chatRoom,
                RoleInChat = roleINChat
            };
        }

        public static RoleViewModel GetRoleViewModel(Role item)
        {
            return new RoleViewModel()
            {
                ID = item.ID,
                Holder = UserMapper.GetAsFriend(item.Holder),
                ChatRoom = ChatMapper.GetSimpleView(item.ChatRoom),
                RoleInChat = item.RoleInChat
            };
        }
    }
}
