using UnityEngine;

public class spawnCar : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float horizontalSpacing;
    [SerializeField] private float verticalSpacing;
    [SerializeField] private GameObject thing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Instantiate(thing, transform.position + new Vector3(x * horizontalSpacing, y * verticalSpacing, 0),Quaternion.Euler(Random.Range(-180,180),Random.Range(-180,180),Random.Range(-180,180)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
