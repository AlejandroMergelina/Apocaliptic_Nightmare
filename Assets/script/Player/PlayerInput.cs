using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    Player player;
    Vector2 directionalInput;



    void Start()
    {
        
        player = GetComponent<Player>();

    }

    
    void Update()
    {
        directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            player.OnJumpInput();

        }

    }

    public Vector2 GettInfoDirection()
    {

        return directionalInput;

    }
}
