using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // TO be atached to Player and any enemies requiring a health rating
    public GameObject DeathParticlesPrefab = null;
    public bool ShouldDestroyOnDeath = true;

    // SerializeField allows this field to be visible in the Inspector while maintaining private scope
    [SerializeField] private float _HealthPoints = 100f;

    public float HealthPoints
    {
        get
        {
            return _HealthPoints;
        }

        set
        {
            // Event-driven: the set mutator is the only place where health will ever change
            _HealthPoints = value;
            if (HealthPoints <= 0)
            {
                // SendMessage allows you to send any other function on any other component by specifying the function name as a string.
                // A function called Die() will be run on any component attached to this object
                SendMessage("Die", SendMessageOptions.DontRequireReceiver);
                if (DeathParticlesPrefab != null)
                {
                    Instantiate(DeathParticlesPrefab, transform.position, transform.rotation);
                }
                if (ShouldDestroyOnDeath)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HealthPoints = 0;
        }
    }
}
