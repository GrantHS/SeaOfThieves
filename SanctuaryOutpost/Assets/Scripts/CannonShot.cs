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
    //public AudioSource cannonFire;
    public Text shootText;
    private float CannonForce = 1000f;
    private bool _touching = false;

    private void Start()
    {
        smokeParticle.SetActive(false);
        shootText.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _touching)
        {
            Debug.Log("Not Firing!");
            if (TorchPickUp.isPickingUp)
            {
                Debug.Log("Firing!");
                shootText.enabled = false;
                StartCoroutine(ShootBall());
            }


        }
    }


    IEnumerator ShootBall()
    {
        fuseParticle.SetActive(true);
        //cannonSound.SetActive(true);
        cannonSound.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.6f);
        smokeParticle.SetActive(true);
        //smokeParticle.SetActive(true);
        smokeParticle.GetComponent<ParticleSystem>().Clear();
        smokeParticle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(.25f);
        //yield return new WaitForSeconds(smokeParticle.GetComponent<ParticleSystem>().main.duration);
        //smokeParticle.SetActive(false);
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
            if(TorchPickUp.isPickingUp)
                shootText.enabled = true;

            _touching = true;
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
