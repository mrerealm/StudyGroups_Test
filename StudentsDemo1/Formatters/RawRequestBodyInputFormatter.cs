using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StudentsDemo1.Formatters
{
    /// <summary>
    /// Formatter that allows content of type text/plain and application/octet stream
    /// or no content type to be parsed to raw data. Allows for a single input parameter
    /// in the form of:
    /// 
    /// public string RawString([FromBody] string data)
    /// </summary>
    public class RawRequestBodyInputFormatter : InputFormatter
    {
        public RawRequestBodyInputFormatter()
        {
            SupportedMediaTypes.Add( new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("text/plain"));
        }


        /// <summary>
        /// Allow text/plain, application/octet-stream and no content type to
        /// be processed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Boolean CanRead(InputFormatterContext context)
        {
            return true;
        }

        /// <summary>
        /// Handle text/plain or no content type for string results
        /// Handle application/octet-stream for byte[] results
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }
    }
}
