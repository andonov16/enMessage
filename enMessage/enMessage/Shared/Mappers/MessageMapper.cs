using enMessage.Model;
using enMessage.Shared.ViewModels;

namespace enMessage.Shared.Mappers
{
    public static class MessageMapper
    {
        public static Message GetMessage(Chat sentIn, User sentBy, DateTime sentOn, string dataType, string content)
        {
            return new Message()
            {
                SentIn = sentIn,
                SentBy = sentBy,
                SentOn = sentOn,
                DataType = dataType,
                Content = content
            };
        }

        public static MessageViewModel GetMessageViewModel(Message item)
        {
            return new MessageViewModel()
            {
                ID = item.ID,
                SentIn = ChatMapper.GetChatViewModel(item.SentIn),
                SentBy = UserMapper.GetAsFriend(item.SentBy),
                SentOn = item.SentOn,
                DataType = item.DataType,
                Content = item.Content
            };
        }
    }
}
