using Microsoft.AspNetCore.SignalR;
using tic_tac_toe.Respond;

namespace tic_tac_toe.GameUtils;
public class Game {
    public static int BOARD_SIZE = 9;
    public List<string> players = new List<string>();
    protected List<string> board = new List<string>();
    protected string XPlayer = "";
    protected string OPlayer = "";
    protected string turn = "X";
    public bool isUsing = false;
    protected bool anyPlayerLeave = false;
    
    public short getPlayerNumber() {
        if (XPlayer == "") return 0;
        else if (OPlayer == "") return 1;
        else return 2;
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

    }

    public bool leaveGame(string player) {
        if (players.Contains(player)) {
            players.Remove(player);
            anyPlayerLeave = true;
            return true;
        }
        return false;
    }

    public List<string> getBoard() {
        return board;
    }

    protected void initGameBoard() {
        board = new List<string> { };
        for (int i = 0; i < BOARD_SIZE; i++) {
            board.Add(" ");
        }
    }

    public StateResp getGameState(string player) {
        StateResp state = new StateResp();
        state.board = board;
        state.sign = player == XPlayer ? "X" : "O";
        state.winner = getWinner();
        return state;
    }

    public bool modifyCell(string player, int number) {
        if (number >= BOARD_SIZE) return false;
        if (board[number] != " ") return false;

        if (turn == "X" && player == XPlayer) {
            board[number] = "X";
            turn = "O";
        } else if (turn == "O" && player == OPlayer) {
            board[number] = "O";
            turn = "X";
        }

        //sendStateToAllPlayer();

        return true;
    }

    //public void sendStateToAllPlayer() {
    //    foreach (string p in players) {
    //        var state = getGameState(p);
    //        _hubContext.Clients.Group(p).SendAsync("ReceiveMessage", state);
    //    }
    //}

    public void resetGame() {
        players.Clear();
        initGameBoard();
        XPlayer = "";
        OPlayer = "";
        turn = "X";
        anyPlayerLeave = false;

    }

    public string getWinner() {
        if(this.anyPlayerLeave) {
            string player = players[0];
            if (player == XPlayer) return "X";
            return "O";
        }

        ushort[,] lines = new ushort[,] { { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 },
        };

        for (int i = 0; i < 7; i++) {
            if (board[lines[i, 0]] == board[lines[i, 1]] &&
            board[lines[i, 1]] == board[lines[i, 2]] &&
            board[lines[i, 0]] != " ") {
                return board[lines[i, 0]];
            }
        }

        return " ";
    }

}
