using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerLevel : GameController
{
    [SerializeField]
    private int nextLevel;

    private int currentNEnemys;
    [SerializeField]
    private int maxNEemys;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject menuWin;

    protected override void Start()
    {
        base.Start();

        currentNEnemys = maxNEemys;
    }

    public void Save()
    {
       
        if (nextLevel > currentLevel)
        {

            SaveSistem.SaveLevel(nextLevel);

        }
     

    }
    
    public void AnimationStop()
    {

        animator.updateMode = AnimatorUpdateMode.Normal;

    }

    public void AnimatioRun()
    {

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

    }

    public void NEnemy(Transform enemyDeathPosition, int n)
    {
        Vector2 _enemyDeathPosition = enemyDeathPosition.position;
        currentNEnemys -= n;
        print("check" + currentNEnemys);
        if (currentNEnemys <= 0)
        {
            menuWin.SetActive(true);

            Pause(true);

            Save();
        }

    }

}
