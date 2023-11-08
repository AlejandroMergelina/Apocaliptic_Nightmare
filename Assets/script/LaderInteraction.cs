using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaderInteraction : MonoBehaviour
{
    private PlayerControler controller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            controller = collision.GetComponent<PlayerControler>();

            controller.SetInfoCollisionCanClimbingLader(true);

            if (controller.getInfoCollision().isClimbingLader)
            {

                collision.transform.position = new Vector3(transform.position.x, collision.transform.position.y, 0);

            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            controller.SetInfoCollisionCanClimbingLader(false);
            controller.SetInfoCollisionClimbingLader(false);
            

        }
    }

}
