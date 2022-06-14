using UnityEngine;
using UnityEngine.UIElements;

namespace Krem.AppCore.EntityGraph.Views
{
    public class ActionNodeView : NodeView
    {
        public ActionNodeView(CoreAction nodeInstance) : base(nodeInstance)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("EntityGraph/USS/ActionNode");
            styleSheets.Add(styleSheet);
        }
    }
}
