using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : MonoBehaviour
{
    [SerializeField] private InteractionButton button;
    [SerializeField] private GameObject wall;
    [SerializeField] private Animator animator;
    public float speed = 2f; // Скорость подъёма стены
    public float targetHeight = 6f; // Конечная высота подъёма
    private bool isMoving = true;

    public void Update()
    {
        if (button.IsPressed)
        {
            if (isMoving)
            {
                // Двигаем стену вверх
                wall.transform.position += Vector3.up * speed * Time.deltaTime;

                // Останавливаем движение, если достигнута целевая высота
                if (transform.position.y >= targetHeight)
                {
                    wall.transform.position = new Vector3(wall.transform.position.x, targetHeight, wall.transform.position.z);
                    isMoving = false;
                }
            }
        }
    }
}
