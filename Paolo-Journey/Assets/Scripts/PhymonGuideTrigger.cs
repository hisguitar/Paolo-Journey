
using UnityEngine;

public class PhymonGuideTrigger : MonoBehaviour
{
	[SerializeField] private GameObject phymonGuideText;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			phymonGuideText.SetActive(true);
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			phymonGuideText.SetActive(false);
		}
	}
}