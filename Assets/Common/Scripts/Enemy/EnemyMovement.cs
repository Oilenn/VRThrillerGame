using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemySub
{
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float followSpeed = 0.1f; // Скорость следования
    [SerializeField] private float distanceThreshold = 1.4f; // Минимальное расстояние до игрока

    private bool isStopped;
    public bool IsStopped {  get { return isStopped; } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = gameObject.GetComponent<Enemy>();   
    }

    public void OnTriggered()
    {
        if (player == null) return; // Если игрока нет, ничего не делаем

        // Вычисление направления к игроку
        Vector3 direction = player.position - transform.position;

        // Убираем компонент по оси Y, чтобы двигаться только в плоскости XZ
        direction.y = 0;

        // Если враг дальше заданного расстояния
        if (direction.magnitude > distanceThreshold)
        {
            isStopped = false;
            // Нормализация направления
            direction.Normalize();

            // Движение в сторону игрока
            transform.position += direction * followSpeed * Time.deltaTime;

            // Поворот в сторону игрока (только по оси Y)
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
        }
        else
        {
            isStopped = true;
        }
    }




    public bool OnDeactivated()
    {
        return !enemy.IsAlive;
    }
}
