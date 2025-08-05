using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(sound);
        Destroy(gameObject, sound.length + 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
