using System.Collections.Generic;
using MyWebApp.Models;

namespace MyWebApp.Resources
{
    public interface IAdventureWorksRepository
    {
        List<ProductViewModel>  GetPaginatedProducts(int page, int pageSize);
    }
}