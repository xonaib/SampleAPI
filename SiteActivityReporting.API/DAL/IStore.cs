namespace SiteActivityReporting.API.DAL
{
    public interface IStore<T>
    {
        T Get(string key);

        bool Save(T entity);

        bool PruneData(int dataOlderThanSeconds);
    }
}
