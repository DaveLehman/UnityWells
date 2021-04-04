using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{

    // Purpose is to let each explosion destroy itself after a while. Otherwise they just keep ahnging around
    public float DestroyTime = 2F;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Explosion is doomed in 2 seconds");
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
