using System.Collections.Generic;
using Krem.AppCore.Ports;

namespace Krem.AppCore.Interfaces
{
    public interface ICoreConnections
    {
        public List<CorePort> Connections { get; }
    }
}