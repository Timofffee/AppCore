namespace Krem.JetPack.ScriptableORM.Interfaces
{
    public interface IHaveGuid
    {
        public string Guid { get; }

        public void RegenerateGuid();
    }
}