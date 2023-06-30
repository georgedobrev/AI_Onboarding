﻿using System.Security.Claims;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.ConversationModels;
using AI_Onboarding.ViewModels.ResponseModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AI_Onboarding.Services.Implementation
{
    public class ConversationService : IConversationService
    {
        private readonly IRepository<Conversation> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ConversationService(IRepository<Conversation> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public int? AddToConversation(int? id, string question, string answer)
        {
            int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);

            int? conversationId = null;

            if (id is null)
            {
                var newConversation = new Conversation
                {
                    UserId = userId,
                    QuestionAnswers = new List<QuestionAnswer>
                    {
                        new QuestionAnswer
                        {
                            Answer = answer,
                            Question = question
                        }
                    }
                };

                _repository.Add(newConversation);
                _repository.SaveChanges();

                conversationId = newConversation.Id;
            }
            else
            {
                var conversation = _repository.Find((int)id);

                if (conversation?.UserId == userId)
                {
                    conversation.QuestionAnswers.Add(new QuestionAnswer
                    {
                        Answer = answer,
                        Question = question
                    });

                    _repository.Update(conversation);
                    _repository.SaveChanges();

                    conversationId = conversation.Id;
                }
            }

            return conversationId;
        }

        public BaseResponseViewModel DeleteConversation(int id)
        {
            int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);

            var conversation = _repository.Remove(id);

            if (conversation is not null && conversation.UserId == userId)
            {
                _repository.SaveChanges();
                return new BaseResponseViewModel { Success = true };
            }
            else if (conversation is not null)
            {
                return new BaseResponseViewModel { Success = false, ErrorMessage = "Unauthorized" };
            }
            else
            {
                return new BaseResponseViewModel { Success = false, ErrorMessage = "Conversation doesn't exist!" };
            }
        }

        public ConversationDTO? GetConversation(int id)
        {
            int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);

            return _mapper.Map<ConversationDTO>(_repository.FindByCondition(x => x.Id == id && x.UserId == userId));
        }

        public UserConversationsViewModel GetUserConversations()
        {
            int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);

            var userConversations = new UserConversationsViewModel();

            foreach (var conversation in _repository.FindAllByCondition(c => c.UserId == userId).ToList())
            {
                userConversations.Conversations.Add(_mapper.Map<ConversationDTO>(conversation));
            }

            return userConversations;
        }
    }
}

