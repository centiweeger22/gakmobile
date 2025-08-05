using UnityEngine;

public class meteor : MonoBehaviour
{
    [SerializeField] private float meteorSpeed;
    [SerializeField] private bool meteorLanded;
    private rotate rotation;
    private car player;
    private AudioSource source;
    [SerializeField] private AudioClip landSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotation = GetComponent<rotate>();
        player = GameObject.Find("Player").GetComponent<car>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!meteorLanded)
        {
            transform.position += new Vector3(0, -meteorSpeed * Time.deltaTime, 0);
            RaycastHit hit;
            int playerLayer = LayerMask.NameToLayer("Pan");
            LayerMask playerMask = 1 << playerLayer;
            if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.down), out hit, 4, playerMask))
            {
                meteorLanded = true;
                rotation.enabled = false;
                player.ShakeCamera(10);
                source.PlayOneShot(landSound,0.4f);
            }
            if (Physics.Raycast(transform.position, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized, out hit, 4, playerMask))
            {
                meteorLanded = true;
                rotation.enabled = false;
                player.ShakeCamera(10);
                source.PlayOneShot(landSound,0.4f);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (!meteorLanded)
        {
            if (other.gameObject.tag == "GROUND")
            {
                meteorLanded = true;
                rotation.enabled = false;
                player.ShakeCamera(10);
                source.PlayOneShot(landSound,0.4f);
            }   
        }
    }
}
