using System.Collections.Generic;
using System.Threading.Tasks;
using API_TEST.Models;

namespace API_TEST.Repositories
{
    public interface IProductRepository
    {
         Task<IEnumerable<Product>> Get();
         Task<Product> Get(int id);
         Task<Product> Create(Product  product);
         Task Update(Product product);
         Task  Delete(int id);
         
         // now add concreate implementation
    }
}