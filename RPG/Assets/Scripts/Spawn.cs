using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Skeleton;
    private float spawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= 5)
        {
            Instantiate(Skeleton, new Vector3(transform.position.x, transform.position.y, -0.08333156f), transform.rotation);
            spawnTime = 0;
        }
    }
}
