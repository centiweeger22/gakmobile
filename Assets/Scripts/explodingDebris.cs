using UnityEngine;

public class explodingDebris : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float rotationForce;
    private Rigidbody rb;
    [SerializeField] private float collisionTimer;
    private Collider coll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 directionVector = new Vector3(
            Random.Range(-1, 1),
            Random.Range(-1, 1),
            Random.Range(-1, 1)
            );
        directionVector.Normalize();
        directionVector *= Random.Range(0, force);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(
            Random.Range(-force, force),
            Random.Range(-force, force),
            Random.Range(-force, force)));
        rb.AddTorque(new Vector3(
            Random.Range(-rotationForce, rotationForce),
            Random.Range(-rotationForce, rotationForce),
            Random.Range(-rotationForce, rotationForce)));
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        collisionTimer -= Time.deltaTime;
        if (collisionTimer < 0)
        {
            coll.enabled = true;
        }
    }
}
