using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public Quest[] Quests;

	private static QuestManager ThisInstance = null;

	void Awake()
	{
		// Ensures that the object that QuestManager is attached to persists between Scenes, and it makes sure there
		// is only one instance of the script in a scene.
		if (ThisInstance == null)
		{
			// tells Unity to keep this object alive when transitioning Scenes. Can cause issues if not careful...
			/*
			 * Say we're in Scene 1 and then we go to Scene 2. So far so good. But what happens if we transition back to scene 1?
			 * The object is created all over again. If we ping pong between scenes we could have several instances of this script running.
			 * That's where the ThisInstance variable comes in. By storing a static reference to an instance of QuestManager,
			 * it is shared between all instances of a class. If ThisInstance is not null, then we know there's alread a copy of this class running
			 * and we can safely destroy this one, preventing more than one from running. */
			DontDestroyOnLoad(this);
			ThisInstance = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	public static Quest.QUESTSTATUS GetQuestStatus(string QuestName)
	{
		foreach (Quest Q in ThisInstance.Quests)
		{
			if (Q.QuestName.Equals(QuestName))
			{
				return Q.Status;
			}
		}

		return Quest.QUESTSTATUS.UNASSIGNED;
	}

	public static void SetQuestStatus(string QuestName, Quest.QUESTSTATUS NewStatus)
	{
		foreach (Quest Q in ThisInstance.Quests)
		{
			if (Q.QuestName.Equals(QuestName))
			{
				Q.Status = NewStatus;
				return;
			}
		}
	}

	public static void Reset()
	{
		if (ThisInstance == null) return;

		foreach (Quest Q in ThisInstance.Quests)
		{
			Q.Status = Quest.QUESTSTATUS.UNASSIGNED;
		}

	}
}
