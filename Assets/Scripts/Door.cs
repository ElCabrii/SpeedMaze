using UnityEngine;

public class Door : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeColor(Color.green);
        }
    }

    private void ChangeColor(Color color)
    {
        if (_renderer != null)
        {
            for (int i = 0; i < _renderer.GetComponentsInChildren<Renderer>().Length; i++)
            {
                _renderer.GetComponentsInChildren<Renderer>()[i].material.color = color;
            }
        }
    }
}
