using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Tooltip("Names of the scenes to load additively")]
    public List<string> sceneNamesToLoad;

    void Start()
    {
        StartCoroutine(LoadSimulationScenes());
    }

    private IEnumerator LoadSimulationScenes()
    {
        foreach (string sceneName in sceneNamesToLoad)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                asyncLoad.allowSceneActivation = true;

                while (!asyncLoad.isDone)
                {
                    yield return null; // wait until scene is loaded
                }
            }
        }

        Debug.Log("All simulation scenes loaded!");
        // Now everything is loaded and physics objects can fall safely
    }
}
