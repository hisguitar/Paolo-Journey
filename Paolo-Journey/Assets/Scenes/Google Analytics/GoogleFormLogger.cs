	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Networking;

	public class GoogleFormLogger : MonoBehaviour
	{
		private const string FormUrl = "https://docs.google.com/forms/u/2/d/e/1FAIpQLSeZCQlKt1PhO9OvGA24tHmDu5dhE4SAHguaQkoGoUNKeAEy5A/formResponse?pli=1"; // URL of Google Forms
		
		// Call this function when you need to submit form
		// Send data to Google Forms
		public void SubmitForm(int correctAnswers, List<string> incorrectFoods, int viewDescription, int tapCount, string totalSeconds)
		{
			StartCoroutine(SendToGoogleForm(correctAnswers, incorrectFoods, viewDescription, tapCount, totalSeconds));
		}

		private IEnumerator SendToGoogleForm(int correctAnswers, List<string> incorrectFoods, int viewDescription, int tapCount, string totalSeconds)
		{
			WWWForm form = new WWWForm();
			
			// Enter the information you want to submit (Entry IDs are required for each field in Google Forms).
			// 1. (Multiple Choice) How many questions does one player answer correctly about what foods to eat/not to eat?
			form.AddField("entry.1301338822", correctAnswers.ToString());  // Replace "123456789" with Entry ID
			// 2. (Checkboxes) Which questions did the player answer incorrectly regarding what foods to eat/not to eat?
			for (int i = 0; i < incorrectFoods.Count; i++)
			{
				form.AddField("entry.1964719216", incorrectFoods[i]);
			}
			// 3. (Short Answer) How many times have players viewed the food description?
			form.AddField("entry.1639523446", viewDescription.ToString());
			form.AddField("entry.596631467", tapCount.ToString());
			form.AddField("entry.1530290587", totalSeconds);

			// Send HTTP POST request
			using (UnityWebRequest www = UnityWebRequest.Post(FormUrl, form))
			{
				yield return www.SendWebRequest();

				if (www.result == UnityWebRequest.Result.Success)
				{
					Debug.Log("Form submitted successfully!");
				}
				else
				{
					Debug.LogError($"Form submission failed: {www.error}");
				}
			}
		}
	}