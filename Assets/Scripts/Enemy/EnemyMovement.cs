using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemySub
{
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float followSpeed = 2f; // �������� ����������
    [SerializeField] private float distanceThreshold = 2f; // ����������� ���������� �� ������

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();   
    }

    public void OnTriggered()
    {
        // ��������, ��� ������ �� ������ �����������
        if (player != null)
        {
            // ���������� ������� ����������� � ������
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude; // ���������� �� ������

            // ���� ���������� ������, ��� �������� �����
            if (distance > distanceThreshold)
            {
                // ������������ ������� ����������� � �������� �����
                direction.Normalize();
                Vector3 movePosition = transform.position + direction * followSpeed * Time.deltaTime;
                transform.position = movePosition;
            }
        }
    }
}
