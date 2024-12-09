using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas menuCanvas; // Assign the menu Canvas in the Inspector
    public Button startButton; // Assign the Start button in the Inspector
    public Text titleText; // Assign the title text in the Inspector

    public RandomTerrainGenerator terrainGenerator; // Assign your terrain generator script in the Inspector
    public RobotController robotController; // Assign your robot controller script in the Inspector

    private bool isMenuActive = true; // Tracks menu state

    void Start()
    {
        // Ensure the menu is active at the start
        ShowMenu();

        // Add listener to Start button
        startButton.onClick.AddListener(StartSimulation);
    }

    void Update()
    {
        // Toggle menu on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuActive)
                ResumeSimulation();
            else
                ShowMenu();
        }
    }

    public void ShowMenu()
    {
        isMenuActive = true;
        menuCanvas.gameObject.SetActive(true);
        Time.timeScale = 0; // Freeze simulation
    }

    public void StartSimulation()
    {
        isMenuActive = false;
        menuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1; // Resume simulation

        // Refresh terrain and reset robot
        terrainGenerator.GenerateTerrain();
        robotController.ResetRobot();
    }

    public void ResumeSimulation()
    {
        isMenuActive = false;
        menuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1; // Resume simulation
    }
}
