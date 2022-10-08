using System;
using enMessage.Model;
using enMessage.Shared.ViewModels;

namespace enMessage.Shared.Mappers
{
    public static class RequestMapper
    {
        public static Request GetRequest(User user)
        {
            return new Request()
            { 
                RequestedFromID = user.ID,
                RequestedFromUsername = user.Username
            };
        }

        public static RequestViewModel GetRequestViewModel(Request r)
        {
            return new RequestViewModel()
            {
                ID = r.ID,
                RequestedFrom = r.RequestedFromUsername
            };
        }
    }
}
