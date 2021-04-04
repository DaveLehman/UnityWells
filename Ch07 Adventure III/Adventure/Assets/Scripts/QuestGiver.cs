using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
	//Human readable quest name
	public string QuestName = string.Empty;
	//Reference to UI Text Box
	public Text Captions = null;
	//List of strings to say
	public string[] CaptionText;

	void OnTriggerEnter2D(Collider2D other)
	{

		if (!other.CompareTag("Player"))
		{
			Debug.Log("trigger enter, but not by player");
			return;
		}
		Debug.Log("Player triggered NPC");
		Canvas convoCanvas = GameObject.Find("ConversationPanel").GetComponent<Canvas>();
		if(convoCanvas != null)
        {
			convoCanvas.enabled = true;
			Debug.Log("Canvas enabled");
		}
		else
        {
			Debug.Log("Failed to find conversation canvas");
        }
		Quest.QUESTSTATUS Status = QuestManager.GetQuestStatus(QuestName);
		Captions.text = CaptionText[(int)Status];
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("trigger exit");
		Canvas convoCanvas = GameObject.Find("ConversationPanel").GetComponent<Canvas>();
		if (convoCanvas != null)
		{
			convoCanvas.enabled = false;
			Debug.Log("Canvas disnabled");
		}
		else
		{
			Debug.Log("Failed to find conversation canvas");
		}
		Quest.QUESTSTATUS Status = QuestManager.GetQuestStatus(QuestName);
		if (Status == Quest.QUESTSTATUS.UNASSIGNED)
		{
			QuestManager.SetQuestStatus(QuestName, Quest.QUESTSTATUS.ASSIGNED);
			Captions.text = CaptionText[(int)Status];
		}
		else if (Status == Quest.QUESTSTATUS.COMPLETE)
		{
			//SceneManager.LoadScene(5);
		}

	}
}
