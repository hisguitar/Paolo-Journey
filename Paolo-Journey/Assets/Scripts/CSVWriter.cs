using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// In OnGUI(), button will appear in "PaoloJourney" scene only
/// Manages CSV file operations including creating, updating, and writing statistical data.
/// </summary>
public class CSVWriter : SingletonPersistent<CSVWriter>
{
	private string filename = "";

	[System.Serializable]
	public class StatisticalData
	{
		[Tooltip("Day/Month/Year")] public string date;
		public int interestNebulizers = 0;
		public int interestDressingWounds = 0;
		public int interestWashingHands = 0;
		public int interestTestChildren = 0;
		public int interestIQTest = 0;
	}

	[System.Serializable]
	public class DataList
	{
		public List<StatisticalData> statisticalDatas = new List<StatisticalData>();
		private string lastDate = "";

		public void CreateNewStatisticalData()
		{
			StatisticalData newStatisticalData = new StatisticalData();
			string currentDate = System.DateTime.Now.ToString("dd/MM/yyyy");
			
			if (currentDate != lastDate)
			{
				lastDate = currentDate;
			}
			newStatisticalData.date = currentDate;
			
			statisticalDatas.Add(newStatisticalData);
		}

		public void WriteCSV(string path)
		{
			using (TextWriter textWriter = new StreamWriter(path, false))
			{
				textWriter.WriteLine(
					"DD/MM/YYYY, " +
					"Interest Nebulizers, " +
					"Interest Dressing Wounds, " +
					"Interest Washing Hands, " +
					"Interest Test Children, " +
					"Interest IQ-Test");

				foreach (var data in statisticalDatas)
				{
					textWriter.WriteLine(
						data.date + "," +
						data.interestNebulizers + "," +
						data.interestDressingWounds + "," +
						data.interestWashingHands + "," +
						data.interestTestChildren + "," +
						data.interestIQTest);
				}
			}
		}
	}

	public DataList dailyDataList = new ();

	private void Start()
	{
		filename = Application.dataPath + "/Statistical Datas.csv";
		InitializeData();
	}
	
	private void OnGUI()
	{
		if (SceneManager.GetActiveScene().name == "PaoloJourney")
		{
			GUIStyle buttonStyle = GUI.skin.button;
			buttonStyle.fontSize = 18;

			// Create button for 'Export to Excel'
			if (GUI.Button(new Rect(10, 10, 140, 30), "Export to Excel", buttonStyle))
			{
				dailyDataList.WriteCSV(filename); // Write CSV with current data
			}

			// Create button for 'New Date Statistical Data'
			if (GUI.Button(new Rect(160, 10, 240, 30), "Create New Statistical Data", buttonStyle))
			{
				CreateNewStatisticalData();
				dailyDataList.WriteCSV(filename); // Write new data to CSV
			}
		}
	}
	
	private void InitializeData()
	{
		if (File.Exists(filename))
		{
			// Check last recorded date in the existing file
			string lastDate = GetLastDateFromCSV();
			string currentDate = System.DateTime.Now.ToString("dd/MM/yyyy");

			if (currentDate != lastDate)
			{
				dailyDataList.CreateNewStatisticalData();
				dailyDataList.WriteCSV(filename); // Write the new data to CSV file
			}
			else
			{
				LoadCSV(filename);
			}
		}
		else
		{
			// File does not exist, create new data
			dailyDataList.CreateNewStatisticalData();
			dailyDataList.WriteCSV(filename);
		}
	}

	private string GetLastDateFromCSV()
	{
		string lastDate = "";
		using (var reader = new StreamReader(filename))
		{
			// Skip header
			reader.ReadLine();

			string line;
			while ((line = reader.ReadLine()) != null)
			{
				// Read last line date
				var columns = line.Split(',');
				lastDate = columns[0].Trim();
			}
		}
		return lastDate;
	}

	private void LoadCSV(string path)
	{
		dailyDataList.statisticalDatas.Clear();
		
		using (var reader = new StreamReader(path))
		{
			// Skip header
			reader.ReadLine();

			string line;
			while ((line = reader.ReadLine()) != null)
			{
				var columns = line.Split(',');
				StatisticalData data = new StatisticalData
				{
					date = columns[0].Trim(),
					interestNebulizers = int.Parse(columns[1].Trim()),
					interestDressingWounds = int.Parse(columns[2].Trim()),
					interestWashingHands = int.Parse(columns[3].Trim()),
					interestTestChildren = int.Parse(columns[4].Trim()),
					interestIQTest = int.Parse(columns[5].Trim())
				};

				dailyDataList.statisticalDatas.Add(data);
			}
		}
	}

	#region Increment Data
	public void CreateNewStatisticalData()
	{
		dailyDataList.CreateNewStatisticalData();
		dailyDataList.WriteCSV(filename); // Write new data to file after creation
	}

	public void IncrementInterest(string fieldName, StatisticalData data)
	{
		switch (fieldName)
		{
			case "Nebulizers":
				data.interestNebulizers += 1;
				break;
			case "DressingWounds":
				data.interestDressingWounds += 1;
				break;
			case "WashingHands":
				data.interestWashingHands += 1;
				break;
			case "TestChildren":
				data.interestTestChildren += 1;
				break;
			case "IQTest":
				data.interestIQTest += 1;
				break;
			default:
				Debug.LogError("Invalid field name: " + fieldName);
				return;
		}

		// Update CSV immediately
		dailyDataList.WriteCSV(filename);
	}
	#endregion
}