using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionEnemy : MonoBehaviour
{

    private Enemy enemy;

    private SpriteRenderer sprite;

    void Start()
    {

        enemy = GetComponent<Enemy>();

        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Sign(enemy.GetInfoDirection()) == -1)
        {

            sprite.flipX = true;

        }
        else if(Mathf.Sign(enemy.GetInfoDirection()) == 1)
        {

            sprite.flipX = false;

        }
        
    }
}
