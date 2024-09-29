using UnityEngine;

[CreateAssetMenu(fileName = "TriggerState", menuName = "ScriptableObjects/TriggerState", order = 1)]
public class TriggerState : ScriptableObject
{
	public bool isGame1Cleared = false;
	public bool isGame2Cleared = false;
}