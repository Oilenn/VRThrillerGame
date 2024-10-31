using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemySub
{
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float followSpeed = 2f; // Скорость следования
    [SerializeField] private float distanceThreshold = 2f; // Минимальное расстояние до игрока

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();   
    }

    public void OnTriggered()
    {
        // Проверка, что ссылка на игрока установлена
        if (player != null)
        {
            // Вычисление вектора направления к игроку
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude; // Расстояние до игрока

            // Если расстояние больше, чем заданный порог
            if (distance > distanceThreshold)
            {
                // Нормализация вектора направления и движение врага
                direction.Normalize();
                Vector3 movePosition = transform.position + direction * followSpeed * Time.deltaTime;
                transform.position = movePosition;
            }
        }
    }
}
