using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    private GameObject[] canvas;
    [SerializeField]
    private GameControllerLevel gameController; 

    protected override void OnDeath()
    {

        canvas[0].SetActive(true);
        canvas[1].SetActive(false);
        canvas[2].SetActive(false);
        gameController.Pause(true);

    }
}
