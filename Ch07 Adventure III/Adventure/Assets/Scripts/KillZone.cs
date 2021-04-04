using UnityEngine;

public class KillZone : MonoBehaviour 
{
	//Amount to damage player per second
	public float Damage = 100f;

	void OnTriggerStay2D(Collider2D other)
	{
		//If not player then exit
		if(!other.CompareTag("Player"))return;

		//Damage player by rate
		if (PlayerControl.PlayerInstance != null)
		{
			Debug.Log("KillZone Damaging player");
			PlayerControl.Health -= Damage * Time.deltaTime;
		}
	}
}