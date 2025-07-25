namespace todolist.Pages.Goals;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    [BindProperty]
    public Goal Goal { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();

        Goal? goal = await _db.Goals
            .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
        if (goal == null) return NotFound();

        Goal = goal;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (!ModelState.IsValid) return Page();

        string userId = UserId();

        Goal? goal = await _db.Goals
            .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
        if (goal == null) return NotFound();

        goal.Name = Goal.Name;
        await _db.SaveChangesAsync();

        return LocalRedirect($"/Goals/Show/{Goal.Id}");
    }
}