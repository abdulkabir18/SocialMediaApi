using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.External;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUploadService _uploadService;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository,ICommentRepository commentRepository,ILikeRepository likeRepository, ICurrentUser currentUser, IUploadService uploadService, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _currentUser = currentUser;
            _uploadService = uploadService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PostDto>> CreatePost(MakePostRequestModel model)
        {
            var currentUser = await _currentUser.GetCurrentMediaUser();
            if (currentUser.Data == null)
            {
                return new Result<PostDto>
                {
                    Message = "Post creation failed",
                    Data = null,
                    Status = false
                };
            }

            if (model.ContentText == null && model.Files.Count == 0) return new Result<PostDto> { Message = "Error: Required", Data = null, Status = false };


            //if (model.ContentType != ContentType.text && model.Content != null)
            //{
            //    var post = new Post
            //    {
            //        Content = await _uploadService.UploadFileAsync(model.Content)!,
            //        ContentType = model.ContentType,
            //        CreatedBy = currentUser.Data.FullName,
            //        PosterId = currentUser.Data.Id,
            //        Title = model.Title
            //    };

            //    await _postRepository.AddAsync(post);
            //    await _unitOfWork.SaveAsync();

            //    return new Result<PostDto>
            //    {
            //        Message = "Post Created Successfully",
            //        Data = new PostDto { Content = post.Content,ContentType = post.ContentType, CreatedBy = post.CreatedBy, PosterId = post.PosterId, Title = post.Title,CommentCount = 0,LikeCount = 0, Id = post.Id,DateCreated = post.DateCreated},
            //        Status = true
            //    };
            //}
            //else if(model.ContentType == ContentType.text && model.ContentText != null)
            //{
            //    var post = new Post
            //    {
            //        Content = model.ContentText,
            //        ContentType = model.ContentType,
            //        CreatedBy = currentUser.Data.FullName,
            //        PosterId = currentUser.Data.Id,
            //        Title = model.Title
            //    };

            //    await _postRepository.AddAsync(post);
            //    await _unitOfWork.SaveAsync();

            //    return new Result<PostDto>
            //    {
            //        Message = "Post Created Successfully",
            //        Data = new PostDto { Content = post.Content, ContentType = post.ContentType, CreatedBy = post.CreatedBy, PosterId = post.PosterId, Title = post.Title, CommentCount = 0, LikeCount = 0, Id = post.Id, DateCreated = post.DateCreated},
            //        Status = true
            //    };
            //}

            //return new Result<PostDto>
            //{ Message = "Error: Post creation failed due to some details is not provided",Data = null, Status = false};
        }

        //public async Task<Result<PostDto>> DeletePost(DeletePostRequestModel delete)
        //{
        //    var post = await _postRepository.GetAsync(delete.PostId);
        //    if(post == null)
        //    {
        //        return new Result<PostDto>
        //        { Message = "Error: No post found to delete", Data = null, Status = false };
        //    }

        //    var mediaUser = await _currentUser.GetCurrentMediaUser();
        //    if(mediaUser.Data == null || mediaUser.Data.Id != post.PosterId)
        //    {
        //        return new Result<PostDto>
        //        { Message = "Error: Verification failed", Data = null, Status = false };
        //    }

        //    _postRepository.Delete(post);
        //    await _unitOfWork.SaveAsync();

        //    return new Result<PostDto>
        //    { Message = "Post delete successfully", Status = true, Data = { } };
        //}

        //public async Task<Result<PostDto>> EditPost(EditPostRequestModel edit)
        //{
        //    var post = await _postRepository.GetAsync(edit.PostId);
        //    var mediaUser = await _currentUser.GetCurrentMediaUser();
        //    if(mediaUser.Data == null || post == null) 
        //        return new Result<PostDto> { Message = "Error: Try again", Data = null, Status = false};

        //    if (post.PosterId != mediaUser.Data.Id)
        //        return new Result<PostDto> { Message = "Error: Failed to edit", Status = false, Data = null };

        //    if(!string.IsNullOrEmpty(edit.Title)) { post.Title = edit.Title; }
        //    if(edit.ContentType != ContentType.text && edit.Content != null) 
        //    {
        //        post.Content = await _uploadService.UploadFileAsync(edit.Content)!;
        //        post.ContentType = edit.ContentType;
        //    }
        //    if(edit.ContentType == ContentType.text && !string.IsNullOrEmpty(edit.ContentText))
        //    {
        //        post.ContentType = edit.ContentType;
        //        post.Content = edit.ContentText;
        //    }

        //    if (edit.ContentText == null && edit.Content == null && edit.Title == null)
        //        return new Result<PostDto> { Message = "No details to update with", Data = null, Status = false };

        //    post.DateModified = DateTime.UtcNow;
        //    post.ModifiedBy = mediaUser.Data.FullName;
        //    _postRepository.Update(post);
        //    await _unitOfWork.SaveAsync();

        //    return new Result<PostDto> { Message = "Post update successfully", Data = new PostDto { Content = post.Content, ContentType = post.ContentType, CreatedBy = post.ModifiedBy, PosterId = post.PosterId, Title = post.Title, CommentCount = await _commentRepository.CountAsync(post.Id), Id = post.Id, DateCreated = post.DateCreated, LikeCount = await _likeRepository.CountAsync(l => l.PostId == post.Id && post.IsDeleted != true) }, Status = true };
        //}

        public async Task<Result<ICollection<PostDto>>> ViewAllPosts(Guid mediaUserId)
        {
            var posts = await _postRepository.GetAllAsync(mediaUserId);
            if(posts.Count == 0)
            {
                return new Result<ICollection<PostDto>>
                { Message = "Post not avaliable. Not post created by you",Data = null, Status =false};
            }

            ICollection<PostDto> postDtos = [];

            foreach(var post in posts)
            {
                var postDto = new PostDto
                { 
                    Content = post.Content,
                    ContentType = post.ContentType,
                    CreatedBy = post.CreatedBy,
                    PosterId = post.PosterId,
                    Title = post.Title,
                    CommentCount = await _commentRepository.CountAsync(post.Id),
                    LikeCount = await _likeRepository.CountAsync(l => l.PostId == post.Id && l.IsDeleted != true),
                    Id = post.Id,
                    DateCreated = post.DateCreated
                };
                postDtos.Add(postDto);
            }

            return new Result<ICollection<PostDto>>
            { Message = "Post loading...", Data = postDtos, Status = true };
        }

        public async Task<Result<ICollection<PostDto>>> ViewAllPosts()
        {
            var posts = await _postRepository.GetAllAsync();
            if (posts.Count == 0)
            {
                return new Result<ICollection<PostDto>>
                { Message = "Post not avaliable.", Data = null, Status = false };
            }

            ICollection<PostDto> postDtos = [];

            foreach (var post in posts)
            {
                var postDto = new PostDto
                {
                    Content = post.Content,
                    ContentType = post.ContentType,
                    CreatedBy = post.CreatedBy,
                    PosterId = post.PosterId,
                    Title = post.Title,
                    CommentCount = await _commentRepository.CountAsync(post.Id),
                    LikeCount = await _likeRepository.CountAsync(l => l.PostId == post.Id && l.IsDeleted != true),
                    Id = post.Id,
                    DateCreated = post.DateCreated
                };
                postDtos.Add(postDto);
            }

            return new Result<ICollection<PostDto>>
            { Message = "Post loading...", Data = postDtos, Status = true };
        }

        public async Task<Result<PostDto?>> ViewPost(Guid postId)
        {
            var post = await _postRepository.GetAsync(postId);

            if(post == null)
            {
                return new Result<PostDto?>
                { Message = "Post not found.", Data = null, Status = false };
            }

            return new Result<PostDto?>
            {
                Data = new PostDto { Content = post.Content, ContentType = post.ContentType, CreatedBy = post.CreatedBy, PosterId = post.PosterId, Title = post.Title, CommentCount = await _commentRepository.CountAsync(post.Id), LikeCount = await _likeRepository.CountAsync(l => l.PostId == post.Id && l.IsDeleted != true), Id = post.Id, DateCreated = post.DateCreated},
                Message = "Found",
                Status = true
            };
        }
    }
}
