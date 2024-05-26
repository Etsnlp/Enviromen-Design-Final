using UnityEngine;
using TMPro;

public class ObjectPickup : MonoBehaviour
{
    public float pickupRange = 2f; 
    public Transform holdPoint; 
    public float dropDistance = 2f; 
    public float dropHeightOffset = 0.5f; 

    public TextMeshProUGUI pickupText; 
    public TextMeshProUGUI dropText; 
    
    private GameObject heldObject;
    private GameObject player;

    public bool dropNormal = true;

    private GemStand gemStand;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickupText.enabled = false; 
        dropText.enabled = false; 

        
    }

    void Update()
    {
        if (heldObject == null)
        {
            ShowPickupText();
        }
        else
        {
            ShowDropText();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                if (dropNormal)
                {
                    DropObjectNormal(); 
                }
            }
        }
    }

    void LateUpdate()
    {
        if (heldObject != null)
        {
            HoldObject();
        }
    }

    void ShowPickupText()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, pickupRange))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Pickup"))
            {
                pickupText.enabled = true; 
            }
            else
            {
                pickupText.enabled = false; 
            }
        }
        else
        {
            pickupText.enabled = false; 
        }
    }

    void ShowDropText()
    {
        dropText.enabled = heldObject != null;
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, pickupRange))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Pickup"))
            {
                PickupObject(hit.collider.gameObject);
                pickupText.enabled = false;
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        heldObject = obj;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        Collider col = obj.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
        obj.transform.position = holdPoint.position;
        obj.transform.rotation = holdPoint.rotation;
        obj.transform.parent = holdPoint;
        dropText.enabled = true; 
    }

    void HoldObject()
    {
        heldObject.transform.position = holdPoint.position;
        heldObject.transform.rotation = holdPoint.rotation;
    }

    public void DropObject(Vector3 dropPosition)
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            Collider col = heldObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
            }

            heldObject.transform.position = dropPosition;
            heldObject.transform.rotation = Quaternion.identity; 
            heldObject.transform.parent = null;
            heldObject = null;
            dropText.enabled = false;
        }
    }

    void DropObjectNormal()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            Collider col = heldObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
            }

            Vector3 dropPosition = transform.position + transform.forward * dropDistance;

            RaycastHit hit;
            if (Physics.Raycast(dropPosition, Vector3.down, out hit))
            {
                dropPosition.y = hit.point.y + col.bounds.extents.y + 1f; 
            }

            heldObject.transform.position = dropPosition;
            heldObject.transform.rotation = Quaternion.identity; 
            heldObject.transform.parent = null;
            heldObject = null;
            dropText.enabled = false;
        }
    }

    public GameObject GetHeldObject()
    {
        return heldObject;
    }
}
