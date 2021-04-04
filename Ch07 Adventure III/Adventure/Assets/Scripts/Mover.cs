using UnityEngine;

public class Mover : MonoBehaviour 
{
	public float Speed = 5f;

	void Update () 
	{
		//Update object position
		// THIS DEFAULT DIRECTION IS Z!!! transfor.forward is Z, transform.right is X, transform.up is Y
		//transform.position += transform.forward * Speed * Time.deltaTime;
		transform.position += transform.right * Speed * Time.deltaTime;
	}
}