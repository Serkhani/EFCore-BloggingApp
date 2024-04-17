namespace tests;
using System.Threading.Tasks;
using EFCore_BloggingApp;
using EFCore_BloggingApp.Models;
using Moq;
using Xunit;
using AutoFixture;

public class PostManagerTests
{
    private readonly Mock<BlogdbContext> _mockContext;
    private readonly PostManager _postManager;
    private readonly Fixture _fixture;

    public PostManagerTests()
    {
        _mockContext = new Mock<BlogdbContext>();
        _postManager = new PostManager(_mockContext.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
.ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(recursionDepth: 1));
    }

    [Fact]
    public async Task GetPostById_ExistingId_ReturnsPost()
    {
        // Arrange
        var postId = 0;
        var post = _fixture.Create<Post>();
        post.Postid = postId;
        _mockContext.Setup(x => x.Posts.FindAsync(postId)).ReturnsAsync(post);

        // Act
        var result = await _postManager.GetPost(postId);

        // Assert
        Assert.Equal(post, result);
    }

    [Fact]
    public async Task DeletePost_ExistingPostId_DeletesPost()
    {
        // Arrange
        var postId = 1;
        var post = _fixture.Create<Post>();
        post.Postid = postId;
        _mockContext.Setup(x => x.Posts.FindAsync(postId)).ReturnsAsync(post);

        // Act
        await _postManager.DeletePost(postId);

        // Assert
        _mockContext.Verify(x => x.Posts.Remove(post), Times.Once);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task CreatePost_ValidPost_ReturnsPostId()
    {
        // Arrange
        var post = _fixture.Create<Post>();
        _mockContext.Setup(x => x.Posts.Add(It.IsAny<Post>())).Verifiable();
        _mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var postId = await _postManager.CreatePost(post);

        // Assert
        Assert.True(postId > 0);
        _mockContext.Verify(x => x.Posts.Add(post), Times.Once);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    

}
public class CommentManagerTests
{
    private readonly Mock<BlogdbContext> _mockContext;
    private readonly CommentManager _commentManager;
    private readonly Fixture _fixture;

    public CommentManagerTests()
    {
        _mockContext = new Mock<BlogdbContext>();
        _commentManager = new CommentManager(_mockContext.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
.ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(recursionDepth: 1));
    }

    [Fact]
    public async Task GetCommentById_ExistingId_ReturnsComment()
    {
        // Arrange
        var postId = 0;
        var post = _fixture.Create<Comment>();
        post.Commentid = postId;
        _mockContext.Setup(x => x.Comments.FindAsync(postId)).ReturnsAsync(post);

        // Act
        var result = await _commentManager.GetComment(postId);

        // Assert
        Assert.Equal(post, result);
    }

    [Fact]
    public async Task DeleteComment_ExistingCommentId_DeletesComment()
    {
        // Arrange
        var postId = 1;
        var post = _fixture.Create<Comment>();
        post.Commentid = postId;
        _mockContext.Setup(x => x.Comments.FindAsync(postId)).ReturnsAsync(post);

        // Act
        await _commentManager.DeleteComment(postId);

        // Assert
        _mockContext.Verify(x => x.Comments.Remove(post), Times.Once);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task CreateComment_ValidComment_ReturnsCommentId()
    {
        // Arrange
        var post = _fixture.Create<Comment>();
        _mockContext.Setup(x => x.Comments.Add(It.IsAny<Comment>())).Verifiable();
        _mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var postId = await _commentManager.CreateComment(post);

        // Assert
        Assert.True(postId > 0);
        _mockContext.Verify(x => x.Comments.Add(post), Times.Once);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    

}
