using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine.SceneManagement;

namespace Krem.JetPack.Basis.Actions.SceneComponent
{
    [NodeGraphGroupName("Jet Pack/Basis/SceneComponent")]
    public class LoadScene : CoreAction
    {
        public InputComponent<Components.SceneComponent> SceneComponent;
        
        protected override bool Action()
        {
            if (SceneComponent.Component.reloadCurrentScene)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
                
                return true;
            }
            
            SceneManager.LoadScene(SceneComponent.Component.sceneName);
            
            return true;
        }
    }
}
