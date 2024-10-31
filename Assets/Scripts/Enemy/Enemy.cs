using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IEnemySub
{
    //TODO упростить
    [SerializeField] private Player player;

    [SerializeField] private float health;
    private bool isNearbyPlayer;

    //Таймер для нанесения урона
    //TODO перенести в отдельный класс
    private float timer = 0;
    private float endOfTime = 5;

    //Дамаг врага
    private float damage = 5;

    public float Health { get { return health; } set { health = value; } }
    public bool IsAlive { get { return health < 0; } }

    public void OnTriggered()
    {
        if(!IsAlive)
        {
            Die();
        }

        if (isNearbyPlayer)
        {
            timer = Time.deltaTime;
            if(timer >= endOfTime)
            {
                player.Health -= damage;
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
