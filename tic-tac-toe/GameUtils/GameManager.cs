using tic_tac_toe.Respond;
using tic_tac_toe.utils;

namespace tic_tac_toe.GameUtils; 
public static class GameManager {
    private static List<Game> gamePool = new List<Game>();
    private static int index = 0;
    private static ReverseDictionary<string, Game> playerMap = new ReverseDictionary<string, Game>();
    //private static Dictionary<string, Game> playerMap = new Dictionary<string, Game>();

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
    public static List<string> getPlayers(int i) {
        return gamePool[i].players;
    }

    public static Game getGameByPlayer(string player) {
        return playerMap.GetValue(player);
    }

    public static string join(string player) {
        for (int i = 0; i < gamePool.Count; i++)
        {
            int j = (i + index) % gamePool.Count;
            if (gamePool[j].players.Count < 2) {
                playerMap.Set(player, gamePool[j]);
                return gamePool[j].joinGame(player);
            }
        }
        Console.WriteLine("Game full");
        return "";
    }

    public static bool leave(string player) {
        if (!isPlayerExist(player)) return false;

        Game game = playerMap.GetValue(player);
        if(playerMap.CountKeysForValue(game) == 1) {
            game.resetGame();
        }
        game.leaveGame(player);
        playerMap.Remove(player);

        return true;
    }

    public static StateResp getGameState(string player) {
        if (!isPlayerExist(player)) return new StateResp();
        Game game = playerMap.GetValue(player);
        StateResp state = game.getGameState(player);
        return state;
    }

    public static bool modifyCell(string player, int number) {
        Game game = playerMap.GetValue(player);
        return game.modifyCell(player, number);
    }

    public static void resetGame(int i) {
        gamePool[i].resetGame();
    }

    public static bool isPlayerExist(string player) {
        return playerMap.ContainsKey(player);
    }


}
