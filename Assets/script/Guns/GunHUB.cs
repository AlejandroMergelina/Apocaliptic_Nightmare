using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHUB : MonoBehaviour
{
    [SerializeField]
    private GunManager GunM;

    [SerializeField]
    private GameObject[] ammunitionHUB;
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private Image gunimage;

    [SerializeField]
    private Sprite[] ammunitionSprits;
    [SerializeField]
    private Sprite[] gunSprits;
    private void OnEnable()
    {

        GunM.onAction += ChangeAmmunition;

    }

    private void ChangeAmmunition(Guns gun)
    {
        gunimage.sprite = gunSprits[gun.ID];
        if (gun.ID == 0)
        {

            images[0].sprite = ammunitionSprits[0];
            foreach(GameObject gB in ammunitionHUB)
            {

                gB.SetActive(false);

            }
            ammunitionHUB[0].SetActive(true);

        }
        else
        {

            SecundayGun secundayGun = (SecundayGun)gun;

            for (int i = 0; i < images.Length; i++)
            {
                print("tengo: " + secundayGun.CurrentAmmo);
                if(i< secundayGun.CurrentAmmo)
                {

                    ammunitionHUB[i].SetActive(true);
                    images[i].sprite = ammunitionSprits[gun.ID];

                }
                else
                {

                    ammunitionHUB[i].SetActive(false);

                }
            }


        }
        
    }

    void Awake()
    {
        //for (int i = 0; i < ammunitionHUB.Length; i++)
        //{

        //    images[i] = ammunitionHUB[i].GetComponent<Image>();

        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
