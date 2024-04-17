

using Core.Entities;
using Core.Specification;

namespace Core.Interface
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntitywithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsynce(ISpecification<T> spec);
    }
}