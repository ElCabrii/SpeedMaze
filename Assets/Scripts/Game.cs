using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _mazePrefab;
    [SerializeField] private GameObject _PlayerPrefab;
    public static bool gamePaused = false;
    public static float targetTime = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject maze = new GameObject("Maze");
        Instantiate(_mazePrefab, Vector3.zero, Quaternion.identity, maze.transform);
        GameObject player = Instantiate(_PlayerPrefab);
        player.SetActive(false);
        player.transform.position = new Vector3(0, 1, 0);
        player.SetActive(true);
        GameObject.Find("PauseMenu").transform.localScale = new Vector3(0, 0, 0);

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || gamePaused)
        {
            gamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("PauseMenu").transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>().text = $"{(int)targetTime}s";
            targetTime -= Time.deltaTime;
            Debug.Log(targetTime);
            if ((int)targetTime < 0)
            {
                timerEnded();
            }

            if (GameObject.Find("Player(Clone)").transform.position.x > PlayerPrefs.GetInt("Difficulty") * 5 * 7 * 3)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(2);
            }
        }
    }

    void timerEnded()
    {
        Debug.Log("Timer ended. Teleporting player...");
        GameObject player = GameObject.Find("Player(Clone)");
        player.SetActive(false);
        player.transform.position = new Vector3(0, 1, 0);
        targetTime = 10.0f;
        player.SetActive(true);
    }
}
