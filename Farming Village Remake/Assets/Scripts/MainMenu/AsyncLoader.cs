using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [Header("Select")]
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject Main;
    

    public void LoadingNextscreen(string levelToLoad)
    {
        Main.SetActive(false);
        LoadingScreen.SetActive(true);

        StartCoroutine(LoadScreenAsync(levelToLoad));
    }

    IEnumerator LoadScreenAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;

        // Wait until the scene is fully loaded
        while (loadOperation.progress < 0.9f)
        {
            yield return null;
        }

        // Wait for 5 seconds on the loading screen
        yield return new WaitForSeconds(2f);

        // Allow the scene to be activated
        loadOperation.allowSceneActivation = true;
    }
}
