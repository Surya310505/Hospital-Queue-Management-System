using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HospitalQueueSystem.Hubs
{
    public class QueueHub : Hub
    {
        public async Task SendQueueUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveQueueUpdate",message);
        }
    }
}