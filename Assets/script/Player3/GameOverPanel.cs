using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject replayGame;
    public StaminaWheel staminaWheel;
    public void ReplayGame()
    {
        Time.timeScale = 2f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        staminaWheel.ResetStamina();
        
    }

    public void Lanjut()
    {
        replayGame.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Keluar(int SceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("game"); // SceneID digantikan dengan nama scene yang ingin kamu load
    }
}
