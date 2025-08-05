using Unity.VisualScripting;
using UnityEngine;

public class speedLinesSpawner : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] private float rate;
    [SerializeField] private float radius;
    [SerializeField] private GameObject speedLine;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (movementSpeed > 1)
        {
            if (timer < 0)
            {
                timer = 1 / rate;
                float angle = Random.Range(-Mathf.PI, Mathf.PI);
                Instantiate(speedLine, transform.position+new Vector3(-Mathf.Sin(angle)*Screen.width*radius,Mathf.Cos(angle)*Screen.height*radius,0),Quaternion.AngleAxis(angle*Mathf.Rad2Deg+180,Vector3.forward),transform);
            }
        }
    }
}
