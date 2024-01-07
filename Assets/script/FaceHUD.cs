using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceHUD : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth pH;

    [SerializeField]
    private Sprite[] sp;

    [SerializeField]
    private Image image;

    void Update()
    {

        if (pH.GetInfoCurrentHealth() >=2 )
        {

            image.sprite = sp[0];

        }
        else if(pH.GetInfoCurrentHealth() == 1)
        {

            image.sprite = sp[1];

        }
        else if(pH.GetInfoCurrentHealth() <= 0)
        {

            image.sprite = sp[2];

        }

    }
}
