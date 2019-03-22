using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Animator curtainAnimator = null;

    [SerializeField]
    [Range(0,10)]
    private int minSceneIndex = 1;

    [SerializeField]
    [Range(0, 10)]
    private int maxSceneIndex = 3;


    private int currentSceneIndex;
    private bool isChangingScenes = false;


    private void Awake()
    {
        if (curtainAnimator == null)
            Debug.LogWarning("Curtain animator reference is missing.");

        if (minSceneIndex > maxSceneIndex)
            Debug.LogWarning("Minimum scene index is larger than maximum scene index. You're going to have a bad time.");

        currentSceneIndex = minSceneIndex;
    }

    private void Update()
    {
        if (!isChangingScenes)
        {
            // Go back 1 scene
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(TransitionScenes(currentSceneIndex, currentSceneIndex - 1 < minSceneIndex ? maxSceneIndex : currentSceneIndex - 1));
            }

            // Go forward 1 scene
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(TransitionScenes(currentSceneIndex, currentSceneIndex + 1 > maxSceneIndex ? minSceneIndex : currentSceneIndex + 1));
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
        yield return SceneManager.UnloadSceneAsync(sceneToUnload);

        // Load new scene
        yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        curtainAnimator.SetTrigger("Open");
        currentSceneIndex = sceneToLoad;
        isChangingScenes = false;
    }

}
