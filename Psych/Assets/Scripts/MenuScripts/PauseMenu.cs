using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuBackground;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public GameObject crosshair;

    public bool isPaused = false;

    PlayerMovement playerMovement;
    CameraController weaponCam;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        weaponCam = CameraManager.cameraManager.cameraController;
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                isPaused = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenuBackground.SetActive(true);
                playerMovement.enabled = false;
                weaponCam.enabled = false;
                crosshair.SetActive(false);
            }
            else
            {
                isPaused = false;
                settingsMenu.SetActive(false);
                ResumeButon();
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void ResumeButon()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuBackground.SetActive(false);
        playerMovement.enabled = true;
        weaponCam.enabled = true;
        crosshair.SetActive(true);
    }

    public void OpenSettingsButton()
    {
        isPaused = true;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseSettingsButton()
    {
        isPaused = true;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
