using System.Threading.Tasks;
using AI_Onboarding.ViewModels.DocumentModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class DocumentViewModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var file = bindingContext.HttpContext.Request.Form.Files.GetFile("File");

        var fileTypeId = bindingContext.ValueProvider.GetValue("FileTypeId").FirstOrDefault();

        var values = bindingContext.ValueProvider.GetValue("QuestionsAnswers").Values;

        var questionAnswersList = new List<QuestionAnswer>();

        foreach (var value in values)
        {
            var questionsAnswers = value;

            var questionAnswer = JsonConvert.DeserializeObject<QuestionAnswer>(questionsAnswers);
            questionAnswersList.Add(questionAnswer);
        }

        var model = new DocumentViewModel
        {
            File = file,
            FileTypeId = int.Parse(fileTypeId),
            QuestionsAnswers = questionAnswersList
        };

        bindingContext.Result = ModelBindingResult.Success(model);

        return Task.CompletedTask;
    }
}
