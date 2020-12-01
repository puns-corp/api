using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Hubs.Interfaces
{
    public interface IGameHub
    {
        Task PlayerJoined(string playerName);

        Task PlayerQuit(string playerName);

        Task GameStarted();

        Task SendMessageToTest(string message);
    }
}
