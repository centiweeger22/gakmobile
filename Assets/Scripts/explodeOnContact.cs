using Unity.VisualScripting;
using UnityEngine;

public class explodeOnContact : MonoBehaviour
{
    public float explosionPower;
    public bool hitPlayer;
    public float killTime;
    public bool killObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (killObject)
        {
            Destroy(gameObject, killTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        Rigidbody rb;
        if (other.gameObject.TryGetComponent(out rb))
        {
            if (other.gameObject.tag != "Player" || hitPlayer)
            {
                rb.AddExplosionForce(explosionPower, transform.position, 10, 3);
                // Destroy(other.gameObject);
            }
        }
    }
}
