using enMessage.Model;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
using Newtonsoft.Json;
using System.Security.Cryptography;

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
                SentIn = ChatMapper.GetSimpleView(item.SentIn),
                SentBy = UserMapper.GetAsFriend(item.SentBy),
                SentOn = item.SentOn,
                DataType = item.DataType,
                Content = MessageUtil.DecryptMessage(
                    JsonConvert.DeserializeObject<RSAParameters>(item.SentBy.PrivateKey),
                    Convert.FromBase64String(item.Content))
            };
        }
    }
}
