using UnityEngine;

public class Reward : MonoBehaviour
{
	[SerializeField] private TriggerState triggerState;
	[SerializeField] private TriggerName triggerName;
	
	public void DeactivateTrigger()
	{
		switch (triggerName)
		{
			case TriggerName.isGame1Cleared:
			triggerState.isGame1Cleared = true;
			break;
			
			case TriggerName.isGame2Cleared:
			triggerState.isGame2Cleared = true;
			break;
		}
	}
}