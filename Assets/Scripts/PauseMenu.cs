using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; 
    
    
    bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResumeGame()
    {
        //Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void PauseGame()
    {
        //Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    public void GoToMainMenu()
    {
        
    }

    public void QuitGame()
    {
        
    }

    public void GoToSettings()
    {
        
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        Debug.Log(isPaused);
        if (isPaused) PauseGame(); else ResumeGame();
    }
}
