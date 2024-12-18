using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IEnemySub
{
    // ������ � ����� �������
    private float time;
    private float limitTime = 2f;

    private bool isMoving; // ���� ��������

    private Vector3 startPos; // ��������� �������
    [SerializeField] private Vector3 limit = new Vector3(2, 0, 2); // ������� �������� (2x2 �� X � Z)
    private Vector3 targetPosition; // ������� ����������

    // �������� �����
    private float health = 1;
    private bool IsAlive { get { return health > 0; } }

    public void Start()
    {
        startPos = transform.position; // ��������� ��������� �������
    }

    public bool OnDeactivated()
    {
        return !IsAlive;
    }

    public void OnTriggered()
    {
        if (!IsAlive)
        {
            Die();
        }

        // ������ ����� ������� ��������
        time += Time.deltaTime;
        if (time > limitTime && !isMoving)
        {
            GenerateDirection(); // ��������� ����� ����� ����������
            StartMove();
        }

        if (isMoving)
        {
            Move();
        }
    }

    private void GenerateDirection()
    {
        // ��������� ����� � �������� �������� ������ ��������� �������
        targetPosition = new Vector3(
            Random.Range(startPos.x - limit.x, startPos.x + limit.x),
            startPos.y,
            Random.Range(startPos.z - limit.z, startPos.z + limit.z)
        );
    }

    private void StartMove()
    {
        isMoving = true;

        // ������� ����� � ������� ����
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero) // ��������, ����� �������� �������� �������
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
        }
    }

    private void Move()
    {
        // �������� ����� � ����� ����������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);

        // �������� �� ���������� ����
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            time = 0; // ����� �������
        }
    }

    private void Die()
    {
        Destroy(gameObject); // �������� �������
    }
}
