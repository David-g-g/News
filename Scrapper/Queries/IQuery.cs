using System.Threading.Tasks;

namespace Scrapper
{
    public interface IQuery<T1, T2>
    {
        Task<T2> Execute (T1 parameters);
    }
}