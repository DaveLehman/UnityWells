using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private ParticleSystem PS;

    // Start is called before the first frame update
    void Awake()
    {
        PS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || (Input.GetMouseButtonDown(0)) )
        {
            Debug.Log("Firing");
            PS.Play();
        }
        else if (Input.GetButtonUp("Fire1") || !(Input.GetMouseButtonDown(0)))
        {
            PS.Stop();
        }
    }
}
