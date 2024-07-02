using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;
using Xunit;

namespace Todo.Tests
{
    public class TodoItemRankingTests
    {
        private readonly IdentityUser testUser = new IdentityUser { Id = Guid.NewGuid().ToString(), UserName = "TestUser" };

        [Fact]
        public void TodoItems_ShouldBeSortedByRank()
        {
            // Arrange
            var todoList = new TestTodoListBuilder(testUser, "Test List")
                .WithItem("High Priority Task", Importance.High, 1)
                .WithItem("Medium Priority Task", Importance.Medium, 2)
                .WithItem("Low Priority Task", Importance.Low, 3)
                .Build();

            // Act
            var sortedItems = todoList.Items.OrderBy(item => item.Rank).ToList();

            // Assert
            Assert.Equal(3, sortedItems.Count);
            Assert.Equal("High Priority Task", sortedItems[0].Title);
            Assert.Equal("Medium Priority Task", sortedItems[1].Title);
            Assert.Equal("Low Priority Task", sortedItems[2].Title);
        }

        [Fact]
        public void TodoItems_WithSameRank_ShouldBeGroupedTogether()
        {
            // Arrange
            var todoList = new TestTodoListBuilder(testUser, "Test List")
                .WithItem("Task 1", Importance.Medium, 1)
                .WithItem("Task 2", Importance.Medium, 1)
                .WithItem("Task 3", Importance.Medium, 2)
                .Build();

            // Act
            var sortedItems = todoList.Items.OrderBy(item => item.Rank).ToList();

            // Assert
            Assert.Equal(3, sortedItems.Count);

            // Check that the first two items have rank 1
            Assert.Equal(1, sortedItems[0].Rank);
            Assert.Equal(1, sortedItems[1].Rank);

            // Check that the first two items are either Task 1 or Task 2
            Assert.Contains(sortedItems[0].Title, new[] { "Task 1", "Task 2" });
            Assert.Contains(sortedItems[1].Title, new[] { "Task 1", "Task 2" });

            // Check that the first two items are different
            Assert.NotEqual(sortedItems[0].Title, sortedItems[1].Title);

            // Check that the last item is Task 3 with rank 2
            Assert.Equal("Task 3", sortedItems[2].Title);
            Assert.Equal(2, sortedItems[2].Rank);
        }
    }

}