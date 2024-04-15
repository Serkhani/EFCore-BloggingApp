using EFCore_BloggingApp.Models;
using Microsoft.EntityFrameworkCore;


namespace EFCore_BloggingApp;

public class CommentManager
{
    private readonly BlogdbContext _context;

    public CommentManager(BlogdbContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetAllComments()
    {
        var comments = await _context.Comments.ToListAsync();
        return comments;
    }
    public async Task<Comment> GetComment(int CommentId)
    {
        var comment = await _context.Comments.FindAsync(CommentId);
        if (comment == null)
        {
            throw new Exception("The comment does not exist");
        }
        return comment;
    }
    public async Task<int> CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment.Commentid;
    }
    public async Task UpdateComment(int CommentId, Comment updatedComment)
    {
        if (CommentId != updatedComment.Commentid)
            throw new Exception("The ids do not match");
        var comment = await _context.Comments.FindAsync(CommentId);

        if (comment == null)
            throw new Exception("The comment does not exist");
        comment.Text = updatedComment.Text;
        _context.Entry(comment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteComment(int CommentId)
    {
        var comment = await _context.Comments.FindAsync(CommentId);
        if (comment == null)
            throw new Exception("The comment does not exist");
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

}
