using UnityEngine;
using System.Collections;

public class painBlock : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private car playerCar;
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
            playerCar = other.gameObject.GetComponent<car>();
            playerCar.DamagePlayer(0.5f, false, 0.01f);
            // playerCar.gameObject.transform.position += new Vector3(0, 1, 0);
            Rigidbody rb = playerCar.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(new Vector3(0, 1, 0));
            if (Random.Range(0,100)>80)
            {
                GameObject g = Instantiate(explosionParticle, other.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject g = Instantiate(explosionParticle, other.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
            Destroy(other.gameObject);
            playerCar.ShakeCamera(2);
        }
    }
}
