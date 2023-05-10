using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform CannonShotLocation;
    public GameObject fuseParticle;
    public GameObject cannonSound;
    private float CannonForce = 1000f;
    private bool _touching = false;

    private bool isLoaded = true;


   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isLoaded)
        {
            if(_touching)
                StartCoroutine(ShootBall());
           
        }
    }

   
    IEnumerator ShootBall()
    {
        fuseParticle.SetActive(true);
        cannonSound.SetActive(true);
        yield return new WaitForSeconds(2);
        fuseParticle.SetActive(false);
        GameObject cannonBall = Instantiate(cannonBallPrefab, CannonShotLocation.position, Quaternion.identity);
        Rigidbody CannonballRB = cannonBall.GetComponent<Rigidbody>();
        CannonballRB.AddForce(transform.forward * CannonForce);
        Destroy(cannonBall, 5f);
        cannonSound.SetActive(false);
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touching");
            _touching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _touching = false;
        }
    }

}
