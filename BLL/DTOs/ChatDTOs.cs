using System;

namespace BLL.DTOs
{
    public class SendMessageDTO
    {
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = null!;
    }

    public class MessageResponseDTO
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class ChatHistoryRequestDTO
    {
        public Guid PartnerId { get; set; }
    }
} 