using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private bool pause = false;

    protected int currentLevel;


    

    protected virtual void Start()
    {

        LevelData data = SaveSistem.LoadData();
        currentLevel = data.GetLevelInf();

        
        //SceneManager.GetActiveScene().buildIndex;
    }

    public void ChangeMenues(GameObject menue)
    {

        menue.SetActive(true);

    }

    public void OffMenues(GameObject menue)
    {

         menue.SetActive(false);
        
    }
    
    public void ChangeScene(int nexScene)
    {

        SceneManager.LoadScene(nexScene);

    }

    public void ChangeMusicVolume(float n)
    {

        AudioManager.Instance.SetMusicVolumen(n);

    }

    public void ChangeFbxVolume(float n)
    {

        AudioManager.Instance.SetFbxVolumen(n);

    }

    public void Pause(bool state)
    {

        pause = state;
        if (pause)
            Time.timeScale = 0;
        if(pause==false)
            Time.timeScale = 1;

    }
    

    public bool GetPause()
    {

        return pause;

    }

       
    public void Exit()
    {

        Application.Quit();

    }

}
