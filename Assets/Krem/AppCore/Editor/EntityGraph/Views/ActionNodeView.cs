using UnityEngine;
using UnityEngine.UIElements;

namespace Krem.AppCore.EntityGraph.Views
{
    public class ActionNodeView : NodeView
    {
        public ActionNodeView(CoreAction nodeInstance, CoreEntity coreGraph) : base(nodeInstance, coreGraph)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("EntityGraph/USS/ActionNode");
            styleSheets.Add(styleSheet);
        }
    }
}
