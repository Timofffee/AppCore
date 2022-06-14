using UnityEngine;
using UnityEngine.UIElements;

namespace Krem.AppCore.EntityGraph.Views
{
    public class ComponentNodeView : NodeView
    {
        public ComponentNodeView(CoreComponent nodeInstance) : base(nodeInstance)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("EntityGraph/USS/ComponentNode");
            styleSheets.Add(styleSheet);
        }
    }
}