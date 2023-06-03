using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public Text loadingPercent;

    private void Start()
    {
        this.DisableLoadingScreen();
    }

    private void DisableLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevel(int sceneIndex)
    {
        
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            loadingSlider.value = progress;
            loadingPercent.text = progress * 100f + "%";
            yield return null;
        }
    }
}
