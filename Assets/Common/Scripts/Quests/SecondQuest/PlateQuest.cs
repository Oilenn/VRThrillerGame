using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateQuest : Quest
{
    [SerializeField] private Plate plate;
    [SerializeField] private SelfMoveWall wall;

    public override void StepOn()
    {
        if (plate.IsStepped && !wall.IsMoving)
        {
            wall.StartMove();
        }
    }
}
