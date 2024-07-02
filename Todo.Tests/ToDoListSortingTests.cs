using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.Models.TodoLists;
using Todo.Models.TodoItems;
using Xunit;

namespace Todo.Tests
{
    public class TodoListSortingTests
    {
        private TodoItemSummaryViewmodel CreateTodoItemSummaryViewmodel(int todoItemId, string title, bool isDone, UserSummaryViewmodel responsibleParty, Importance importance)
        {
            return new TodoItemSummaryViewmodel(todoItemId, title, isDone, responsibleParty, importance);
        }

        [Fact]
        public void Items_Should_Be_Sorted_By_Importance()
        {
            var responsibleParty = new UserSummaryViewmodel("user1","mike@aol.com");
            var items = new List<TodoItemSummaryViewmodel>
            {
                CreateTodoItemSummaryViewmodel(1, "Low Importance Task", false, responsibleParty, Importance.Low),
                CreateTodoItemSummaryViewmodel(2, "High Importance Task", false, responsibleParty, Importance.High),
                CreateTodoItemSummaryViewmodel(3, "Medium Importance Task", false, responsibleParty, Importance.Medium)
            };

            var viewModel = new TodoListDetailViewmodel(1, "Test List", items);

            // Act
            var sortedItems = viewModel.Items.OrderBy(item => item.Importance).ToList();

            // Assert
            Assert.Equal(Importance.High, sortedItems[0].Importance);
            Assert.Equal(Importance.Medium, sortedItems[1].Importance);
            Assert.Equal(Importance.Low, sortedItems[2].Importance);
        }
    }
}
