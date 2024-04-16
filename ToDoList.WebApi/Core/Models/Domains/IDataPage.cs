
namespace MyFinances.WebApi.Models.Domains
{
    public interface IDataPage<T>
    {
     
        IEnumerable<T> Items { get; set; }
        int CurrentPage { get; set; }
        int LastPage { get; set; }
    }
}