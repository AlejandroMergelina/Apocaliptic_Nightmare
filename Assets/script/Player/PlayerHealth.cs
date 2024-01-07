using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    private GameObject[] canvas;
    [SerializeField]
    private GameControllerLevel gameController;

    [SerializeField]
    private float invulnerabilityTime;
    [SerializeField]
    private float invulnerabilityTimer;

    private bool isInvulnerable;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float blinkTime;
    [SerializeField]
    private float blinkTimer;
    private bool isRed;

    protected override void Start()
    {
        base.Start();

        invulnerabilityTimer = invulnerabilityTime;
        blinkTimer = blinkTime;

    }
    private void Update()
    {

        if (isInvulnerable && invulnerabilityTimer > 0)
        {           
            
            invulnerabilityTimer -= Time.deltaTime;
            if(blinkTimer > 0)
            {
                print("parpadeo = " + blinkTimer);
                blinkTimer -= Time.deltaTime;
            }
            else
            {

                blinkTimer = blinkTime;
                if (isRed)
                {

                    spriteRenderer.color = Color.red;
                    isRed = false;
                }
                else
                {

                    spriteRenderer.color = Color.white;
                    isRed = true;
                }
                
            }
            
            

        }
        else
        {
            spriteRenderer.color = Color.white;
            invulnerabilityTimer = invulnerabilityTime;
            blinkTimer = blinkTime;
            isInvulnerable = false;
        }


    }

    protected override void OnDeath()
    {

        canvas[0].SetActive(true);
        canvas[1].SetActive(false);
        canvas[2].SetActive(false);
        gameController.Pause(true);

    }

    public override void LoseHealth(float healthMinus)
    {
        if(isInvulnerable == false)
        {

            base.LoseHealth(healthMinus);
            isInvulnerable = true;

        }



    }
}
