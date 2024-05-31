using UnityEngine;
using TMPro;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private float pickupRange = 2f; 
    [SerializeField] private Transform holdPoint; 
    [SerializeField] private float dropDistance = 2f; 
    [SerializeField] private float dropHeightOffset = 0.5f; 
    [SerializeField] private TextMeshProUGUI pickupText; 
    [SerializeField] private TextMeshProUGUI dropText; 
    public bool dropNormal = true;

    private GameObject heldObject;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickupText.enabled = false; 
        dropText.enabled = false; 
    }

    private void Update()
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

    private void LateUpdate()
    {
        if (heldObject != null)
        {
            HoldObject();
        }
    }

    private void ShowPickupText()
    {
        if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("Pickup"))
            {
                pickupText.enabled = true; 
                return;
            }
        }
        pickupText.enabled = false;
    }

    private void ShowDropText()
    {
        dropText.enabled = heldObject != null;
    }

    private void TryPickupObject()
    {
        if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("Pickup"))
            {
                PickupObject(hit.collider.gameObject);
                pickupText.enabled = false;
            }
        }
    }

    private void PickupObject(GameObject obj)
    {
        heldObject = obj;
        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
        }
        if (obj.TryGetComponent<Collider>(out var col))
        {
            col.enabled = false;
        }
        obj.transform.position = holdPoint.position;
        obj.transform.rotation = holdPoint.rotation;
        obj.transform.SetParent(holdPoint);
        dropText.enabled = true; 
    }

    private void HoldObject()
    {
        heldObject.transform.position = holdPoint.position;
        heldObject.transform.rotation = holdPoint.rotation;
    }

    public void DropObject(Vector3 dropPosition)
    {
        if (heldObject != null)
        {
            if (heldObject.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
            }
            if (heldObject.TryGetComponent<Collider>(out var col))
            {
                col.enabled = true;
            }

            heldObject.transform.position = dropPosition;
            heldObject.transform.rotation = Quaternion.identity; 
            heldObject.transform.SetParent(null);
            heldObject = null;
            dropText.enabled = false;
        }
    }

    private void DropObjectNormal()
    {
        if (heldObject != null)
        {
            if (heldObject.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
            }
            if (heldObject.TryGetComponent<Collider>(out var col))
            {
                col.enabled = true;
            }

            Vector3 dropPosition = transform.position + transform.forward * dropDistance;

            if (Physics.Raycast(dropPosition, Vector3.down, out RaycastHit hit))
            {
                dropPosition.y = hit.point.y + col.bounds.extents.y + dropHeightOffset; 
            }

            heldObject.transform.position = dropPosition;
            heldObject.transform.rotation = Quaternion.identity; 
            heldObject.transform.SetParent(null);
            heldObject = null;
            dropText.enabled = false;
        }
    }

    public GameObject GetHeldObject()
    {
        return heldObject;
    }
}
