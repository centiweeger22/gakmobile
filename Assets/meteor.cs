using UnityEngine;

public class meteor : MonoBehaviour
{
    [SerializeField] private float meteorSpeed;
    [SerializeField] private bool meteorLanded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!meteorLanded)
        {
            transform.position += new Vector3(0, -meteorSpeed*Time.deltaTime, 0);
            RaycastHit hit;
            int playerLayer = LayerMask.NameToLayer("Pan");
            LayerMask playerMask = 1 << playerLayer;
            if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.down), out hit, 1, playerLayer))
            {
                meteorLanded = true;
            }
        }
    }
}
