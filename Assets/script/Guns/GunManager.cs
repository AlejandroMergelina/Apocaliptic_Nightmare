using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunManager : MonoBehaviour
{

    public Guns CurrentGuns;

    public Transform gunHolder;

    private bool haveAGun = false;

    PlayerController controller;

    [SerializeField]
    private GameControllerLevel gameController;
    [SerializeField]
    private bool onShoot;

    public event Action<Guns> onAction;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();

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
            onAction?.Invoke(CurrentGuns);
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
        onAction?.Invoke(CurrentGuns);
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
