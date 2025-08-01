using UnityEngine;

public class dissapear : MonoBehaviour
{
    [SerializeField] private float killTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killTimer -= Time.deltaTime;
        if (killTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
