using UnityEngine;

public class PositionSaver : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        float savedX = PlayerPrefs.GetFloat("PlayerPosX", player.transform.position.x);
        float savedY = PlayerPrefs.GetFloat("PlayerPosY", player.transform.position.y);
        float savedZ = PlayerPrefs.GetFloat("PlayerPosZ", player.transform.position.z);
        
        player.transform.position = new Vector3(savedX, savedY, savedZ);
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.Save();
    }
}