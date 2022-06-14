using System;
using UnityEngine;

namespace Krem.AppCore.Ports
{
    [Serializable]
    public sealed class InputData<T> : CorePort
    {
        public override Color Color { get; } = new Color(1f,.5f,.5f,1f);
        
        public T Data => _outputData.Data;

        private RuntimeFieldHandle _fieldHandle;
        private OutputData<T> _outputData;

        public void Bind(ref OutputData<T> outputData)
        {
            _outputData = outputData;
        }
    }
}