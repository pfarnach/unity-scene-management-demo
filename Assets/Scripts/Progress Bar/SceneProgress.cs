using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneProgress : MonoBehaviour
{
    [SerializeField]
    private SceneReference gameScene;

    [SerializeField]
    private Text loadingPercentText;

    [SerializeField]
    private GameObject pressAnyKeyLabel;

    private AsyncOperation ao;
    private bool hasLoaded = false;


    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        //Debug.Break();
        //yield return null;

        ao = SceneManager.LoadSceneAsync(gameScene);

        //yield return SceneManager.LoadSceneAsync(gameScene);

        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            loadingPercentText.text = (progress * 100f).ToString("0.##") + "%";

            Debug.Log(progress);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            if (ao.progress >= 0.9f)
            {
                // Activate instructional text
                if (!pressAnyKeyLabel.activeSelf)
                    pressAnyKeyLabel.SetActive(true);

                if (Input.anyKeyDown)
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
