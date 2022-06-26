﻿using Core.Entities;
using Core.ViewModels.CommentViewModels;
using WebApi.AutoMapper.Interface;

namespace WebApi.AutoMapper.CommentMappers;

public class ReadCommentMapper : IViewModelMapper<Comment, ReadCommentViewModel>
{
    public ReadCommentViewModel Map(Comment source)
    {
        return new ReadCommentViewModel()
        {
    /*public int Id { get; set; }
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Edited { get; set; }
    public int ArticleId { get; set; }*/
            Id = source.Id,
            AuthorId = source.AuthorId,
            AuthorName = source.Author!.FirstName + " " + source.Author!.LastName,
            CreatedAt = source.CreatedAt,
            Edited = source.Edited,
            ArticleId = source.ArticleId,
            Content = source.Content
        };
    }
}