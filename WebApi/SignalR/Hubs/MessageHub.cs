﻿using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModel.MessageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApi.AutoMapper.Interface;

namespace WebApi.SignalR.Hubs;

[Authorize]
public class MessageHub : Hub
{
    private readonly IMessageService _messageService;
    private readonly IChatRoomService _chatRoomService;
    private readonly IViewModelMapper<MessageSendViewModel, Message> _sendMessageMapper;
    private readonly IViewModelMapper<Message, MessageGetViewModel> _getMessageMapper;
    public MessageHub(
        IMessageService messageService, 
        IChatRoomService chatRoomService, 
        IViewModelMapper<MessageSendViewModel, Message> sendMessageMapper,
        IViewModelMapper<Message, MessageGetViewModel> getMessageMapper)
    {
        _messageService = messageService;
        _chatRoomService = chatRoomService;
        _sendMessageMapper = sendMessageMapper;
        _getMessageMapper = getMessageMapper;
    }

    public async Task SendPrivateMessage(MessageSendViewModel message)
    {
        int senderId = Int32.Parse(Context.UserIdentifier);
        
        var chatRoom = await _chatRoomService.EnsurePrivateRoomCreatedAsync(senderId, message.ReceiverId);

        var messageMap = _sendMessageMapper.Map(message);
        messageMap.ChatRoomId = chatRoom.Id;
        messageMap.SenderId = senderId;
        messageMap.SentAt = DateTime.Now;
        await _messageService.CreateAsync(messageMap);
            
        var messageGetMap = _getMessageMapper.Map(messageMap);
        await Clients.User(message.ReceiverId.ToString())
            .SendAsync("getMessage", messageGetMap);
    }

    public async Task ReadMessage(int messageId)
    {
        int readerId = Int32.Parse(Context.UserIdentifier);
        await _messageService.ReadMessageAsync(readerId, messageId);
    }

}