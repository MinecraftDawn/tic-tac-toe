using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using tic_tac_toe.GameUtils;
using tic_tac_toe.Hubs;
using tic_tac_toe.Respond;

namespace tic_tac_toe.Services {
    
    public class TicTacToeService {

        private readonly IHubContext<TestHub> _hubContext;


        public TicTacToeService(IHubContext<TestHub> hubContext) {
            _hubContext = hubContext;
        }

        public string joinGame(string player) {
            var sign =  GameManager.join(player);

            Game game = GameManager.getGameByPlayer(player);
            if (game.players.Count == 2) {
                this.sendStateToAllPlayer(player);
            }

            return sign;

            //_hubContext.Clients.All.SendAsync("ReceiveMessage", this.getGameState("www"));
            //_hubContext.Clients.Groups(player).SendAsync("sayHi","HiHi");
        }

        public bool leaveGame(string player) {
            return GameManager.leave(player);
        }

        public StateResp getGameState(string player) {
            return GameManager.getGameState(player);
        }

        public bool modifyCell(string player, int number) {
            GameManager.modifyCell(player, number);

            sendStateToAllPlayer(player);

            //_hubContext.Clients.All.SendAsync("ReceiveMessage", this.getGameState(player));

            return true;
        }

        public void sendStateToAllPlayer(string player) {
            Game game = GameManager.getGameByPlayer(player);
            bool flag = false;
            foreach (string p in game.players) {
                var state = getGameState(p);
                if (!state.winner.Equals(" ")) flag = true;
                _hubContext.Clients.Client(p).SendAsync("ReceiveMessage", state);
            }
            if(flag) { game.resetGame(); }
        }

        public void resetGame(int i) {
            GameManager.resetGame(i);
        }
    }
}
