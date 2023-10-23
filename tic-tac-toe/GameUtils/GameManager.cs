﻿using tic_tac_toe.Respond;

namespace tic_tac_toe.GameUtils; 
public static class GameManager {
    private static List<Game> gamePool = new List<Game>();

    static GameManager() {
        for (int i = 0; i < 1; i++)
        {
            Game game = new Game();
            game.resetGame();
            gamePool.Add(game);
        }
    }

    static string join(string player) {
        return gamePool[0].joinGame(player);
    }

    static StateResp getGameState(string player) {
        StateResp state = new StateResp();
        state.board = gamePool[0].getBoard();
        // todo
        // state.sign = player == XPlayer ? "X" : "O";
        state.sign = "X";
        return state;
    }

    static bool modifyCell(string player, int number) {
        return gamePool[0].modifyCell(player, number);
    }


}
