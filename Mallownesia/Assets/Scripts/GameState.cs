using UnityEngine;
using System;

public enum GameMode { Exploration, InPuzzleUI, Paused }

public static class GameState
{
    static GameMode _mode = GameMode.Exploration;
    public static GameMode Mode => _mode;
    public static bool IsFreeToInteract => _mode == GameMode.Exploration;
    public static event Action<GameMode> OnModeChanged;

    public static void Set(GameMode m)
    {
        if (_mode == m) return;
        _mode = m;
        OnModeChanged?.Invoke(_mode);
    }
}

