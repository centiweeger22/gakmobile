using UnityEngine;

public class loopDirection : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private loopsController lc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player entered the trigger!");
            lc.registerLoop(direction);
        }
    }
}
