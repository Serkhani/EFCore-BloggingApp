namespace EFCore_BloggingApp;
using EFCore_BloggingApp.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly BlogdbContext _context;
    CommentManager commentManager;

    public CommentController(BlogdbContext context)
    {
        _context = context;
        commentManager = new CommentManager(_context);
    }

    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var tasks = await commentManager.GetAllComments();
        return Ok(tasks);
    }

    [HttpGet("{CommentId:int}")]
    public async Task<IActionResult> GetComment(int CommentId)
    {
        try
        {
            var task = await commentManager.GetComment(CommentId);
            return Ok(task);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(Comment comment)
    {
        var commentId = await commentManager.CreateComment(comment);
        return CreatedAtAction(nameof(GetComment), new { CommentId = commentId }, comment);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateComment(int CommentId, Comment updatedComment)
    {
        try
        {
            await commentManager.UpdateComment(CommentId, updatedComment);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteComment(int CommentId)
    {
        try
        {
            await commentManager.DeleteComment(CommentId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
