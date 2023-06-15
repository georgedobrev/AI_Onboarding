using AI_Onboarding.ViewModels.DocumentModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

public class DocumentViewModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(DocumentViewModel))
        {
            return new BinderTypeModelBinder(typeof(DocumentViewModelBinder));
        }

        return null;
    }
}
