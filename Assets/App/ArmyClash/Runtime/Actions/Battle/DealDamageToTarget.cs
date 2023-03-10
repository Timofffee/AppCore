using System.Collections;
using App.ArmyClash.Components.AI;
using App.ArmyClash.Components.Battle;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Actions.Battle
{
    [NodeGraphGroupName("ArmyClash/Battle")] 
    public class DealDamageToTarget : CoreAction
    {
        public InputComponent<DamageDealer> DamageDealer;
        public InputComponent<AIBehaviour> AIBehaviour;
        
        public OutputSignal OnDealDamage;
        
        private bool _triggered = false;
        private IEnumerator _coroutine;
        
        protected override bool Action()
        {
            if (_triggered 
                || DamageDealer.Component.gameObject.activeInHierarchy == false
                || DamageDealer.Component.Active == false
                )
                return false;
            
            _triggered = true;

            if (_coroutine != null)
            {
                DamageDealer.Component.StopCoroutine(_coroutine);
            }
            _coroutine = DelayInSeconds();
            
            DamageDealer.Component.StartCoroutine(_coroutine);
            
            return true;
        }
        
        IEnumerator DelayInSeconds()
        {
            yield return new WaitForSeconds(1 / DamageDealer.Component.UnitModel.CurrentUnitModel.AttackSpeed);
            
            _triggered = false;

            DamageReceiver damageReceiver = AIBehaviour.Component.AITarget == null ? 
                null :
                AIBehaviour.Component.AITarget.Transform.GetComponent<DamageReceiver>();

            if (DamageDealer.Component.Active && damageReceiver != null && damageReceiver.Active)
            {
                float sqrDistance = (DamageDealer.Component.Transform.position - damageReceiver.Transform.position)
                    .sqrMagnitude;

                if (sqrDistance <= Mathf.Pow(DamageDealer.Component.UnitModel.CurrentUnitModel.AttackRadius, 2))
                {
                    DamageDealer.Component.DealDamage(damageReceiver);
                }
            }
            
            OnDealDamage.Invoke();
        }
    }
}