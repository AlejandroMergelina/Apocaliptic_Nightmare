using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : SecundayGun
{

    public float range = 10; // encapsular

    public GameObject linePrefab; // encapsular

    private LineRenderer[] lineRenderer;

    private int lineIndex;

    [SerializeField]
    private LayerMask collisionLayerMask;

    

    protected override void Start()
    {

        base.Start();

        lineRenderer = new LineRenderer[bulletsPerShoot];

        for (int i = 0; i < bulletsPerShoot; i++)
        {

            GameObject gO = Instantiate(linePrefab);

            LineRenderer line = gO.GetComponent<LineRenderer>();
            line.endWidth = 0.1f;

            line.enabled = false;

            lineRenderer[i] = line;
            
            gO.transform.SetParent(transform);

        }
        lineIndex = 0;
    }

    protected override void ShootProjectile(Transform direction)
    {
        cShake.Shake(shakeDistance, shakeTime);
        RaycastHit2D hit = Physics2D.Raycast(direction.position, direction.up, range,collisionLayerMask);
        Vector3 _range = direction.position + direction.up * range;

        if (hit)
        {

            print(hit.collider.tag);



            _range = hit.point;




            GameObject obj = hit.collider.gameObject;


            if (obj.gameObject.tag == "Enemy")
            {
                EnemyHealth health = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    //gun.OnHit(health);

                }
                else
                {
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

                    enemy.OnDeath();

                }
            }



        }



        StartCoroutine(DoTheLineRenderer(direction.position,_range, lineIndex));
   
        lineIndex++;
   
        lineIndex %= lineRenderer.Length;
      
    }

    IEnumerator DoTheLineRenderer(Vector3 start, Vector3 end, int index)
    {

        LineRenderer lineRenderer = this.lineRenderer[index];

        lineRenderer.enabled = true;
        
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(maxCooldownTime * 0.9f);

        lineRenderer.enabled = false;

        CheckAmount();

    }

}
