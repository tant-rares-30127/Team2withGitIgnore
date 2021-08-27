
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Team2Application.Services
{
    public class LibraryResourceBroadcastService : ILibraryResourceBroadcastService
    {
        private readonly IHubContext<MessageHub> messageHub;

        public LibraryResourceBroadcastService(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
        }
        public void LibraryResourceAdded(int id, string name, string recommandation, string url)
        {
            messageHub.Clients.All.SendAsync("LibraryResourceAdded", id, name, recommandation, url);
        }

        public void LibraryResourceDeleted(int id)
        {
            messageHub.Clients.All.SendAsync("LibraryResourceDeleted", id);
        }

        public void LibraryResourceUpdated(int id, string name, string recommandation, string url)
        {
            messageHub.Clients.All.SendAsync("LibraryResourceUpdated", id, name, recommandation, url);
        }
    }
}