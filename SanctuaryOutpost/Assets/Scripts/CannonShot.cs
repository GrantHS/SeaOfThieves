using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform CannonShotLocation;
    public ParticleSystem fuseParticle;
    private float CannonForce = 1000f;

    private bool isLoaded = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isLoaded)
        {
            StartCoroutine(ShootBall());
           
        }
    }

   
    IEnumerator ShootBall()
    {
        fuseParticle.Play();
        yield return new WaitForSeconds(2);

        fuseParticle.Stop();
    }

}
