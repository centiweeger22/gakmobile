using UnityEngine;

public class pointAtCamera : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraObject.transform.position, Vector3.up);
        transform.rotation = transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
    }
}
