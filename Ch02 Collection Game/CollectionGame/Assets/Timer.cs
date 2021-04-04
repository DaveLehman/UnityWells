using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Time to complete in seconds
    public float MaxTime = 60f;

    [SerializeField] private float CountDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        CountDown = MaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce time
        CountDown -= Time.deltaTime;

        //Restart level if time runs out
        if (CountDown <= 0)
        {
            Coin.CoinCount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        
    }
}
