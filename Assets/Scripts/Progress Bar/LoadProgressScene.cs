using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadProgressScene : MonoBehaviour
{
    [SerializeField]
    private SceneReference progressBarScene;


    public void LoadScene()
    {
        SceneManager.LoadScene(progressBarScene);
    }
}
