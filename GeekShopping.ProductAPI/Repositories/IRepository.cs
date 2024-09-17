using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Base;

namespace GeekShopping.ProductAPI.Repositories
{
    public interface IRepository
    {
        void Create<T>(T entity) where T : BaseEntity;

        void Update<T>(T entity) where T : BaseEntity;

        void Delete<T>(T entity) where T : BaseEntity;

        Task<bool> SaveChangesAsync();

        Task<List<Product>> FindAllProducts();

        Task<Product?> FindProductById(long id);
    }
}
