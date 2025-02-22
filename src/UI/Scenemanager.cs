using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public GameObject gameOverUI, gamePauseUI;
    [SerializeField] Animator transitionAnim;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // Ensure game starts unpaused
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverUI.activeInHierarchy && !gamePauseUI.activeInHierarchy)
        {
            pause();
        }

        if (gameOverUI.activeInHierarchy || gamePauseUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        Debug.Log("Restarting...");
        Time.timeScale = 1f; // Unpause the game before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f; // Ensure normal time before quitting
        SceneManager.LoadSceneAsync(0);
    }

    public void Resume()
    {
        gamePauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pause()
    {
        Time.timeScale = 0f;
        gamePauseUI.SetActive(true);
    }

    public void nextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSecondsRealtime(0.5f); // Ensures animation plays even when paused
        Time.timeScale = 1f; // Reset before loading new level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }
}
