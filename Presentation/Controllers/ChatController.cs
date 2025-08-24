using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<ActionResult<MessageResponseDTO>> SendMessage([FromBody] SendMessageDTO messageDto)
        {
            var senderId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var message = await _chatService.SendMessageAsync(senderId, messageDto.ReceiverId, messageDto.Content);
            
            return Ok(new MessageResponseDTO
            {
                MessageId = message.MessageId,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                CreatedAt = message.CreatedAt
            });
        }

        [HttpGet("history/{partnerId}")]
        public async Task<ActionResult<List<MessageResponseDTO>>> GetChatHistory(Guid partnerId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var messages = await _chatService.GetChatHistoryAsync(userId, partnerId);
            
            return Ok(messages.Select(m => new MessageResponseDTO
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                CreatedAt = m.CreatedAt
            }));
        }

        [HttpGet("recent")]
        public async Task<ActionResult<List<MessageResponseDTO>>> GetRecentChats()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var messages = await _chatService.GetRecentChatsAsync(userId);
            
            return Ok(messages.Select(m => new MessageResponseDTO
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                CreatedAt = m.CreatedAt
            }));
        }
    }
} 