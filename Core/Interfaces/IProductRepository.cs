using System.Threading.Tasks; //for Task
using System.Collections.Generic; // for IReadOnlyList
using Core.Entities;
namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(int id);
        Task<IReadOnlyList<Product>> GetProducts();
        Task<IReadOnlyList<ProductBrand>> GetProductBrands();
        Task<IReadOnlyList<ProductType>> GetProductTypes();
    }
}