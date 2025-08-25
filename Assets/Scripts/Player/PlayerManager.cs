using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;

    public GameObject PauseMenu;
    public GameObject gameOverScreen;

    public Text scoreText;
    public static int score;

    public Text ResultScore;
    public static int result;


    private void Awake()
    {
        isGameOver = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        score = 0;
        scoreText.text = score + "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        scoreText.text = score + "";
        ResultScore.text = "Рахунок: " + result;
    }


    public void ContinueGame()
    {
        PauseMenu.SetActive(false); 
        Time.timeScale = 1; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
