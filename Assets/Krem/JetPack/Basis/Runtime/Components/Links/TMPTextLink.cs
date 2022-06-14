using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using TMPro;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class TMPTextLink : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected TMP_Text _tmpText;
        
        public TMP_Text TmpText => _tmpText;
    }
}