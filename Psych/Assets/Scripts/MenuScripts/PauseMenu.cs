using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu pauseMenuRef;
    public GameObject pauseMenuBackground;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public GameObject crosshair;
    public Image crosshairImage;
    public Image weaponIconDisplay;

    public bool isPaused = false;

    PlayerMovement playerMovement;

    void Awake()
    {
        if (pauseMenuRef == null)
            pauseMenuRef = this;
    }
    private void OnEnable()
    {
        if (pauseMenuRef == null)
            pauseMenuRef = this;
    }


    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        if (crosshair != null)
        {
            crosshairImage = crosshair.GetComponent<Image>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenuBackground.SetActive(true);
                playerMovement.enabled = false;
                //CameraManager.cameraManager.cameraController.enabled = false;
                crosshair.SetActive(false);
            }
            else
            {
                settingsMenu.SetActive(false);
                ResumeButon();
                Time.timeScale = 1;
                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;
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
        //CameraManager.cameraManager.cameraController.enabled = true;
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
