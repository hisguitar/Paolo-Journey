using UnityEngine;

public class HideAnotherPhymon : MonoBehaviour
{
	[SerializeField] private GameObject anotherPhymon;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			anotherPhymon.SetActive(false);
		}
	}
}