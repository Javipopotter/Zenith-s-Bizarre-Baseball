using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChangerButton : ButtonControllerParent
{
    [SerializeField] GameStates state;

    public override void OnButtonClicked()
    {
        GameManager.GM.ChangeStateOfGame(state);
    }
}
