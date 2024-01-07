using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTopLader : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
 
            controller.SetInfoCollisionDownLader(true);

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            controller.SetInfoCollisionDownLader(false);

        }
    }
}
