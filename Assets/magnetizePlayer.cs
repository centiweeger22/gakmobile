using UnityEngine;

public class magnetizePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody rb;
            if (other.gameObject.TryGetComponent(out rb))
            {
                rb.isKinematic = false;
                rb.useGravity = false;
                rb.AddForce(-80 * (other.gameObject.transform.position - transform.position));
                rb.linearVelocity *= 0.94f;
                Debug.Log("Player Entered Portal!");
                // other.gameObject.transform.position += -0.5f * (other.gameObject.transform.position - transform.position).normalized;
                if ((transform.position - other.gameObject.transform.position).magnitude < 2)
                {
                    rb.position = transform.position;
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    other.gameObject.GetComponent<car>().floorVelocity = Vector2.zero;
                    other.gameObject.GetComponent<car>().playerPortal = GetComponent<Portal>();
                }
            }
        }
    }
}
