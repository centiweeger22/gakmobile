using UnityEngine;
using System.Collections;

public class LandMine : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private car playerCar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int playerLayer = LayerMask.NameToLayer("Pan");
        LayerMask playerMask = 1 << playerLayer;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, playerMask))
        {
            transform.position = hit.point+hit.normal*0.4f;
            transform.rotation = Quaternion.LookRotation(hit.normal)*Quaternion.AngleAxis(90,Vector3.right);
        }
        else
        {
            Destroy(gameObject);
        }
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
            playerCar.DamagePlayer(20f, true, 100f);
            for (int i = 0; i < 10; i++)
            {
                GameObject g = Instantiate(explosionParticle, other.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
