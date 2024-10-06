using Godot;
using System;

public partial class QuitButton : Node
{
    public void Press()
    {
        GetTree().Quit();
    }
}
