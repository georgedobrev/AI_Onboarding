using System;
using AI_Onboarding.Data.Models;
using AutoMapper;

namespace AI_Onboarding.ViewModels.ConversationModels.Profiles
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            CreateMap<Conversation, ConversationInfo>()
                .ForMember(dest => dest.QuestionAnswers, opt => opt.MapFrom(src => src.QuestionAnswers.ToList()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}

