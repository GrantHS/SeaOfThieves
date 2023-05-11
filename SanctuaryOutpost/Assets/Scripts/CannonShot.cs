using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform CannonShotLocation;
    public GameObject fuseParticle, smokeParticle;
    public GameObject cannonSound;
    //public AudioSource cannonFire;
    private float CannonForce = 1000f;
    private bool _touching = false;

    private bool isLoaded = true;


    private void Awake()
    {
        smokeParticle.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isLoaded)
        {
            if(_touching && TorchPickUp.isPickingUp)
                StartCoroutine(ShootBall());
           
        }
    }

   
    IEnumerator ShootBall()
    {
        fuseParticle.SetActive(true);
        //cannonSound.SetActive(true);
        cannonSound.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        smokeParticle.SetActive(true);
        //smokeParticle.SetActive(true);
        smokeParticle.GetComponent<ParticleSystem>().Clear();
        //smokeParticle.GetComponent<ParticleSystem>().Stop();
        smokeParticle.GetComponent<ParticleSystem>().Play();
        //yield return new WaitForSeconds(smokeParticle.GetComponent<ParticleSystem>().main.duration);
        smokeParticle.SetActive(true);
        //smokeParticle.SetActive(false);
        fuseParticle.SetActive(false);
        GameObject cannonBall = Instantiate(cannonBallPrefab, CannonShotLocation.position, Quaternion.identity);
        Rigidbody CannonballRB = cannonBall.GetComponent<Rigidbody>();
        
        CannonballRB.AddForce(transform.forward * CannonForce);
        
        Destroy(cannonBall, 5f);
        //smokeParticle.SetActive(false);
       

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
