using System;
using System.Collections.Generic;

namespace EFCore_BloggingApp.Models;

public partial class Comment
{
    public int Commentid { get; set; }

    public int Postid { get; set; }

    public string Text { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Post Post { get; set; } = null!;
}
