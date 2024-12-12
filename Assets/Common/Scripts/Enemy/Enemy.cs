using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IEnemySub
{
    //TODO упростить
    private GameObject playerObj;
    private Player player;

    [SerializeField] private Slider bar;
    private EnemyHealthBar healthBar;
    private EnemyMovement enemyMovement;
    [SerializeField] private float health = 10;
    private bool isNearbyPlayer;

    private AudioSource punchAudio;

    //Таймер для нанесения урона
    //TODO перенести в отдельный класс
    private float timer = 0;
    private float endOfTime = 1;

    //Дамаг врага
    private float damage = 5;

    public float Health { get { return health; } set { health = value; } }
    public bool IsAlive { get { return health > 0; } }

    public void Start()
    {
        punchAudio = GetComponent<AudioSource>();
        healthBar = new EnemyHealthBar(health, bar);
        healthBar.UpdateHealthBar(health);
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void OnTriggered()
    {
        healthBar.UpdateHealthBar(health);

        if (!IsAlive)
        {
            Die();
        }

        if (enemyMovement.IsStopped)
        {
            Debug.Log("SDFd");
            timer += Time.deltaTime;
            if(timer >= endOfTime)
            {
                timer = 0;
                punchAudio.Play();
                player.Health -= damage;
                player.PlayPainSound();
            }
        }
        else
        {
            timer = 0;
        }
    }

    public bool OnDeactivated()
    {
        return !IsAlive;
    }

    public void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
