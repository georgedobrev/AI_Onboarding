using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace Custom_Exception_Filter.Filters
{
    public interface IExceptionFilter
    {
        void OnException(ExceptionContext context);

    }
}
