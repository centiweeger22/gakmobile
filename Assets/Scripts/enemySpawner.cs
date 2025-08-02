using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Vector3 range;
    [SerializeField] private float spawnTime;
    public int difficulty;
    [SerializeField] private int minimumDifficulty;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (minimumDifficulty <= difficulty)
            {
                Vector3 position = transform.position + new Vector3(
                Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(-range.z, range.z));
                int randomIndex = Random.Range(0, objects.Length);
                Instantiate(objects[randomIndex], position, Quaternion.identity);
                timer = spawnTime;
            }
        }
    }
}
