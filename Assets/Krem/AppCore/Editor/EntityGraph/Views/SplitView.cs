using UnityEngine.UIElements;

namespace Krem.AppCore.EntityGraph.Views
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }

        public SplitView()
        {
        }
    }
}
