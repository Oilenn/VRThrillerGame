using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemySub
{
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float followSpeed = 0.1f; // �������� ����������
    [SerializeField] private float distanceThreshold = 1.4f; // ����������� ���������� �� ������

    private bool isStopped;
    public bool IsStopped {  get { return isStopped; } }


    private bool isPlayerFound
    {
        get
        {
            return Vector3.Distance(transform.position, player.transform.position) < 12;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = gameObject.GetComponent<Enemy>();   
    }

    public void OnTriggered()
    {
        if (player == null) return; // ���� ������ ���, ������ �� ������

        if (isPlayerFound)
        {
            // ���������� ����������� � ������
            Vector3 direction = player.position - transform.position;

            // ������� ��������� �� ��� Y, ����� ��������� ������ � ��������� XZ
            direction.y = 0;

            // ���� ���� ������ ��������� ����������
            if (direction.magnitude > distanceThreshold)
            {
                isStopped = false;
                // ������������ �����������
                direction.Normalize();

                // �������� � ������� ������
                transform.position += direction * followSpeed * Time.deltaTime;

                // ������� � ������� ������ (������ �� ��� Y)
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
            }
            else
            {
                isStopped = true;
            }
        }
    }




    public bool OnDeactivated()
    {
        return !enemy.IsAlive;
    }
}
