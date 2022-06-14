using Krem.JetPack.HyperControls.Interfaces;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableAxis2D", menuName = "Jet Pack/HyperControls/ScriptableAxis2D", order = 0)]
    public class ScriptableAxis2D : ScriptableObject, IAxis2D
    {
        public Vector2 Axis { get; set; }
    }
}
