using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.ProtoElements.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Krem.JetPack.ProtoElements.Actions
{
    [NodeGraphGroupName("Jet Pack/Proto Elements")]
    public class LoadSceneAsync : CoreAction
    {
        [InjectComponent] private AsyncSceneLoader _asyncSceneLoader;

        private bool _loadingState = false;

        protected override bool Action()
        {
            if (_loadingState)
            {
                return false;
            }

            _loadingState = true;
            
            _asyncSceneLoader.StartCoroutine(Load());
            
            return true;
        }

        private IEnumerator Load()
        {

            Scene currentScene = SceneManager.GetActiveScene();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(
                _asyncSceneLoader.SceneComponent.reloadCurrentScene ? 
                    currentScene.name : 
                    _asyncSceneLoader.SceneComponent.sceneName
                    );

            while (asyncLoad.isDone == false)
            {
                _asyncSceneLoader.HorizontalProgressBar.ProgressValue = asyncLoad.progress;
                yield return null;
            }

            _loadingState = false;
            _asyncSceneLoader.HorizontalProgressBar.ProgressValue = 1f;
        }
    }
}