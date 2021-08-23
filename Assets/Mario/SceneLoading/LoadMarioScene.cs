using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class used to reload the scene
/// </summary>
public class LoadMarioScene : MonoBehaviour
{
    public Slider progressBar;

    private AsyncOperation loadingOperation;
    private readonly string sceneToLoad = "MarioScene";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);

        if (loadingOperation.isDone)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts loading the scene
    /// </summary>
    public void LoadScene()
    {
        progressBar.value = 0;
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
