using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.CoinCollector.Components;
using Krem.JetPack.Basis.Components.Links;
using UnityEngine;

namespace Krem.CoinCollector.Actions
{
    [NodeGraphGroupName("CoinCollector")] 
    public class GetCoinFXFromPool : CoreAction 
    {
        public InputComponent<TransformLink> _transformLink;
        
        [InjectComponent] private CoinFXPoolProvider _FXPoolProvider;
        [InjectComponent] private CameraLink _mainCamera;
        
        protected override bool Action()
        {
            Vector2 screenPoint = _mainCamera.Camera.WorldToScreenPoint(_transformLink.Component.Transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _FXPoolProvider.scriptableFXPool.coinFXPool.RectTransform, 
                screenPoint, 
                _FXPoolProvider.scriptableFXPool.coinFXPool.UICamera,
                out Vector2 position);

            _FXPoolProvider.GetFromPool(position);

            return true;
        }
    }
}