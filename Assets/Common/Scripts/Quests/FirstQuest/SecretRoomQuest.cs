using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomQuest : Quest
{
    [SerializeField] private InteractionButton button;
    [SerializeField] private SelfMoveWall wall;

    public override void StepOn()
    {
        if (button.IsPressed && !wall.IsMoving)
        {
            wall.StartMove();
        }

        if (wall.IsMoveDone())
        {
            DoneQuest();
        }
    }
}
