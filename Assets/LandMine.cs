using UnityEngine;
using System.Collections;

public class LandMine : MonoBehaviour
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
            playerCar.DamagePlayer(15f, false, 10f);
            for (int i = 0; i < 10; i++)
            {
                GameObject g = Instantiate(explosionParticle, other.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
