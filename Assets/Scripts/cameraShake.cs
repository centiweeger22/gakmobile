using UnityEngine;

public class cameraShake : MonoBehaviour
{
    [SerializeField] private float shakiness;
    [SerializeField] private float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(Random.Range(-shakiness, shakiness)+17.535f, Random.Range(-shakiness, shakiness)+offset, 0);
    }
    public void Shake(float amount)
    {
        shakiness += amount;
    }
    void FixedUpdate()
    {
        shakiness *= 0.8f;
    }
}
