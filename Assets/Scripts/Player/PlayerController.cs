using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float mouseSensitivity = 100.0f;

    private CharacterController characterController;
    private Transform cameraTransform;
    private float verticalVelocity;
    private const float gravity = 9.81f;
    private float pitch = 0.0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        LookAround();
    }

    private void Move()
    {
        float moveDirectionX = Input.GetAxis("Horizontal") * speed;
        float moveDirectionZ = Input.GetAxis("Vertical") * speed;

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        if (characterController.isGrounded)
        {
            verticalVelocity = -2f; // Ensures the player stays grounded
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}