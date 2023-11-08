using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerLevelSelect : GameController
{

    [SerializeField]
    private Button[] levelButton;

    protected override void Start()
    {
        base.Start();
        
        
        

        if (levelButton != null)
        {

            for (int i = 0; i <= currentLevel; i++)
            {
                print(i);
                levelButton[i].interactable= true;


            }

        }
    }

}
