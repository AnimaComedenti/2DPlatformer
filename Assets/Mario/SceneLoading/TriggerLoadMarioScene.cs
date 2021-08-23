using UnityEngine;

/// <summary>
/// A helper class to trigger loading the scene
/// </summary>
public class TriggerLoadMarioScene : MonoBehaviour
{
    public GameObject loadingScreen;

    /// <summary>
    /// Attached to a button to trigger loading the scene
    /// </summary>
    public void TriggerLoading()
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<LoadMarioScene>().LoadScene();
    }
}
