using Moq;
using FluentAssertions;
using AutoMapper;
using ReviewVault.Application.Service;
using ReviewVault.Application.DTOs.ResponseDTOs;
using ReviewVault.Domain.Interfaces;
using ReviewVault.Domain.Models;

namespace ReviewVault.Tests.Services;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _postRepoMock;
    private readonly Mock<ICategoryRepository> _categoryRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly PostService _sut;

    public PostServiceTests()
    {
        _postRepoMock = new Mock<IPostRepository>();
        _categoryRepoMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _sut = new PostService(
            _postRepoMock.Object,
            _categoryRepoMock.Object,
            _mapperMock.Object
        );
    }

    // ======= GetByIdAsync =======

    [Fact]
    public async Task GetByIdAsync_PostExists_ReturnsMappedDto()
    {
        var post = new Post { Id = 1, Title = "AOT Review" };
        var dto = new PostResponseDTO { Id = 1, Title = "AOT Review", Slug = "aot-review" };

        _postRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(post);
        _mapperMock.Setup(m => m.Map<PostResponseDTO>(post)).Returns(dto);

        var result = await _sut.GetByIdAsync(1);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Title.Should().Be("AOT Review");
    }

    [Fact]
    public async Task GetByIdAsync_NotFound_ThrowsException()
    {
        _postRepoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Post?)null);

        var act = () => _sut.GetByIdAsync(999);

        await act.Should().ThrowAsync<Exception>().WithMessage("Post not found");
    }

    // ======= GetBySlugAsync =======

    [Fact]
    public async Task GetBySlugAsync_ValidSlug_ReturnsMappedDto()
    {
        var post = new Post { Id = 1, Slug = "aot-review" };
        var dto = new PostResponseDTO { Id = 1, Slug = "aot-review", Title = "AOT Review" };

        _postRepoMock.Setup(r => r.GetBySlugAsync("aot-review")).ReturnsAsync(post);
        _mapperMock.Setup(m => m.Map<PostResponseDTO>(post)).Returns(dto);

        var result = await _sut.GetBySlugAsync("aot-review");

        result.Slug.Should().Be("aot-review");
    }

    [Fact]
    public async Task GetBySlugAsync_NotFound_ThrowsException()
    {
        _postRepoMock.Setup(r => r.GetBySlugAsync("nonexistent")).ReturnsAsync((Post?)null);

        var act = () => _sut.GetBySlugAsync("nonexistent");

        await act.Should().ThrowAsync<Exception>().WithMessage("Post not found");
    }

    // ======= GetAllPublishedAsync =======

    [Fact]
    public async Task GetAllPublished_ReturnsMappedDtos()
    {
        var posts = new List<Post>
        {
            new Post { Id = 1, Title = "Post 1" },
            new Post { Id = 2, Title = "Post 2" }
        };
        var dtos = new List<PostResponseDTO>
        {
            new PostResponseDTO { Id = 1, Title = "Post 1" },
            new PostResponseDTO { Id = 2, Title = "Post 2" }
        };

        _postRepoMock.Setup(r => r.GetAllPublishedAsync(1, 6)).ReturnsAsync(posts);
        _mapperMock.Setup(m => m.Map<IEnumerable<PostResponseDTO>>(posts)).Returns(dtos);

        var result = await _sut.GetAllPublishedAsync(1, 6);

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllPublished_NoPosts_ReturnsEmpty()
    {
        var empty = new List<Post>();
        _postRepoMock.Setup(r => r.GetAllPublishedAsync(1, 6)).ReturnsAsync(empty);
        _mapperMock.Setup(m => m.Map<IEnumerable<PostResponseDTO>>(empty))
            .Returns(new List<PostResponseDTO>());

        var result = await _sut.GetAllPublishedAsync(1, 6);

        result.Should().BeEmpty();
    }

    // ======= SearchAsync =======

    [Fact]
    public async Task SearchAsync_MatchFound_ReturnsDtos()
    {
        var posts = new List<Post> { new Post { Id = 1, Title = "Naruto" } };
        var dtos = new List<PostResponseDTO> { new PostResponseDTO { Id = 1, Title = "Naruto" } };

        _postRepoMock.Setup(r => r.SearchAsync("naruto", 1, 6)).ReturnsAsync(posts);
        _mapperMock.Setup(m => m.Map<IEnumerable<PostResponseDTO>>(posts)).Returns(dtos);

        var result = await _sut.SearchAsync("naruto", 1, 6);

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task SearchAsync_NoMatch_ReturnsEmpty()
    {
        var empty = new List<Post>();
        _postRepoMock.Setup(r => r.SearchAsync("xyz", 1, 6)).ReturnsAsync(empty);
        _mapperMock.Setup(m => m.Map<IEnumerable<PostResponseDTO>>(empty))
            .Returns(new List<PostResponseDTO>());

        var result = await _sut.SearchAsync("xyz", 1, 6);

        result.Should().BeEmpty();
    }

    // ======= SearchCountAsync =======

    [Fact]
    public async Task SearchCountAsync_ReturnsCount()
    {
        _postRepoMock.Setup(r => r.SearchCountAsync("naruto")).ReturnsAsync(5);

        var result = await _sut.SearchCountAsync("naruto");

        result.Should().Be(5);
    }

    // ======= GetTotalCountAsync =======

    [Fact]
    public async Task GetTotalCountAsync_ReturnsPublishedCount()
    {
        _postRepoMock.Setup(r => r.GetTotalCountAsync(true)).ReturnsAsync(13);

        var result = await _sut.GetTotalCountAsync();

        result.Should().Be(13);
    }

    // ======= DeleteAsync =======

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        await _sut.DeleteAsync(1);

        _postRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}