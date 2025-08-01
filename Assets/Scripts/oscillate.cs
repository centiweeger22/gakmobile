using UnityEngine;

public class oscillate : MonoBehaviour
{
    [SerializeField] private float magnitude;
    [SerializeField] private float cycleTime;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector3(0,Mathf.Sin(timer*Mathf.PI*2/cycleTime)*magnitude+verticalOffset,0);
    }
}
