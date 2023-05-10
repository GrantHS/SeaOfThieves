using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchPickUp : MonoBehaviour
{
    public float pickupDistance = 2f;
    //public GameObject torchPrefab;
    public Transform torchHolder;
    public Text pickupText;

    private GameObject torchObject;
    public static bool isPickingUp = false;


    private void Awake()
    {
        pickupText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            pickupText.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            pickupText.enabled = false;
        }
    }
    private void Update()
    {
        if (!isPickingUp && Input.GetKeyDown(KeyCode.F))
        {
            // Cast a ray in front of the player to check for torch objects to pick up
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDistance))
            {

                //debuglog raycast to see it 
              

                // Check if the object is a torch
                if (hit.collider.gameObject.CompareTag("Torch"))
                {
                    torchObject = hit.collider.gameObject;
                    isPickingUp = true;

                    // Show the pickup text
                    //pickupText.enabled = true;

                    // Disable the torch's collider and rigidbody
                    Rigidbody rb = torchObject.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }
                    torchObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
        else if (isPickingUp && Input.GetKeyDown(KeyCode.F))
        {
            // Hide the pickup text
            pickupText.enabled = false;

            // Set the torch's parent to the player's transform
            torchObject.transform.SetParent(torchHolder, false);
            torchObject.transform.localPosition = Vector3.zero;
            torchObject.transform.localRotation = Quaternion.identity;

            isPickingUp = false;
            torchObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * pickupDistance);
    }
}
