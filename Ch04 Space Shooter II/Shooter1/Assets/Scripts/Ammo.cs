using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Primary purpose of Ammo class is to damage any objects it collides with
    public float Damage = 100F;
    public float LifeTime = 2F;

    void OnEnable()
    {
        // Disables the Ammo object after the LifeTime interval this function is called each time an ibject is changed from disabled to enabled
        CancelInvoke();
        Invoke("Die", LifeTime);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Health H = other.gameObject.GetComponent<Health>();
        if (H == null)
        {
            return;
        }
        else
        {
            H.HealthPoints -= Damage;
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
