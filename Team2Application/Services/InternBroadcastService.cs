
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Team2Application.Services
{
    public class InternBroadcastService : IInternBroadcastService
    {
        private readonly IHubContext<MessageHub> messageHub;

        public InternBroadcastService(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
        }
        public void InternAdded(int id, string name, DateTime birthDate, string emailAddress)
        {
            messageHub.Clients.All.SendAsync("InternAdded", id, name, birthDate, emailAddress);
        }

        public void InternDeleted(int id)
        {
            messageHub.Clients.All.SendAsync("InternDeleted", id);
        }

        public void InternUpdated(int id, string name, DateTime birthDate, string emailAddress)
        {
            messageHub.Clients.All.SendAsync("InternUpdated", id, name, birthDate, emailAddress);
        }
    }
}