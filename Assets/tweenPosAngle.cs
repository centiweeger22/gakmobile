using UnityEngine;

public class tweenPosAngle : MonoBehaviour
{
    [SerializeField] private float newRadius;
    [SerializeField] private float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x + Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad-Mathf.PI*0.5f) * newRadius, transform.position.y + Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad-Mathf.PI*0.5f) * newRadius, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*Speed);
    }
}
