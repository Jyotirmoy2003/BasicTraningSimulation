using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModelEndUI : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] GVR_Button NextButton;
    [Header("SceneTravel")]
    [SerializeField] string nextSceneName;

    private bool listenToInput = false;

    void Start()
    {
        EventManager.OnChapterEndEvent += ActivateUI;
        DeactivateUI();
    }

    

    void ActivateUI()
    {
        NextButton.SetInteractable(IsSceneValid(nextSceneName));
        ui.SetActive(true);
        listenToInput = true;
    }

    void DeactivateUI()
    {
        ui.SetActive(false);
    }



    #region BUTTONS
    public void OnRestartButtonPressed()
    {
        if (!listenToInput) return;
        UIManager.Instance.BlackScreenFadeIn();
        Invoke(nameof(ReloadScene), 1f);
    }

    public void OnExitButtonPressed()
    {
        if (!listenToInput) return;
        UIManager.Instance.BlackScreenFadeIn();
        Invoke(nameof(LoadLobby), 1f);
    }

    public void OnNextModuleButtonPressed()
    {
        if (!listenToInput) return;
        UIManager.Instance.BlackScreenFadeIn();
        Invoke(nameof(LoadNextScene), 1f);
    }
    #endregion





    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void LoadLobby()
    {
        SceneManager.LoadScene(_GameAssets.Instance.LobbySceneName);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    bool IsSceneValid(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);

            if (name == sceneName)
                return true;
        }
        return false;
    }

}
