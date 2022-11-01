using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace App.ArmyClash.Models
{
    [CreateAssetMenu(fileName = "UnitScriptableModel", menuName = "ArmyClash/UnitScriptableModel", order = 0)]
    public class UnitScriptableModel : ScriptableModel<UnitModel> 
    {
    }
}