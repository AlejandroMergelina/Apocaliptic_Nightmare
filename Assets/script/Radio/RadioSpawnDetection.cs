using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSpawnDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPieceOfRadio;
    [SerializeField]
    private LayerMask spawn;

    [SerializeField]
    private RadioDetaction rD;

    [SerializeField]
    private GameObject menuWin;

    [SerializeField]
    private GameControllerLevel gC;


    public void DetectSpawn(Vector2 point, float radius)
    {

        Collider2D[] spawners = Physics2D.OverlapCircleAll(point, radius, spawn);
    
        if(spawners.Length > 0)
        {

            Transform position = spawners[Random.Range(0, spawners.Length)].GetComponent<Transform>();

            GameObject pieceOfRadio = Instantiate(prefabPieceOfRadio, position.position, position.localRotation);

            rD.SetRadioInfo(pieceOfRadio);

            RadioInteraction rI;

            rI = pieceOfRadio.GetComponent<RadioInteraction>();

            rI.SetInfo(menuWin,gC);

        }
                
    }
}
