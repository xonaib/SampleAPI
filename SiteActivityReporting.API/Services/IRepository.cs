namespace SiteActivityReporting.API.Services
{
    public interface IRepository<T>
    {
        public T Get(string key);
        public bool Save(string key, T entity);

        bool PruneData(int dataOlderThanMinutes);
    }
}
