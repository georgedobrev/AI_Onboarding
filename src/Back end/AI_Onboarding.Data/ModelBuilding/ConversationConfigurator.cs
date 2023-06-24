using System;
using AI_Onboarding.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Onboarding.Data.ModelBuilding
{
    public class ConversationConfigurator : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)

        {
            builder.Property(x => x.UserId).IsRequired();

            builder
            .HasOne(x => x.User)
            .WithMany(x => x.Conversations)
            .HasForeignKey(x => x.UserId);

            builder.OwnsMany(x => x.QuestionAnswers);
        }
    }
}

