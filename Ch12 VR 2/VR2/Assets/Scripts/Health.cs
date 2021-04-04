using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnHealthChanged;
    public string SpawnPoolTag = string.Empty;

    public float HealthPoints
    {
        // changes object Health and potentially to notify other objects and process about the event
        get { return _HealthPoints; }
        set { _HealthPoints = value;
            OnHealthChanged?.Invoke();
            /*
             * the above syntax is equivalent to 
             *      if (OnHealthChanged != null)
             *          OnHealthChanged.Invoke();
             */
            if (_HealthPoints <= 0f)
            {
                Die();
            }
        }
    }
    [SerializeField] private float _HealthPoints = 100f;
    // allows the Health component to link with the Object Pool so that if the object is dying it can be returned to the object pool rather than being removed entirely
    private ObjectPool Pool = null;

    void Awake()
    {
        if (SpawnPoolTag.Length > 0)
        {
            Pool = GameObject.FindWithTag(SpawnPoolTag).GetComponent<ObjectPool>();
        }
    }

    private void Die()
    {
        if (Pool != null)
        {
            Pool.DeSpawn(transform);
            HealthPoints = 100f;
            // seems odd to set a dying object to 100 health, but it's going back to the pool, and may be respawned alter
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthPoints = 0;
        }
    }
}
