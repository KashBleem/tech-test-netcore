using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IGravatarService _gravatarService;

        public TodoItemController(ApplicationDbContext dbContext, IGravatarService gravatarService)
        {
            this.dbContext = dbContext;
            _gravatarService = gravatarService;
        }

        [HttpGet]
        public IActionResult Create(int todoListId)
        {
            var todoList = dbContext.SingleTodoList(todoListId);
            var fields = TodoItemCreateFieldsFactory.Create(todoList, User.Id(), todoList.Owner);
            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }
            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance, fields.Rank);
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return RedirectToListDetail(fields.TodoListId);
        }

        [HttpGet]
        public IActionResult Edit(int todoItemId)
        {
            var todoItem = dbContext.SingleTodoItem(todoItemId);
            var fields = TodoItemEditFieldsFactory.Create(todoItem);
            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoItemEditFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }
            var todoItem = dbContext.SingleTodoItem(fields.TodoItemId);
            TodoItemEditFieldsFactory.Update(fields, todoItem);
            dbContext.Update(todoItem);
            await dbContext.SaveChangesAsync();
            return RedirectToListDetail(todoItem.TodoListId);
        }

        //full disclosure, if i had time i would MUCH rather stick this in its own Swagger API
        [HttpPost]
        [AllowAnonymous]
        [Route("api/TodoItem/Create")]
        public async Task<IActionResult> CreateApi([FromBody] CreateTodoItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoList = dbContext.SingleTodoList(request.TodoListId);
            var fields = TodoItemCreateFieldsFactory.Create(todoList, User.Id(), todoList.Owner);
            fields.Title = request.Title;
            fields.Importance = request.Importance;
            fields.Rank = request.Rank;

            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance, fields.Rank);
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();

            var responsibleParty = await dbContext.Users.FindAsync(fields.ResponsiblePartyId);
            var gravatarProfile = await GetGravatarProfile(responsibleParty.Email);

            var viewModel = new TodoItemSummaryViewmodel(
                item.TodoItemId,
                item.Title,
                item.IsDone,
                new UserSummaryViewmodel(responsibleParty.UserName, responsibleParty.Email),
                item.Importance,
                item.Rank
            );

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("api/TodoItem/SetRank")]
        public async Task<IActionResult> SetRank([FromBody] SetRankRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoItem = await dbContext.TodoItems.FindAsync(request.TodoItemId);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Rank = request.NewRank;
            await dbContext.SaveChangesAsync();

            return Ok();
        }

    private RedirectToActionResult RedirectToListDetail(int fieldsTodoListId)
        {
            return RedirectToAction("Detail", "TodoList", new { todoListId = fieldsTodoListId });
        }

        private async Task<GravatarProfile> GetGravatarProfile(string email)
        {
            return await _gravatarService.GetProfileDetailsAsync(email);
        }
    }

    public class CreateTodoItemRequest
    {
        public string Title { get; set; }
        public Importance Importance { get; set; }
        public int Rank { get; set; }
        public int TodoListId { get; set; }
    }
    public class SetRankRequest
    {
        public int TodoItemId { get; set; }
        public int NewRank { get; set; }
    }

}