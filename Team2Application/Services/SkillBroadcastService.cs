
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Team2Application.Services
{
    public class SkillBroadcastService : ISkillBroadcastService
    {
        private readonly IHubContext<MessageHub> messageHub;

        public SkillBroadcastService(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
        }
        public void SkillAdded(int id, string name, string skillMatrixUrl, string description)
        {
            messageHub.Clients.All.SendAsync("SkillAdded", id, name, skillMatrixUrl, description);
        }

        public void SkillDeleted(int id)
        {
            messageHub.Clients.All.SendAsync("SkillDeleted", id);
        }

        public void SkillUpdated(int id, string name, string skillMatrixUrl, string description)
        {
            messageHub.Clients.All.SendAsync("SkillUpdated", id, name, skillMatrixUrl, description);
        }
    }
}