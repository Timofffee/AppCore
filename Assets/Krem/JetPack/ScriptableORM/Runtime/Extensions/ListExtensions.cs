using System.Collections.Generic;

namespace Krem.JetPack.ScriptableORM.Extensions
{
    public static class ListExtensions
    {
        public static List<TModel> Clone<TModel>(this List<TModel> source) where TModel : Model, new()
        {
            List<TModel> clone = new List<TModel>();
            
            source.ForEach(item =>
            {
                clone.Add((TModel)item.Clone<TModel>());
            });
            
            return clone;
        }
    }
}
