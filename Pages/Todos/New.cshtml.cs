namespace todolist.Pages.Todos;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public Todo Todo { get; set; } = null!;

    [BindProperty]
    public string? ReturnUrl { get; set; } = null;

    public IActionResult OnGet()
    {
        string userId = UserId();

        Todo = new() { Description = "", UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        string userId = UserId();
        if (userId != Todo.UserId) return BadRequest();

        Todo todo = new()
        {
            UserId = userId,
            Description = Todo.Description,
            OnDate = Todo.OnDate,
            Completed = Todo.Completed,
            ProjectId = Todo.ProjectId,
            StartTime = Todo.StartTime,
            EndTime = Todo.EndTime,
            GoalId = Todo.GoalId
        };

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();

        if (ReturnUrl != null)
            return LocalRedirect(ReturnUrl);

        return LocalRedirect("/Todos/Index");
    }
}