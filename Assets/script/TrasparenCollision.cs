using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasparenCollision : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private PlayerController controller;

    private Collider2D collision;

    void Start()
    {
        
        collision = GetComponent<Collider2D>();

    }


    void Update()
    {
        if (transform.position.y > player.transform.position.y)
        {

             collision.enabled = false;

        }
        else
        {

            collision.enabled = true;

        }

        if (controller.getInfoCollision().isTryingDonwLader)
        {

            collision.enabled = false;

        }

    }
    
}
