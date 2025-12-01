using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.MessageCommands.DeleteMessage
{
    public sealed class DeleteMessageResponse
    {
        public bool Success { get; set; }
        public Message Message { get; set; } = null!;
    }
}
