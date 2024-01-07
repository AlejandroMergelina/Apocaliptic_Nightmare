using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField]
    PlayerController pC;
    [SerializeField]
    PlayerInput pI;
    [SerializeField]
    Animator animator;
    [SerializeField]
    PlayerHealth pH;
    [SerializeField]
    GunManager gM;
    
    void Start()
    {
        
    }

    
    void Update()
    {

        animator.SetFloat("move", Mathf.Abs(pI.GettInfoDirection().x));
        //print("aqui" + pC.getInfoCollision().isClimbingLader);

        animator.SetBool("on clim", pC.getInfoCollision().isClimbingLader);

        animator.SetBool("fall", pC.getInfoCollision().below);

        animator.SetFloat("moveY", pI.GettInfoDirection().y);

        animator.SetFloat("health", pH.GetInfoCurrentHealth());

        animator.SetBool("on shoot", gM.GetInfoOnShoot());

        if (Input.GetKeyDown(KeyCode.Space))
        {

            animator.SetBool("on jump", true);
            

        }
        else
        {

            animator.SetBool("on jump", false);

        }
    }
}
