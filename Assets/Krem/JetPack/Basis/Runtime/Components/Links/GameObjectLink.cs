using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class GameObjectLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected GameObject _gameObject;

        public GameObject GameObject => _gameObject;
    }
}