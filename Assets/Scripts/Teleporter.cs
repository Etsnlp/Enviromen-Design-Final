using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform targetLocation; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Teleport(other);
        }
    }

    private void Teleport(Collider player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            MovePlayer(player);
            controller.enabled = true;
        }
        else
        {
            MovePlayer(player);
        }
    }

    private void MovePlayer(Collider player)
    {
        if (targetLocation != null)
        {
            player.transform.position = targetLocation.position;
            player.transform.rotation = targetLocation.rotation;
        }
        else
        {
            Debug.LogWarning("Target location is not set.");
        }
    }
}