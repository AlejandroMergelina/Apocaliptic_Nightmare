using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    public Guns CurrentGuns;

    public Transform gunHolder;

    private bool haveAGun = false;

    PlayerControler controller;

    [SerializeField]
    private GameControllerLevel gameController;
    [SerializeField]
    private bool onShoot;

    private void Awake()
    {
        controller = GetComponent<PlayerControler>();

        Guns[] guns = GetComponentsInChildren<Guns>();
        foreach(Guns g in guns)
        {


            PickUpGuns(g);

        }


    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && controller.getInfoCollision().below && gameController.GetPause() == false)
        {

            CurrentGuns.Activate();
            StartCoroutine(ResetOnShoot());
            onShoot = true;
        }

    }

    public void PickUpGuns(Guns guns)
    {
        if (haveAGun)
        {

            CurrentGuns.AtoDestroy();

        }
        

        guns.transform.position = gunHolder.position;
        guns.transform.rotation = gunHolder.rotation;
        guns.transform.SetParent(gunHolder);

        CurrentGuns = guns;

        haveAGun = true;

    }

    public bool GetInfoOnShoot()
    {


        return onShoot;

    }

    IEnumerator ResetOnShoot()
    {



        yield return new WaitForSeconds(0.08f);

        onShoot = false;

    }

}
