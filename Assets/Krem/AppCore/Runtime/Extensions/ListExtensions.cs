using System.Collections.Generic;

namespace Krem.AppCore.Extensions
{
    public static class ListExtensions
    {

        public static T? Random<T>(this List<T> src)
        {
            return src.Count == 0 ? default(T) : src[UnityEngine.Random.Range(0, src.Count)];
        }
    }
}