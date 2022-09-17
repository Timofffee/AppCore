namespace Krem.JetPack.ScriptableORM.Interfaces
{
    public interface IScriptableRepository
    {
        public bool Load();
        public bool Save();
        public bool Delete();
        public bool IsStorable();
        public object Clone();
        public void Fill(object dataToFill);
    }
}