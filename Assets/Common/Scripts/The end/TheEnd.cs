using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{
    [SerializeField] private GameObject theEndTitle;

    private float time;
    private float targetTime = 8;

    private void Update()
    {
        if (theEndTitle.activeSelf)
        {
            time += Time.deltaTime;

            if(time > targetTime)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            theEndTitle.SetActive(true);
        }
    }
}
