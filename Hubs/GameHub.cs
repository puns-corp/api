using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PunsApi.Hubs.Interfaces;

namespace PunsApi.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        public async Task JoinGame(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        }

        public async Task LeaveGame(Guid gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        }

        public async Task TestSignalRSendMessage(string message)
        {
            await Clients.Caller.SendMessageToTest("this message come from frontend by signalR: " + message);
        }
    }
}
