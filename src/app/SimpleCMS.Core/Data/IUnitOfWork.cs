namespace SimpleCMS.Core.Data
{
    public interface IUnitOfWork
    {
        void Dispose();
        void Commit();
    }
}