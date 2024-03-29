using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeAmount = 0;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {

            Shake(0.1f, 0.2f);

        }

    }

    public void Shake(float amt, float length)
    {

        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void BeginShake()
    {

        if (shakeAmount > 0)
        {

            Vector3 camPos = transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            transform.position = camPos;

        }

    }

    void StopShake()
    {

        CancelInvoke("BeginShake");
        transform.localPosition = Vector3.zero;

    }
}
