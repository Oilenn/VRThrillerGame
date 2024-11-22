using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemySub
{
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float followSpeed = 0.1f; // �������� ����������
    [SerializeField] private float distanceThreshold = 2f; // ����������� ���������� �� ������

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = gameObject.GetComponent<Enemy>();   
    }

    public void OnTriggered()
    {
        // ��������, ��� ������ �� ������ �����������
        if (player != null)
        {
            // ���������� ������� ����������� � ������
            Vector3 direction = player.position - transform.position;

            // ��������� �������� ������ � ��������� XZ (�������� Y)
            direction.y = 0;

            float distance = direction.magnitude; // ���������� �� ������

            // ���� ���������� ������, ��� �������� �����
            if (distance > distanceThreshold)
            {
                // ������������ ������� �����������
                direction.Normalize();

                // ������� ����� � ����������� ������ (������ �� ��� Y)
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);

                // �������� ����� � ������� ������
                Vector3 movePosition = transform.position + direction * followSpeed * Time.deltaTime;
                transform.position = movePosition;
            }
        }
    }



    public bool OnDeactivated()
    {
        return !enemy.IsAlive;
    }
}
