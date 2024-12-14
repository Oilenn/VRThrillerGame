using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomQuest : Quest
{
    [SerializeField] private InteractionButton button;
    [SerializeField] private Wall wall;
    public float speed = 2f; // Скорость подъёма стены
    public float targetHeight = 6f; // Конечная высота подъёма
    private bool isMoving = true;

    public void MoveWall()
    {
        wall.transform.position += Vector3.up * speed * Time.deltaTime;
    }


    public override void StepOn()
    {
        if (button.IsPressed)
        {
            if (isMoving)
            {
                MoveWall();

                // Останавливаем движение, если достигнута целевая высота
                if (transform.position.y >= targetHeight)
                {
                    wall.transform.position = new Vector3(wall.transform.position.x, targetHeight, wall.transform.position.z);
                    DoneQuest();
                }
            }
        }
    }
}
