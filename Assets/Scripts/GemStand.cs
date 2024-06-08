using UnityEngine;
using TMPro;

public class GemStand : MonoBehaviour
{
    [SerializeField] private Transform[] gemPositions; 
    [SerializeField] private GameObject exitDoor; 

    private int currentGemCount = 0; 
    private bool isPlayerNear = false; 
    private ObjectPickup objectPickup; 

    private void Start()
    {
        exitDoor.SetActive(false); 

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            objectPickup = player.GetComponent<ObjectPickup>();
            if (objectPickup == null)
            {
                Debug.LogError("ObjectPickup component is not found on the player.");
            }
        }
        else
        {
            Debug.LogError("Player object is not found.");
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && objectPickup != null && objectPickup.GetHeldObject() != null)
        {
            PlaceGem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (objectPickup != null)
            {
                objectPickup.dropNormal = false;
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (objectPickup != null)
            {
                objectPickup.dropNormal = true;
            }
        }
    }

    private void PlaceGem()
    {
        if (currentGemCount < gemPositions.Length && objectPickup != null)
        {
            GameObject gem = objectPickup.GetHeldObject();
            if (gem != null)
            {
                gem.transform.position = gemPositions[currentGemCount].position;
                gem.transform.rotation = gemPositions[currentGemCount].rotation;
                gem.transform.SetParent(gemPositions[currentGemCount]);
                if (gem.TryGetComponent<Collider>(out var col))
                {
                    col.enabled = false; 
                }
                objectPickup.DropObject(gemPositions[currentGemCount].position);
                currentGemCount++;

                if (currentGemCount >= gemPositions.Length)
                {
                    ActivateExitDoor();
                }
            }
        }
    }

    private void ActivateExitDoor()
    {
        exitDoor.SetActive(true); 
    }
}
