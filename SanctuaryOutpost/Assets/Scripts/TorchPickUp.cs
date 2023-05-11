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
    private bool _canPickup = false;


    private void Awake()
    {
        pickupText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            _canPickup = true;
            pickupText.enabled = true;
            torchObject = other.GetComponentInChildren<Torchy>().gameObject;
            //_torchAlign = torchObject.transform.rotation;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            _canPickup = false;
            pickupText.enabled = false;
        }
    }
    private void Update()
    {
        Debug.Log(isPickingUp);
        if (isPickingUp && Input.GetKeyDown(KeyCode.F) && !pickupText.isActiveAndEnabled)
        {
            isPickingUp = false;

            // Show the pickup text
            //pickupText.enabled = true;

            // Drop the torch
            torchObject.SetActive(false);
            torchObject = null;
            //torchObject.GetComponent<Collider>().enabled = true;

            /*
            // Cast a ray in front of the player to check for torch objects to pick up
            RaycastHit hit;

            Vector3 forward = transform.TransformDirection(Vector3.forward) * pickupDistance;
            Debug.DrawRay(transform.position, forward, Color.red);

            if (Physics.Raycast(transform.position, forward, out hit))
            {

                //debuglog raycast to see it 
                Debug.Log("Drawing Ray");
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
            */
        }
        else if (!isPickingUp && Input.GetKeyDown(KeyCode.F) && pickupText.isActiveAndEnabled)
        {
            isPickingUp = true;
            // Hide the pickup text
            pickupText.enabled = false;

            // Set the torch's parent to the player's transform
            torchObject.transform.SetParent(torchHolder, false);
            torchObject.transform.localPosition = Vector3.zero;
            torchObject.transform.localRotation = Quaternion.identity;
        }
    }
}
