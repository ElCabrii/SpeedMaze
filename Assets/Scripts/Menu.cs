using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Game.gamePaused = false;
        Game.targetTime = 10.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void IncreaseDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty") < 3){
            PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty") + 1);
        } else {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
    }

    public void DecreaseDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty") > 1){
           PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty") - 1); 
        } else {
            PlayerPrefs.SetInt("Difficulty", 3);
        }
    }

    public void UpdateDifficultyButtonText()
    {
        TMP_Text difficultyText = GameObject.Find("DifficultyText").GetComponent<TMP_Text>();
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 1:
                difficultyText.text = "Easy";
                break;
            case 2:
                difficultyText.text = "Medium";
                break;
            case 3:
                difficultyText.text = "Hard";
                break;
        }
    }

    public void DisplayRules()
    {   
        if (GameObject.Find("Rules").transform.localScale == new Vector3(1, 1, 1)){
            GameObject.Find("Rules").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("MainMenu").transform.localScale = new Vector3(1, 1, 1);
            return;
        } else {
            GameObject.Find("Rules").transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("MainMenu").transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void HidePauseMenu()
    {
        GameObject.Find("PauseMenu").transform.localScale = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Game.gamePaused = false;
    }
}
