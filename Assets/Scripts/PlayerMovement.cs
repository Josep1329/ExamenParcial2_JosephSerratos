using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    
    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move forward/backward
        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Rotate left/right
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotation);
    }
}
