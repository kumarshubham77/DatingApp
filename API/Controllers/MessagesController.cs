using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using API.Extensions;
using AutoMapper;
using API.Entities;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;
        public MessagesController(IMapper mapper , IUnitOfWork uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();
            if(username == createMessageDto.RecipientUsername.ToLower()) return BadRequest("You cannot message to Yourself");
            var sender = await _uow.UserRepository.GetUserByUsernameAsync(username);
            var recipient = await _uow.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
            if(recipient == null) return NotFound();
            var message = new Message
            {
                Sender  = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            _uow.MessageRepository.AddMessage(message);
            if(await _uow.Complete()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Failed to Send Message");
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams )
        {
            messageParams.Username = User.GetUsername();
            var  messages = await _uow.MessageRepository.GetMessagesForUser(messageParams);
            Response.AddPaginationHeader(messages.CurrentPage,messages.PageSize,messages.TotalCount,messages.TotalPages);
            return messages;
        }

        // [HttpGet("thread/{username}")]
        // public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        // {
        //     var currentUsername = User.GetUsername();
        //     return Ok(await _uow.MessageRepository.GetMessageThread(currentUsername,username));
        // }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUsername();
            var message = await _uow.MessageRepository.GetMessage(id);
            if(message.Sender.UserName != username && message.Recipient.UserName != username) return Unauthorized();
            if(message.Sender.UserName == username) message.SenderDeleted = true;
            if(message.Recipient.UserName == username) message.RecipientDeleted =true;
            if(message.SenderDeleted && message.RecipientDeleted) _uow.MessageRepository.DeleteMessage(message);
            if(await _uow.Complete()) return Ok();
            return BadRequest("Problem deleting the Message");
        }

    }
}