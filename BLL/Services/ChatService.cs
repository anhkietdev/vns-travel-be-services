using DAL.Models;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public interface IChatService
    {
        Task<Message> SendMessageAsync(Guid senderId, Guid receiverId, string content);
        Task<List<Message>> GetChatHistoryAsync(Guid userId1, Guid userId2);
        Task<List<Message>> GetRecentChatsAsync(Guid userId);
    }

    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Message> SendMessageAsync(Guid senderId, Guid receiverId, string content)
        {
            var message = new Message
            {
                MessageId = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Message.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetChatHistoryAsync(Guid userId1, Guid userId2)
        {
            var messages = await _unitOfWork.Message.GetAllAsync(
                filter: m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                            (m.SenderId == userId2 && m.ReceiverId == userId1),
                orderBy: m => m.CreatedAt,
                sortDirection: SortDirection.Ascending
            );
            return messages.ToList();
        }

        public async Task<List<Message>> GetRecentChatsAsync(Guid userId)
        {
            var messages = await _unitOfWork.Message.GetAllAsync(
                filter: m => m.SenderId == userId || m.ReceiverId == userId,
                orderBy: m => m.CreatedAt,
                sortDirection: SortDirection.Descending
            );
            return messages.ToList();
        }
    }
} 