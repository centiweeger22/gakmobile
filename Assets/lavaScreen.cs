using UnityEngine;

public class lavaScreen : MonoBehaviour
{
    [SerializeField] private GameObject lavaObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lavaObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Lava")
            lavaObject.SetActive(true);
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Lava")
            lavaObject.SetActive(false);
    }
}
