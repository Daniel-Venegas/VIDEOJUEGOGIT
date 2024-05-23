using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button loadGameButton;
    public void NewGame()
    {
        StartCoroutine(LoadGameAsync(SceneTransitionManager.Location.FarmXC, null));
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadGameAsync(SceneTransitionManager.Location.FarmXC, LoadGame));
    }

    void LoadGame()
    {
        if (GameStateManager.Instance == null)
        {
            Debug.LogError("cannot fin Game State Manager");
            return;
        }

        GameStateManager.Instance.LoadSave();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGameAsync(SceneTransitionManager.Location scene, Action onFirstFrameLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.ToString());
        DontDestroyOnLoad(gameObject);
        while(!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading");
        }
        Debug.Log("Loaded");

        yield return new WaitForEndOfFrame();
        Debug.Log("First frame is loaded");
        onFirstFrameLoad?.Invoke();

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        loadGameButton.interactable = SaveManager.HasSave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
