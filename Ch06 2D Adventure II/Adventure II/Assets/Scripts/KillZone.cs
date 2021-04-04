using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    /* Damages the player's health for as long as he is in the kill zone box collider
     * */
    public float Damage = 1f;
    private void OnTriggerStay2D(Collider2D other)
    {
        // called by Unity once per frame when an object with a Rigidbody enters and remains within a trigger volumne
        // make sure we're the player
        if (!other.CompareTag("Player")) 
            return;
        if(PlayerController.PlayerInstance != null )
        {
            PlayerController.Health -= Damage * Time.deltaTime;
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
