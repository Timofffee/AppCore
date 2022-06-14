using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.DrawPad.Models
{
    [CreateAssetMenu(fileName = "DrawPadSegmentList", menuName = "DrawPad/DrawPadSegmentList", order = 0)]
    public class SegmentModelCollection : ScriptableCollection<SegmentModel>
    {

    }
}
