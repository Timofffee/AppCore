using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.JetPack.MetaGame.Models
{
    [System.Serializable]
    public class MetaUpgradeSettingsModel : Model 
    {
        [Header("Data")]
        [SerializeField] protected int _initialPrice;
        [SerializeField] protected int _perLevelPrice;

        public int InitialPrice => _initialPrice;
        public int PerLevelPrice => _perLevelPrice;

    }
}