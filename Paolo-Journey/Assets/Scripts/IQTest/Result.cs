using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI IQResult;
    public float iq;
    
    void Start()
    {
        int age = CanvasManager.age;
        int countScore = CanvasManager.countScore;
        float time = CanvasManager.timeCount;
        
        Debug.Log( "age: " + age);
        Debug.Log("Score: " + countScore);
        Debug.Log("Time: " + time);


        if (age <= 15 || (age >= 61 && age <= 70))
        {
            iq = Mathf.RoundToInt(countScore * 8.5f);
        }
        else if ((age >= 16 && age <= 20) || (age >= 51 && age <= 60))
        {
            iq = Mathf.RoundToInt(countScore * 6.5f);
        }
        else if (age >= 21 && age <= 50)
        {
            iq = Mathf.RoundToInt(countScore * 5.8f);
        }

        if (iq >= 81 && iq <= 144)
        {
            IQResult.text = string.Format("{0}", iq);
        }
        else if (iq <= 80)
        {
            IQResult.text = string.Format("คุณมี IQ น้อยกว่า 80");
        }
        else if (iq >= 145)
        {
            IQResult.text = string.Format("คุณมี IQ มากกว่า 145");
        }
        
    }
    
    /*void Start()
    {
        int age = CanvasManager.age;
        Debug.Log( "age: " + age);
        
        int countScore = CanvasManager.countScore;
        
        Debug.Log("Score: " + countScore);
        
        //iq = (countScore * 10.5f) / age;
        iq = Mathf.RoundToInt((countScore * 10.5f) / age);
        IQResult.text = string.Format("{0}", iq);
    }*/
    
}
