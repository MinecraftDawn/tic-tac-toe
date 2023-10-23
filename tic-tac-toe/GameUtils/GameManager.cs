namespace tic_tac_toe.GameUtils; 
public static class GameManager {
    private static List<Game> gamePool = new List<Game>();

    static GameManager() {
        for (int i = 0; i < 10; i++)
        {
            gamePool.Add(new Game());
        }
    }
}
