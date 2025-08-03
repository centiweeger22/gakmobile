using UnityEngine;

public class annoyingBall : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField] private float acceleration;
    [SerializeField] private float distance;
    private Rigidbody rb;
    private float deathTimer = 14.9f;
    private GameObject myDeathParticle;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private int explosionCount;
    [SerializeField] private AudioClip[] sounds;
    private AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.Find("Player");
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 15f);
        source.clip = sounds[0];
        source.Play();
    }
    void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer < 0)
        {
            // myDeathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
            deathTimer = 1000;
            // rb.isKinematic = true;
            // rb.useGravity = false;
            for (int i = 0;i<explosionCount;i++)
            {
                GameObject g = Instantiate(deathParticle, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerObject != null)
        {
            if ((playerObject.transform.position - transform.position).magnitude < distance)
            {
                rb.AddForce(Vector3.Scale((playerObject.transform.position - transform.position).normalized, new Vector3(1, 0, 1)) * acceleration);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            car playerCar = collision.gameObject.GetComponent<car>();
            playerCar.DamagePlayer(10, true, 15);
            source.PlayOneShot(sounds[1],5);
        }
    }
}
