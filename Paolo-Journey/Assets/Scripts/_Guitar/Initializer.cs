using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    private const string LoadingScreenName = "GameplayManager";

    private void Awake()
    {
        var loadingScreenScene = SceneManager.GetSceneByName(LoadingScreenName);

        if (!loadingScreenScene.isLoaded)
        {
            SceneManager.LoadScene(LoadingScreenName, LoadSceneMode.Additive);
        }
    }
}