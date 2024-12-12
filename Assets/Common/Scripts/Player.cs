using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    [SerializeField] private float health;

    private AudioSource playerPain;

    public float Health { get { return health; } set { health = value; } }

    private void Start()
    {
        playerPain = GetComponent<AudioSource>();
    }

    public void PlayPainSound()
    {
        playerPain.Play();
    }

    private void Update()
    {
        healthBar.value = health;

        if(health < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
