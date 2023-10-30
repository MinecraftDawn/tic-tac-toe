using Microsoft.AspNetCore.SignalR;
using System.Numerics;
using tic_tac_toe.GameUtils;
using tic_tac_toe.Services;

namespace tic_tac_toe.Hubs {
    public class TestHub : Hub {
        private readonly TicTacToeService _service;

        public TestHub(TicTacToeService service) {
            _service = service;
        }

        public async Task SendMessage(string user, string message) {
            //foreach (string player in _service.players) {
            //    var state = _service.getGameState(player);
            //    await Clients.Group(player).SendAsync("ReceiveMessage", state);
            //    Console.WriteLine(player + " " + state);
            //}
            var state = _service.getGameState(user);
            Console.WriteLine(user + " : " + message);
            //await Clients.All.SendAsync("ReceiveMessage", state);

            Game game = GameManager.getGameByPlayer(user);
            foreach (string p in game.players) {
                var s = _service.getGameState(p);
                await Clients.Group(p).SendAsync("ReceiveMessage", state);
                //Console.WriteLine(p + " , state:" + state);
            }

        }

        public async Task joinWebsocket(string user) {
            await Groups.AddToGroupAsync(Context.ConnectionId, user);
            _service.joinGame(user);

            Game game = GameManager.getGameByPlayer(user);
            if (game.players.Count > 1) {
                _service.sendStateToAllPlayer(user);
            }
        }


    }
}
