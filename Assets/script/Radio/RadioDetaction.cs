using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioDetaction : MonoBehaviour
{
    private GameObject radio;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform[] topes;

    void Update()
    {
        if (radio != null)
            CalculateDistace();
    }

    private void CalculateDistace()
    {

        Vector2 poit1 = player.transform.position;
        Vector2 poit2 = radio.transform.position;

        Vector2 vector = poit1 - poit2;

        float distX =Mathf.Clamp(vector.x, -10f, 10f);

        float puoinM = (topes[0].position.x + topes[1].position.x) / 2;

        float range;

        if (topes[0].position.x < topes[1].position.x)
            range = (topes[1].position.x - topes[0].position.x)/2;
        else
            range = (topes[0].position.x - topes[1].position.x) / 2;

        float posX = (range * distX) / 10;

        Move(-posX + puoinM);

        float dis = Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
        
    }

    private void Move(float disX)
    {
        transform.position = new Vector2(disX, transform.position.y);

    }

    public void SetRadioInfo(GameObject radio)
    {

        this.radio = radio;

    }

}
