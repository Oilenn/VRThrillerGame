using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : MonoBehaviour
{
    [SerializeField] private InteractionButton button;
    [SerializeField] private GameObject wall;
    [SerializeField] private Animator animator;
    public float speed = 2f; // �������� ������� �����
    public float targetHeight = 6f; // �������� ������ �������
    private bool isMoving = true;

    public void Update()
    {
        if (button.IsPressed)
        {
            if (isMoving)
            {
                // ������� ����� �����
                wall.transform.position += Vector3.up * speed * Time.deltaTime;

                // ������������� ��������, ���� ���������� ������� ������
                if (transform.position.y >= targetHeight)
                {
                    wall.transform.position = new Vector3(wall.transform.position.x, targetHeight, wall.transform.position.z);
                    isMoving = false;
                }
            }
        }
    }
}
