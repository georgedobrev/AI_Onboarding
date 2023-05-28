using System;

public interface IExceptionFilter
{
    void OnException(ExceptionContext context);
}
