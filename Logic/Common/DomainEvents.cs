using System.Reflection;

namespace DomainDrivenDesign.Logic.Common;

public class DomainEvents
{
	private static List<Type> _handlers;

	public static void Init()
	{
		_handlers = Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
			.ToList();
	}

	public static void Dispatch(IDomainEvent domainEvent)
	{
		foreach (Type handlerType in _handlers)
		{
			var canHandle = handlerType.GetInterfaces()
				.Any(x => x.IsGenericType 
				          && x.GetGenericTypeDefinition() == typeof(IHandler<>) 
				          && x.GetGenericArguments()[0] == domainEvent.GetType());
			if (canHandle)
			{
				dynamic handler = Activator.CreateInstance(handlerType);
				handler.Handle((dynamic)domainEvent);
			}
		}
	}

	public static async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}