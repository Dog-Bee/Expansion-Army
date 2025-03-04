using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private float secondsToLoad;
        [SerializeField] private int sceneNum;
        
        
        private IEnumerator Start()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum);
            asyncLoad.allowSceneActivation = false;

            yield return new WaitForSeconds(secondsToLoad);

            asyncLoad.allowSceneActivation = true;
        }
    }

