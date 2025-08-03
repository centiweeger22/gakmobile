using UnityEngine;

public class GoToOffset : MonoBehaviour
{
    [SerializeField] private GameObject other;
    [SerializeField] private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = other.transform.position + offset;
    }
}
