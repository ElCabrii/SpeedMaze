using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _mazePrefab;
    [SerializeField] private GameObject _PlayerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject maze = new GameObject("Maze");
        Instantiate(_mazePrefab, Vector3.zero, Quaternion.identity, maze.transform);
        GameObject player = Instantiate(_PlayerPrefab);
        player.transform.position = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
