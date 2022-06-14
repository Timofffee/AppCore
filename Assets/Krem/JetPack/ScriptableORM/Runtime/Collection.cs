using System;
using System.Collections.Generic;

namespace Krem.JetPack.ScriptableORM
{
    [Serializable]
    public class Collection<TModel> where TModel : Model
    {
        public List<TModel> Data;
    }
}
