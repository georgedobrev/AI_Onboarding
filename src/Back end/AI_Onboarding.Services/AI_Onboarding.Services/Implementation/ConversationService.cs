using System;
using System.Security.Claims;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AI_Onboarding.Services.Implementation
{
    public class ConversationService : IConversationService
    {
        private readonly IRepository<Conversation> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConversationService(IRepository<Conversation> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToConversation(int? id, string question, string answer)
        {
            int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);

            if (id is null)
            {
                _repository.Add(new Conversation
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
                });
            }
            else
            {
                var conversation = _repository.Find((int)id);
                conversation.QuestionAnswers.Add(new QuestionAnswer
                {
                    Answer = answer,
                    Question = question
                });

                _repository.Update(conversation);
            }

            _repository.SaveChanges();
        }
    }
}

