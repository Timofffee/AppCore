using App.ArmyClash.Components.Placeholders;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Actions.Placeholders
{
    [NodeGraphGroupName("ArmyClash/Placeholders")] 
    public class GenerateArmy : CoreAction
    {
        public InputComponent<ArmyGenerator> ArmyGenerator;

        public OutputSignal GenerateRequest;
        
        protected override bool Action()
        {
            int referencedArmyUnitsCount = ArmyGenerator.Component.ReferencedArmy.Collection.Count;
            referencedArmyUnitsCount = referencedArmyUnitsCount == 0 ? Random.Range(1, 3) : referencedArmyUnitsCount;
            int randomUnitsCount =
                Mathf.RoundToInt(referencedArmyUnitsCount * ArmyGenerator.Component.RandomCountPercentage / 100);
            randomUnitsCount = referencedArmyUnitsCount - randomUnitsCount <= 0 ? 0 : randomUnitsCount;

            int unitsCountToGenerate = referencedArmyUnitsCount + Random.Range(-randomUnitsCount, randomUnitsCount + 1);

            for (int i = 0; i < unitsCountToGenerate; i++)
            {
                GenerateRequest.Invoke();
            }
            
            return true;
        }
    }
}