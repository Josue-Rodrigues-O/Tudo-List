using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Repositories;

namespace Tudo_List.Test.Infrastructure.Repositories
{
    public class TodoListItemRepository : UnitTest
    {
        private readonly ITodoListItemRepository _itemRepository;

        public TodoListItemRepository()
        {
            _itemRepository = _serviceProvider.GetRequiredService<ITodoListItemRepository>();
        }
    }
}
