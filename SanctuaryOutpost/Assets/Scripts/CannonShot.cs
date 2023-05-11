using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonShot : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform CannonShotLocation;
    public GameObject fuseParticle, smokeParticle;
    public GameObject cannonSound;
    public Text shootText;
    private float CannonForce = 1000f;
    private bool _touching = false;

    //private bool isLoaded = true;


    private void Awake()
    {
        smokeParticle.SetActive(false);
        shootText.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _touching)
        {
            if (TorchPickUp.isPickingUp)
            {
                shootText.enabled = false;
                StartCoroutine(ShootBall());
            }        
        }
    }

   
    IEnumerator ShootBall()
    {
        fuseParticle.SetActive(true);
        cannonSound.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(2);

        smokeParticle.SetActive(true);
        smokeParticle.GetComponent<ParticleSystem>().Clear();
        //smokeParticle.GetComponent<ParticleSystem>().Stop();
        smokeParticle.GetComponent<ParticleSystem>().Play();
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
            if(TorchPickUp.isPickingUp)
                shootText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _touching = false;
            shootText.enabled = false;
        }
    }

}
