using tic_tac_toe.Respond;

namespace tic_tac_toe.Services {
    
    public class TicTacToeService {

        protected static int BOARD_SIZE = 9;
        protected List<string> players = new List<string>();
        protected List<string> board = new List<string>();
        protected string XPlayer = "";
        protected string OPlayer = "";
        protected string turn = "X";

        public TicTacToeService() {
            resetGame();
        }

        public void joinGame(string player) { 
            players.Add(player);

            if (XPlayer == "") {
                XPlayer = player;
            } else if (OPlayer == "") {
                OPlayer = player;
            }
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

            if(turn == "X" && player == XPlayer) {
                board[number] = "X";
                turn = "O";
            } else if(turn == "O" && player == OPlayer) {
                board[number] = "O";
                turn = "X";
            }

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
