namespace Krem.JetPack.ScriptableORM.Interfaces
{
    public interface IScriptableRepository
    {
        bool Load();
        bool Save();
        bool Delete();
        bool IsStorable();
        object Clone();
        void Fill(object dataToFill);
    }
}