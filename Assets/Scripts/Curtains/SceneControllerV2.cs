using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerV2 : MonoBehaviour
{
    [SerializeField]
    private Animator curtainAnimator = null;

    [SerializeField]
    private SceneReference[] gameScenes = null;

    private int currentSceneIndex;
    private bool isChangingScenes = false;


    private void Awake()
    {
        if (curtainAnimator == null)
            Debug.LogWarning("Curtain animator reference is missing.");

        if (gameScenes.Length <= 0)
            Debug.LogWarning("No scenes are included in array.");

        currentSceneIndex = 0;
    }

    private void Update()
    {
        if (!isChangingScenes)
        {
            // Go back 1 scene
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Loop scenes
                int nextIndex = currentSceneIndex - 1 < 0 ? gameScenes.Length - 1 : currentSceneIndex - 1;
                StartCoroutine(TransitionScenes(currentSceneIndex, nextIndex));
            }

            // Go forward 1 scene
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                int nextIndex = currentSceneIndex + 1 > gameScenes.Length - 1 ? 0 : currentSceneIndex + 1;
                StartCoroutine(TransitionScenes(currentSceneIndex, nextIndex));
            }
        }
    }

    private IEnumerator TransitionScenes(int sceneToUnload, int sceneToLoad)
    {
        isChangingScenes = true;
        curtainAnimator.SetTrigger("Close");

        // Wait for animation to finish (quick and dirty way of doing this!)
        yield return new WaitForSeconds(1f);

        // Unload previous background scene
        yield return SceneManager.UnloadSceneAsync(gameScenes[sceneToUnload]);

        // Load new scene
        yield return SceneManager.LoadSceneAsync(gameScenes[sceneToLoad], LoadSceneMode.Additive);

        curtainAnimator.SetTrigger("Open");
        currentSceneIndex = sceneToLoad;
        isChangingScenes = false;
    }

}
