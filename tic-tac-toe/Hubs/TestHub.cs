using Microsoft.AspNetCore.SignalR;
using System.Numerics;
using tic_tac_toe.Services;

namespace tic_tac_toe.Hubs {
    public class TestHub : Hub {
        private readonly TicTacToeService _service;

        public TestHub(TicTacToeService service) {
            _service = service;
        }

        public async Task SendMessage(string user, string message) {
            var state = _service.getGameState(user);
            Console.WriteLine(user + " : "+ message);
            await Clients.All.SendAsync("ReceiveMessage", state);

        }

        public async Task joinWebsocket(string user) {
            await Groups.AddToGroupAsync(Context.ConnectionId, user);
        }


    }
}
