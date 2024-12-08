using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Forward/backward speed
    public float turnSpeed = 100.0f; // Turning speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Robot Position: " + rb.position);
        GUI.Label(new Rect(10, 30, 300, 20), "Robot Speed: " + rb.linearVelocity.magnitude.ToString("F2"));
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        // Get input for movement and turning
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // Apply forward/backward force
        Vector3 forwardMove = transform.forward * move * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // Apply rotation
        Quaternion turnRotation = Quaternion.Euler(0, turn * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Debug information
        Debug.Log("Robot Position: " + rb.position); // Displays position
        Debug.Log("Robot Speed: " + rb.linearVelocity.magnitude); // Displays speed
    }
}
