using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Interfaces;
using Snowly.Application.MessageSecure;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.MessageCommands.CreateMessage
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, ApplicationHandlerResponse<CreateMessageResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly MessageCrypto _messageCrypto;

        public CreateMessageHandler(IUserRepository userRepository, IMessageRepository messageRepository, MessageCrypto messageCrypto)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _messageCrypto = messageCrypto;
        }

        public async Task<ApplicationHandlerResponse<CreateMessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await _userRepository.GetUserByIdAsync(request.SenderId, cancellationToken);
            if (sender == null)
                return ApplicationHandlerResponse<CreateMessageResponse>.Fail(MessageMessages.SenderNotFound);

            var receiver = await _userRepository.GetUserByIdAsync(request.ReceiverId, cancellationToken);
            if (receiver == null)
                return ApplicationHandlerResponse<CreateMessageResponse>.Fail(MessageMessages.ReceiverNotFound);

            Message newMessage = new Message 
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Content = _messageCrypto.Encrypt(request.Content),
                SenderUser = sender,
                ReceiverUser = receiver,
                IsRead = false
            };
            Message addMessageResult = await _messageRepository.AddMessageAsync(newMessage, cancellationToken).ConfigureAwait(false);

            CreateMessageResponse createMessageResponse = new CreateMessageResponse
            {
                Id = newMessage.Id,
                Content = request.Content,
                SenderId = newMessage.SenderId,
                ReceiverId = newMessage.ReceiverId,
                IsRead = newMessage.IsRead,
                ReadAt = newMessage.ReadAt,
                CreatedAt = addMessageResult.CreateDate
            };
            return ApplicationHandlerResponse<CreateMessageResponse>.Ok(createMessageResponse, MessageMessages.MessageSendSuccess);
        }
    }
}
