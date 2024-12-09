using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour
{
    public Terrain terrain; // Assign your terrain in the Inspector
    public float offsetAboveTerrain = 0.5f; // Offset to ensure the robot starts above the terrain
    public float moveSpeed = 5.0f; // Forward/backward speed
    public float turnSpeed = 100.0f; // Turning speed
    private Rigidbody rb;

    private float pitch; // Rotation around X-axis
    private float yaw;   // Rotation around Y-axis
    private float roll;  // Rotation around Z-axis

    public float stabilityThreshold = 45.0f; // Threshold for instability in degrees
    private bool isFlipped = false; // Tracks if the robot is flipped
    private bool isSelfCorrected = false; // Tracks if the robot has self-corrected

    private Vector3 initialPosition; // Stores the initial position of the robot
    private Quaternion initialRotation; // Stores the initial rotation of the robot

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Adjust the robot's starting position above the terrain
        AdjustStartPositionAboveTerrain();

        // Save initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void AdjustStartPositionAboveTerrain()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned! Please assign the terrain in the Inspector.");
            return;
        }

        // Get the current position
        Vector3 robotPosition = transform.position;

        // Get the terrain height at the robot's X and Z position
        float terrainHeight = terrain.SampleHeight(robotPosition);

        // Adjust the robot's Y position to be above the terrain
        transform.position = new Vector3(robotPosition.x, terrainHeight + offsetAboveTerrain, robotPosition.z);

        Debug.Log($"Robot positioned above terrain at: {transform.position}");
    }

    void FixedUpdate()
    {
        // Calculate pitch, yaw, and roll
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
        roll = transform.eulerAngles.z;

        // Correct pitch and roll values to range [-180, 180]
        if (pitch > 180) pitch -= 360;
        if (roll > 180) roll -= 360;

        // If the robot is flipped, check if it has stabilized
        if (isFlipped)
        {
            if (IsStable())
            {
                isFlipped = false; // Unlock controls
                isSelfCorrected = true; // Show corrected message
                Debug.Log("Robot has self-corrected!");
                StartCoroutine(AutoCloseCorrectionMessage()); // Start auto-close timer
            }
            return; // Stop further movement while flipped
        }

        // Check for instability
        if (Mathf.Abs(pitch) > stabilityThreshold || Mathf.Abs(roll) > stabilityThreshold)
        {
            isFlipped = true; // Mark the robot as flipped
            Debug.Log("Robot has flipped! Awaiting reset or correction.");
            return;
        }

        // Get input for movement and turning
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // Apply movement and rotation
        Vector3 forwardMove = transform.forward * move * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);

        Quaternion turnRotation = Quaternion.Euler(0, turn * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void OnGUI()
    {
        // Display robot information
        GUI.Label(new Rect(10, 10, 300, 20), "Robot Position: " + rb.position.ToString("F2"));
        GUI.Label(new Rect(10, 30, 300, 20), "Robot Speed: " + rb.linearVelocity.magnitude.ToString("F2"));
        GUI.Label(new Rect(10, 50, 300, 20), "Robot Pitch: " + pitch.ToString("F2") + "°");
        GUI.Label(new Rect(10, 70, 300, 20), "Robot Yaw: " + yaw.ToString("F2") + "°");
        GUI.Label(new Rect(10, 90, 300, 20), "Robot Roll: " + roll.ToString("F2") + "°");

        // Display reset prompt if the robot is flipped
        if (isFlipped)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), "Robot Flipped!");
            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2, 150, 40), "Restart Simulation"))
            {
                ResetRobot();
            }
        }

        // Display correction message if the robot self-corrected
        if (isSelfCorrected)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), "Robot has corrected itself!");
        }
    }

    public void ResetRobot()
    {
        // Reset the robot's position and rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.linearVelocity = Vector3.zero; // Reset velocity
        rb.angularVelocity = Vector3.zero; // Reset angular velocity
        isFlipped = false; // Allow controls again
        isSelfCorrected = false; // Clear correction message
        Debug.Log("Simulation restarted.");
    }

    bool IsStable()
    {
        // Check if pitch and roll are within stability bounds
        return Mathf.Abs(pitch) < stabilityThreshold - 5 && Mathf.Abs(roll) < stabilityThreshold - 5;
    }

    IEnumerator AutoCloseCorrectionMessage()
    {
        yield return new WaitForSeconds(10); // Wait for 10 seconds
        isSelfCorrected = false; // Auto-close correction message
        Debug.Log("Correction message auto-closed.");
    }
}
