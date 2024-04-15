namespace EFCore_BloggingApp.Controllers;

using EFCore_BloggingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{

    private readonly BlogdbContext _context;

    public PostController(BlogdbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var tasks = await _context.Posts.ToListAsync();
        if (tasks == null)
        {
            return NoContent();
        }
        return Ok(tasks);
    }

    [HttpGet("{PostId:int}")]
public async Task<IActionResult> GetPost(int PostId)
{
    var task = await _context.Posts.FindAsync(PostId);
    if (task == null)
    {
        return NotFound();
    }
    return Ok(task);
}

    [HttpPost]
    public async Task<IActionResult> CreatePost(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPost), new { PostId = post.Postid }, post);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePost(int PostId, Post post)
    {
        if (PostId != post.Postid)
        {
            return BadRequest();
        }
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePost(int PostId)
    {
        var post = await _context.Posts.FindAsync(PostId);
        if (post == null)
        {
            return NotFound();
        }
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}

