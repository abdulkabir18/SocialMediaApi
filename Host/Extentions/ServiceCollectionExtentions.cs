using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Persistense.Repositories;

namespace Host.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMediaUserRepository, MediaUserRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IReplyRepository, ReplyRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMediaUserService, MediaUserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IReplyService, ReplyService>();

            return services;
        }
    }
}
