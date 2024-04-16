namespace EFCore_BloggingApp.Controllers;

using EFCore_BloggingApp.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{

    private readonly BlogdbContext _context;
    PostManager postManager;

    public PostController(BlogdbContext context)
    {
        _context = context;
        postManager = new PostManager(_context);
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var tasks = await postManager.GetAllPosts();
        return Ok(tasks);
    }

    [HttpGet("{PostId:int}")]
    public async Task<IActionResult> GetPost(int PostId)
    {
        try
        {
            var task = await postManager.GetPost(PostId);
            return Ok(task);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(Post post)
    {
        var postId = await postManager.CreatePost(post);
        return CreatedAtAction(nameof(GetPost), new { PostId = postId }, post);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePost(int PostId, Post updatedPost)
    {
        try
        {
            await postManager.UpdatePost(PostId, updatedPost);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePost(int PostId)
    {
        try
            {
                await postManager.DeletePost(PostId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
    }

}

