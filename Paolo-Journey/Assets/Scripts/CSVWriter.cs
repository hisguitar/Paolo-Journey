using UnityEngine;
using System.IO;
using static CSVWriter;

/// <summary>
/// For now!
/// Once you've exported your data,
/// you should move 'Statistical Datas.csv' file to another location before starting to create the next set of data.
/// </summary>
public class CSVWriter : SingletonPersistent<CSVWriter>
{
    #region CSVWriter
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
        public StatisticalData[] statisticalDatas;
        private string lastDate = "";

        // Enter date to date parameter & Create new statistical data list
        public void CreateNewStatisticalData()
        {
            StatisticalData newStatisticalData = new StatisticalData();

            #region Enter date to date parameter
            string currentDate = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (currentDate != lastDate)
            {
                lastDate = currentDate;
            }
            newStatisticalData.date = currentDate;
            #endregion

            #region Create new statistical data
            if (statisticalDatas == null)
            {
                statisticalDatas = new StatisticalData[1];
            }
            else
            {
                StatisticalData[] newStatisticalDatas = new StatisticalData[statisticalDatas.Length + 1];
                for (int i = 0; i < statisticalDatas.Length; i++)
                {
                    newStatisticalDatas[i] = statisticalDatas[i];
                }
                statisticalDatas = newStatisticalDatas;
            }
            statisticalDatas[statisticalDatas.Length - 1] = newStatisticalData;
            #endregion
        }
    }

    public DataList dailyDataList = new();

    private void Start()
    {
        filename = Application.dataPath + "/Statistical Datas.csv";

        CreateNewStatisticalData();
    }

    // Use this function to export 'excel report file'
    public void WriteCSV()
    {
        Debug.Log("WriteCSV");
        if(dailyDataList.statisticalDatas.Length > 0)
        {
            TextWriter textWriter = new StreamWriter(filename, false);
            textWriter.WriteLine(
                "DD/MM/YYYY, " +
                "Interest Nebulizers, " +
                "Interest Dressing Wounds, " +
                "Interest Washing Hands, " +
                "Interest Test Children, " +
                "Interest IQ-Test");
            textWriter.Close();

            textWriter = new StreamWriter(filename, true);

            for(int i = 0; i < dailyDataList.statisticalDatas.Length; i++)
            {
                textWriter.WriteLine(
                    dailyDataList.statisticalDatas[i].date + "," +
                    dailyDataList.statisticalDatas[i].interestNebulizers + "," +
                    dailyDataList.statisticalDatas[i].interestDressingWounds + "," +
                    dailyDataList.statisticalDatas[i].interestWashingHands + "," +
                    dailyDataList.statisticalDatas[i].interestTestChildren + "," +
                    dailyDataList.statisticalDatas[i].interestIQTest);
            }
            textWriter.Close();
        }
    }
    #endregion

    #region Increment Data
    public void CreateNewStatisticalData()
    {
        dailyDataList.CreateNewStatisticalData();
    }

    public void IncrementInterestNebulizers(StatisticalData data)
    {
        
        data.interestNebulizers += 1;
    }

    public void IncrementInterestDressingWounds(StatisticalData data)
    {
        data.interestDressingWounds += 1;
    }

    public void IncrementInterestWashingHands(StatisticalData data)
    {
        data.interestWashingHands += 1;
    }

    public void IncrementInterestTestChildren(StatisticalData data)
    {
        data.interestTestChildren += 1;
    }

    public void IncrementInterestIQTest(StatisticalData data)
    {
        data.interestIQTest += 1;
    }
    #endregion
}