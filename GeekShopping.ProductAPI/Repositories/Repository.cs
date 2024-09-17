using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Base;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repositories
{
    public class Repository : IRepository
    {

        private readonly MySQLContext _context;

        public Repository(MySQLContext context)
        {
            _context = context;
        }

        public void Create<T>(T entity) where T : BaseEntity
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<List<Product>> FindAllProducts()
        {
            IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await query.ToListAsync();
        }

        public async Task<Product?> FindProductById(long id)
        {
            Product? prod = new Product();
            prod = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return prod;
        }
    }
}
