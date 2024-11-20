using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public float elapsedTime {get; private set;} = 0f; // Start time (unit: seconds)
	private bool isTimerRunning = false;
	
	// Don't need to ResetTimer because it can continue.
	public void StartTimer()
	{
		if (!isTimerRunning)
		{
			isTimerRunning = true;
			StartCoroutine(TimerCoroutine());
		}
	}

	public void StopTimer()
	{
		isTimerRunning = false;
	}

	public void ResetTimer()
	{
		elapsedTime = 0f;
	}

	private IEnumerator TimerCoroutine()
	{
		while (isTimerRunning)
		{
			elapsedTime += Time.deltaTime;
			yield return null; // Wait until the next frame.
		}
	}

	public string GetFormattedTime()
	{
		int minutes = Mathf.FloorToInt(elapsedTime / 60f); // Minute
		int seconds = Mathf.FloorToInt(elapsedTime % 60f); // Second
		return $"{minutes:00}:{seconds:00}";
	}
}