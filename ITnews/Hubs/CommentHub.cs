using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Hubs
{
    public class CommentHub : Hub
    {
            public async Task Send(string message)
            {
                await Clients.All.SendAsync("Send", message);
            }
    }
}
