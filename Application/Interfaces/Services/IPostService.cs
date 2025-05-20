using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<Result<PostDto>> CreatePost(MakePostRequestModel model);
        Task<Result<PostDto?>> ViewPost(Guid postId);
        Task<Result<PostDto>> EditPost(EditPostRequestModel edit);
        Task<Result<PostDto>> DeletePost(DeletePostRequestModel delete);
        Task<Result<ICollection<PostDto>>> ViewAllPosts(Guid mediaUserId);
        Task<Result<ICollection<PostDto>>> ViewAllPosts();
    }
}
