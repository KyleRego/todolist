using System.ComponentModel.DataAnnotations;

namespace todolist.Models;

public class Goal : Entity
{
    [Required, MinLength(10)]
    public string Name { get; set; } = null!;
    public List<ProgressNote> ProgressNotes { get; set; } = [];
    public List<Todo> Todos { get; set; } = [];
}
