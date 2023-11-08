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
    private RadioSpawnDetection detection;

    [SerializeField]
    private Animator animator;

    

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

            detection.DetectSpawn(_enemyDeathPosition, 10);

        }

    }

}
