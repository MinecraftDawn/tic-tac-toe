using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using tic_tac_toe.Hubs;
using tic_tac_toe.Respond;

namespace tic_tac_toe.Services {
    
    public class TicTacToeService {

        private readonly IHubContext<TestHub> _hubContext;

        protected static int BOARD_SIZE = 9;
        public List<string> players = new List<string>();
        protected List<string> board = new List<string>();
        protected string XPlayer = "";
        protected string OPlayer = "";
        protected string turn = "X";

        public TicTacToeService(IHubContext<TestHub> hubContext) {
            _hubContext = hubContext;
            resetGame();
        }

        public string joinGame(string player) {

            players.Add(player);

            if (XPlayer == "") {
                XPlayer = player;
                return "X";
            } else if (OPlayer == "") {
                OPlayer = player;
                return "O";
            }

            return "";

            //_hubContext.Clients.All.SendAsync("ReceiveMessage", this.getGameState("www"));
            //_hubContext.Clients.Groups(player).SendAsync("sayHi","HiHi");
        }

        protected void initGameBoard() {
            board = new List<string> {};
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                board.Add(" ");
            }
        }

        public StateResp getGameState(string player) {
            StateResp state = new StateResp();
            state.board = board;
            state.sign = player == XPlayer ? "X" : "O";
            return state;
        }

        public bool modifyCell(string player, int number) {
            if (number >= BOARD_SIZE) return false;
            if (board[number] != " ") return false;

            if (turn == "X" && player == XPlayer) {
                board[number] = "X";
                turn = "O";
            } else if(turn == "O" && player == OPlayer) {
                board[number] = "O";
                turn = "X";
            }

            foreach (string p in players) {
                var state = getGameState(p);
                _hubContext.Clients.Group(p).SendAsync("ReceiveMessage", state);
                Console.WriteLine(p + " , state:" + state);
            }

            //_hubContext.Clients.All.SendAsync("ReceiveMessage", this.getGameState(player));



            return true;
        }

        public void resetGame() {
            players = new List<string>();
            initGameBoard();
            XPlayer = "";
            OPlayer = "";
            turn = "X";

        }
    }
}
