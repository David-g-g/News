using System.Threading.Tasks;
using AngleSharp.Dom;

namespace Scrapper
{
    public interface IAngleSharpPageLoader    
    {
        Task<IDocument> LoadPage(string url);
    }
}