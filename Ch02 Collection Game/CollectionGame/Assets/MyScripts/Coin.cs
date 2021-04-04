using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Keeps track of total coin count in scene
    public static int CoinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Coin Object created");
        ++Coin.CoinCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin Collision Reported");
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        --Coin.CoinCount;

        if(Coin.CoinCount <= 0)
        {
            GameObject Timer = GameObject.Find("LevelTimer");
            Destroy(Timer);
            GameObject[] FireworkSystems = GameObject.FindGameObjectsWithTag("Fireworks");
            if (FireworkSystems.Length <= 0)
                return;
            foreach (GameObject GO in FireworkSystems)
                GO.GetComponent<ParticleSystem>().Play();

        }
    }
}
