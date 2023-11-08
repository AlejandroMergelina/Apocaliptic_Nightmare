using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioInteraction : MonoBehaviour
{
    GameControllerLevel gC;

    GameObject menu;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {

            menu.SetActive(true);

            gC.Pause(true);

            gC.Save();

            Destroy(gameObject);

        }
    }

    public void SetInfo(GameObject menu, GameControllerLevel gC)
    {
        this.gC = gC;

        this.menu = menu;

    }
}
