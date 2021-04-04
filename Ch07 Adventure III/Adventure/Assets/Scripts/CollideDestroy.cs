using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDestroy : MonoBehaviour
{
	//When hit objects with associated tag, then destroy
	//public string TagCompare = string.Empty;
	public string TagCompare = "Player";

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag(TagCompare)) return;

		Destroy(gameObject);
	}
}
