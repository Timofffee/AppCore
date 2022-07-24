using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Scriptables;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Components
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls")]
    public class ScriptableAxis2DDecorator : Axis2D
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ScriptableAxis2D _scriptableAxis2D;
        
        public override Vector2 Axis { set => _scriptableAxis2D.Axis = value; get => _scriptableAxis2D.Axis; }
    }
}