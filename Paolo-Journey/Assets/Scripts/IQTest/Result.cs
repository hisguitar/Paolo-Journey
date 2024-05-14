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
        Debug.Log( "age: " + age);
        
        int countScore = CanvasManager.countScore;
        
        Debug.Log("Score: " + countScore);
        
        iq = (countScore * 10.5f) / age;
        //IQResult.text = string.Format(iq);
        IQResult.text = string.Format("{0}", iq);

    }
}
