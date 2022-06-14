using Krem.JetPack.StateMachine.Scriptables;
using StateExample.Enums;
using UnityEngine;

namespace StateExample.Scriptables
{
    [CreateAssetMenu(fileName = "MyLevelState", menuName = "State Example/MyLevelState")]
    public class ScriptableMyLevelState : BaseScriptableState<MyLevelStates>
    {
        
    }
}