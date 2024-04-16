namespace MyFinances.WebApi.Models.Domains
{
    public class DataPage<T> : IDataPage<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
    }
}
