using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
