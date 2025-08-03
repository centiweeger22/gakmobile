using UnityEngine;

public class goInCircle : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 angleOffset;
    [SerializeField] private Vector3 positionOffset;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector3(Mathf.Sin(timer * speed * Mathf.PI), 0, Mathf.Cos(timer * speed * Mathf.PI)) * radius;
        transform.LookAt(Vector3.zero);
        transform.eulerAngles += angleOffset;
        transform.position += positionOffset;
    }
}
