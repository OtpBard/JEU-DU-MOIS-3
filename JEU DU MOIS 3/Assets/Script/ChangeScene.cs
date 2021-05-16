using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    
    public void LoadSceneNow()
    {
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator LoadSceneInternal()
    {
        LoadingScreen.Instance.ShowLoadingScreen(1.0f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);

    }

    public void LoadScene()
    {
        
        StartCoroutine(LoadSceneInternal());
        
    }
}
