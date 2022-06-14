using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DrawPad.Components;
using UnityEngine;

namespace Krem.DrawPad.Actions
{
    [NodeGraphGroupName("DrawPad")]
    public class UpdateDrawPad : CoreAction
    {
        public OutputSignal OnSegmentDrawn;
        
        [InjectComponent] private Components.DrawPad _drawPad;
        
        protected override bool Action()
        {
            DrawShadowSegment();
            DrawSegments();

            return true;
        }

        private void DrawShadowSegment()
        {
            Vector2 direction = (_drawPad.ActualPointerPosition - _drawPad.LastSpawnedPosition).normalized;
            SetSegmentPosition(_drawPad.CurrentShadowSegment, direction);
            float distance = Vector2.Distance(_drawPad.LastSpawnedPosition, _drawPad.ActualPointerPosition);
            _drawPad.CurrentShadowSegment.RectTransform.sizeDelta =
                new Vector2(_drawPad.CurrentShadowSegment.RectTransform.sizeDelta.x, distance);
            _drawPad.CurrentShadowSegment.RectTransform.anchoredPosition = _drawPad.LastSpawnedPosition + direction * distance / 2;
            
            _drawPad.CurrentShadowSegment.gameObject.SetActive(true);
        }
        
        private void DrawSegments()
        {
            while (
                Vector2.Distance(_drawPad.LastSpawnedPosition, _drawPad.ActualPointerPosition) > _drawPad.Settings.Model.SegmentHeight && 
                _drawPad.AvailableSegmentsCount > 0)
            {
                // Get From Pool
                DrawPadSegment segmentInstance = _drawPad.GetFromPool();

                // Set Actual Position
                Vector2 direction = (_drawPad.ActualPointerPosition - _drawPad.LastSpawnedPosition).normalized;
                SetSegmentPosition(segmentInstance, direction);
                _drawPad.LastSpawnedPosition += direction * _drawPad.Settings.Model.SegmentHeight - direction * _drawPad.Settings.Model.SegmentCapSize;
                
                _drawPad.DrawnSegments.Add(segmentInstance);
                
                OnSegmentDrawn.Invoke();
            }
        }

        private void SetSegmentPosition(DrawPadSegment segmentInstance, Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _drawPad.Settings.Model.SegmentDrawAngleOffset;

            RectTransform instanceRectTransform = segmentInstance.RectTransform;
            instanceRectTransform.sizeDelta = new Vector2(instanceRectTransform.sizeDelta.x, _drawPad.Settings.Model.SegmentHeight);
            instanceRectTransform.anchoredPosition = _drawPad.LastSpawnedPosition + direction * _drawPad.Settings.Model.SegmentHeight / 2;
            instanceRectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}