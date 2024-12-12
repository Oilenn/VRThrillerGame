using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject volume;
    [SerializeField] private GameObject creators;
    //[SerializeField] private List<GameObject> stages = new List<GameObject>();
    //private int currentStage = 0;

    //public void StepOff()
    //{
    //    stages[currentStage].SetActive(false);
    //    currentStage--;
    //    stages[currentStage].SetActive(true);
    //}

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        creators.SetActive(false);
    }
    public void OpenSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenVolume()
    {
        volume.SetActive(true);
        settings.SetActive(false);
    }

    public void OpenCreators()
    {
        creators.SetActive(true);
        settings.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume(int volume)
    {

    }

    public void ChangeMusic(int music)
    {

    }
}
