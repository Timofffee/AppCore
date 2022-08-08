using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.MassDestruction.Components
{
    
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent] 
    public class TouchDestructor : CoreComponent, IPointerClickHandler
    {
        private Camera _mainCamera;
        
        private Destructible _destructible;
        public Destructible TouchedDestructible => _destructible;
        
        public OutputSignal OnDesctructibleTouched;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_mainCamera)
                return;
            
            Ray ray = _mainCamera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Solid solid = hit.collider.gameObject.GetComponent<Solid>();
                
                if (!solid)
                    return;

                _destructible = solid.Destructible;
                OnDesctructibleTouched.Invoke();
            }
        }
    }
}