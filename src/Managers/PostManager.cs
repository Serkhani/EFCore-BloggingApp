using EFCore_BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_BloggingApp;

public class PostManager
{

    private readonly BlogdbContext _context;

    public PostManager(BlogdbContext context)
    {
        _context = context;
    }
    public async Task<List<Post>> GetAllPosts()
    {
        var posts = await _context.Posts.ToListAsync();
        return posts;
    }
    public async Task<Post> GetPost(int PostId)
    {
        var post = await _context.Posts.FindAsync(PostId);
        if (post == null)
        {
            throw new Exception("The post does not exist");
        }
        return post;
    }
    public async Task<int> CreatePost(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post.Postid;
    }
    public async Task UpdatePost(int PostId, Post updatedPost)
    {
        if (PostId != updatedPost.Postid)
            throw new Exception("The ids do not match");
        var post = await _context.Posts.FindAsync(PostId);

        if (post == null)
            throw new Exception("The post does not exist");
        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;
        post.Comments = updatedPost.Comments;
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeletePost(int PostId)
    {
        var post = await _context.Posts.FindAsync(PostId);
        if (post == null)
            throw new Exception("The post does not exist");
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}