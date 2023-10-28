using tic_tac_toe.Respond;

namespace tic_tac_toe.GameUtils; 
public static class GameManager {
    private static List<Game> gamePool = new List<Game>();

    // init game pool
    static GameManager() {
        for (int i = 0; i < 1; i++)
        {
            Game game = new Game();
            game.resetGame();
            gamePool.Add(game);
        }
    }
    // todo remove
    public static List<string> getPlayers() {
        return gamePool[0].players;
    }

    public static string join(string player) {
        return gamePool[0].joinGame(player);
    }

    public static StateResp getGameState(string player) {
        StateResp state = new StateResp();
        state = gamePool[0].getGameState(player);
        //state.board = gamePool[0].getBoard();
        // todo
        // state.sign = player == XPlayer ? "X" : "O";
        //state.sign = "X";
        return state;
    }

    public static bool modifyCell(string player, int number) {
        return gamePool[0].modifyCell(player, number);
    }

    public static void resetGame() {
        gamePool[0].resetGame();
    }


}
