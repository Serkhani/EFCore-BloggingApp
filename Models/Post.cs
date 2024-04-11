namespace EFCore_BloggingApp.Models;

public partial class Post
{
    public int Postid { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
