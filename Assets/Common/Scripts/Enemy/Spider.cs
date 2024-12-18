using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IEnemySub
{
    // Таймер и лимит времени
    private float time;
    private float limitTime = 2f;

    private bool isMoving; // Флаг движения

    private Vector3 startPos; // Стартовая позиция
    [SerializeField] private Vector3 limit = new Vector3(2, 0, 2); // Пределы квадрата (2x2 по X и Z)
    private Vector3 targetPosition; // Позиция назначения

    // Здоровье паука
    private float health = 1;
    private bool IsAlive { get { return health > 0; } }

    public void Start()
    {
        startPos = transform.position; // Сохраняем стартовую позицию
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

        // Таймер перед началом движения
        time += Time.deltaTime;
        if (time > limitTime && !isMoving)
        {
            GenerateDirection(); // Генерация новой точки назначения
            StartMove();
        }

        if (isMoving)
        {
            Move();
        }
    }

    private void GenerateDirection()
    {
        // Генерация точки в пределах квадрата вокруг стартовой позиции
        targetPosition = new Vector3(
            Random.Range(startPos.x - limit.x, startPos.x + limit.x),
            startPos.y,
            Random.Range(startPos.z - limit.z, startPos.z + limit.z)
        );
    }

    private void StartMove()
    {
        isMoving = true;

        // Поворот паука в сторону цели
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero) // Проверка, чтобы избежать нулевого вектора
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
        }
    }

    private void Move()
    {
        // Движение паука к точке назначения
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);

        // Проверка на достижение цели
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            time = 0; // Сброс таймера
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Удаление объекта
    }
}
