using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + rotation.x * Time.deltaTime, transform.eulerAngles.y + rotation.y * Time.deltaTime, transform.eulerAngles.z + rotation.z * Time.deltaTime); 
    }
}
