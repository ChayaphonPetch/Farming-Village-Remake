using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button NewGamebutton;

    [SerializeField] private Button Continuebutton;

    [SerializeField] private AsyncLoader loadingScene;
    public void OnNewGameClicked()
    {
        DisableMenuButton();
        DataPersistenceManager.Instance.NewGame();

        loadingScene.LoadingNextscreen("GamePlay Test");
    }

    public void OnContinueGameClick()
    {
        DisableMenuButton();
        loadingScene.LoadingNextscreen("GamePlay Test");
    }

    private void DisableMenuButton()
    {
        NewGamebutton.interactable = false;
        Continuebutton.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
