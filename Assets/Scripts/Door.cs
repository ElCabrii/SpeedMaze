using UnityEngine;

public class Door : MonoBehaviour
{

    private Renderer _renderer;
    private bool isActivated = false;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.Find("Player(Clone)").transform.position, transform.position);
        if (isActivated)
        {
            return;
        } else {
            if (distance < 1.2f)
            {
                isActivated = true;
                _renderer.material.color = Color.green;
                Game.targetTime += 10.0f;                
            }
        }   
    }
}