using DddInPractice.Logic;

namespace Logic;

public abstract class Repository<T> where T : AggregateRoot
{
	public T GetById(long id)
	{
		using (var session = SessionFactory.OpenSession())
		{
			return session.Get<T>(id);
		}
	}
	
	public void Save(T entity)
	{
		using(var session = SessionFactory.OpenSession())
		using (var transaction = session.BeginTransaction())
		{
			session.SaveOrUpdate(entity);
			transaction.Commit();
		}
	}
}