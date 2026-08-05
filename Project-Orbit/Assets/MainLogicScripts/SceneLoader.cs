using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    [SerializeField] private string loadSceneName = "";

   
    public void TitleLoad()
    {
        if(loadSceneName != null)
        {
            SceneManager.LoadScene(loadSceneName);
        }
    }
}
