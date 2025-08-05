using UnityEngine;

public class cameraRay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int playerLayer = LayerMask.NameToLayer("Pan");
        LayerMask playerMask = 1 << playerLayer;
        transform.localPosition = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 38.5f,playerMask))
        {
            transform.position = hit.point + transform.TransformDirection(Vector3.forward)*2;
        }
        else
        {
            transform.position += transform.TransformDirection(-Vector3.forward)*38.5f;
        }
    }
}
