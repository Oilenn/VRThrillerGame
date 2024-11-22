using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemySub
{
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float followSpeed = 0.1f; // —корость следовани€
    [SerializeField] private float distanceThreshold = 2f; // ћинимальное рассто€ние до игрока

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = gameObject.GetComponent<Enemy>();   
    }

    public void OnTriggered()
    {
        // ѕроверка, что ссылка на игрока установлена
        if (player != null)
        {
            // ¬ычисление вектора направлени€ к игроку
            Vector3 direction = player.position - transform.position;

            // ќставл€ем движение только в плоскости XZ (обнул€ем Y)
            direction.y = 0;

            float distance = direction.magnitude; // –ассто€ние до игрока

            // ≈сли рассто€ние больше, чем заданный порог
            if (distance > distanceThreshold)
            {
                // Ќормализаци€ вектора направлени€
                direction.Normalize();

                // ѕоворот врага в направлении игрока (только по оси Y)
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);

                // ƒвижение врага в сторону игрока
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
