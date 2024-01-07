using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;
    private Vector2 directionalInput;
    public Vector2 DirectionalInput { get => directionalInput; set => directionalInput = value; }



    void Start()
    {
        
        player = GetComponent<Player>();

    }

    
    void Update()
    {
        DirectionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(DirectionalInput);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            player.OnJumpInput();

        }
                
    }

    public Vector2 GettInfoDirection()
    {

        return DirectionalInput;

    }
}
