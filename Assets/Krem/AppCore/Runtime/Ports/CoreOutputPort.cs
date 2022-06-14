using System;
using System.Collections.Generic;
using Krem.AppCore.Interfaces;
using UnityEngine;

namespace Krem.AppCore.Ports
{
    [Serializable]
    public class CoreOutputPort : CorePort, ICoreConnections
    {
        [SerializeField] protected List<CorePort> _connections = new List<CorePort>();

        public List<CorePort> Connections { get => _connections; set => _connections = value; }
    }
}