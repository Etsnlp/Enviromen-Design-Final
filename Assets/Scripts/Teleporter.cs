using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetLocation; 

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false; 
                other.transform.position = targetLocation.position;
                controller.enabled = true; 
            }
            else
            {
                other.transform.position = targetLocation.position;
            }

            other.transform.rotation = targetLocation.rotation;
        }
    }
}
