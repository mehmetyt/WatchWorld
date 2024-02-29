using ApplicationCore.Entities;
using Ardalis.Specification;


namespace ApplicationCore.Interfaces
{
	public interface IRepository<T> where T :BaseEntity
	{
		public Task<T?> GetByIdAsync(int id);
		public Task<List<T>> GetAllAsync(ISpecification<T> specification);
		public Task<T> AddAsync(T entity);
		public Task UpdateAsync(T entity);
		public Task DeleteAsync(T entity);
		public Task<int> CountAsync(ISpecification<T> specification);
		public Task<T> FirstAsync(ISpecification<T> specification);
		public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
	}
}
