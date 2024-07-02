using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemCreateFields
    {
        public int TodoListId { get; set; }
        public string Title { get; set; }
        public string TodoListTitle { get; set; }
        public string ResponsiblePartyId { get; set; }
        public IdentityUser ResponsibleParty { get; set; }
        public Importance Importance { get; set; } = Importance.Medium;

        public TodoItemCreateFields() { }

        public TodoItemCreateFields(int todoListId, string todoListTitle, string responsiblePartyId, IdentityUser responsibleParty)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            ResponsiblePartyId = responsiblePartyId;
            ResponsibleParty = responsibleParty;
        }
    }
}