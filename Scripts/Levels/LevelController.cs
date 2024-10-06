using Godot;
using System;

public partial class LevelController : Node
{
    private static int CurrentLevel = 0;

    [Export] private GameFlow GameFlow;

    public override void _Ready()
    {
        base._Ready();
        GameFlow.ConnectLevel(Levels[Mathf.Min(CurrentLevel, Levels.Count - 1)]); // Failsafe
    }

    public void RestartLevel()
    {
        SceneController.Current.TransitionToScene("Game");
    }

    public void NextLevel()
    {
        if (CurrentLevel + 1 >= Levels.Count)
        {
            SceneController.Current.TransitionToScene("Win");
        }
        else
        {
            CurrentLevel++;
            SceneController.Current.TransitionToScene("Game");
        }
    }
}
