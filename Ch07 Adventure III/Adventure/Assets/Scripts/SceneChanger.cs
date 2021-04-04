using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	[SerializeField] public string SceneToLoad = null;
	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("SceneChanger triggered, checking for Player");
		//If not player entered, then exit
		if (!other.gameObject.CompareTag("Player")) return;

		//Lock player controls
		PlayerControl.PlayerInstance.CanControl = false;
		Debug.Log("OneToThree, Player controls locked");
		SceneManager.LoadScene(SceneToLoad);
	}


}
