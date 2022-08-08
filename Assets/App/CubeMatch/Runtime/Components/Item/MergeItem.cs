using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.CubeMatch.Components.Item
{
    [NodeGraphGroupName("Cube Match/Item")]
    [DisallowMultipleComponent]
    public class MergeItem : CoreComponent
    {
        [SerializeField] private GameObject _hideFX;
        
        public OutputSignal OnSelectRequest;

        public void SelectRequest()
        {
            StartCoroutine(InstantiateHideFX());
            
            OnSelectRequest.Invoke();
        }
        
        protected IEnumerator InstantiateHideFX()
        {
            yield return new WaitForSeconds(.4f);

            Instantiate(_hideFX, transform.position, Quaternion.identity , null);
            Destroy(this.gameObject);
        }
    }
}