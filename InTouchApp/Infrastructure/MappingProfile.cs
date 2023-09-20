using AutoMapper;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<SignUpDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, IncludeUserDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
            CreateMap<Post, PostDto>();
            CreateMap<User, FriendDto>();
            CreateMap<User, AccountDto>();
            CreateMap<UpdateAccountDto, User>();
            CreateMap<UpdateAddressDto, Address>();
            CreateMap<CreateCommentDto, PostComment>();
            CreateMap<Comment, IncludeCommentDto>();
            CreateMap<Reaction, IncludeReactionDto>();
            CreateMap<UpdateCommentDto, PostComment>();
            CreateMap<CreateReactionDto, PostReaction>();
            CreateMap<CreateReactionDto, CommentReaction>();
            CreateMap<UpdateReactionDto, PostReaction>();
            CreateMap<UpdateReactionDto, CommentReaction>();
            CreateMap<SendMessageDto, Message>();
            CreateMap<Message, MessageDto>();
            CreateMap<UserPhoto, IncludeUserPhotoDto>();
        }
    }
}
