using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    public Canvas screenSpace_Canvas;
    public Canvas menuCanvas;
    public GameObject player;
    public Button playButton;
    public Button iqTestButton;
    public Button optionsButton;
    public Button quitButton;
    public static bool isLevelComplete = false;
    void Start()
    {
        if (isLevelComplete) // เช็คว่าเล่นด่านเสร็จหรือยัง
        {
            screenSpace_Canvas.gameObject.SetActive(true);
            player.SetActive(true);
            menuCanvas.gameObject.SetActive(false);
        }
        else
        {
            screenSpace_Canvas.gameObject.SetActive(false);
            player.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
        }
    }

    public void PlayButton()
    {
        screenSpace_Canvas.gameObject.SetActive(true);
        player.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
    }
}
