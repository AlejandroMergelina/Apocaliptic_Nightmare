using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private PlayerInput input;
    private SpriteRenderer sprite;

    [SerializeField]
    private GameControllerLevel gameController;

    private void Start()
    {

        input = GetComponent<PlayerInput>();

        sprite = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        if (gameController.GetPause() == false)
        {

            if (input.GettInfoDirection().x == -1)
            {

                sprite.flipX = true;

            }
            else if (input.GettInfoDirection().x == 1)
            {

                sprite.flipX = false;

            }

        }
        
    }

}
