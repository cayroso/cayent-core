using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Common.Extensions
{
    public static class FileExtensions
    {
        private static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

        public static string GetContentType(this string filename)
        {
            if (!Provider.TryGetContentType(filename, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
