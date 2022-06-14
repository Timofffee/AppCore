using System;
using UnityEngine;

namespace Krem.AppCore.Ports
{
    [Serializable]
    public sealed class InputSignal : CorePort
    {
        public override Color Color { get; } = Color.green;
        
        public Action InvokeAction;

        public void Invoke()
        {
            InvokeAction?.Invoke();
        }
    }
}