using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BaGet.Core;
using BaGet.Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

    namespace BaGet.Web
{
    public class UploadModel : PageModel
    {
        private readonly AuthenticationHelper _authenticationHelper = new AuthenticationHelper();
                
        public async Task<IActionResult> OnGetAsync()
        {
            var errorResult = _authenticationHelper.getAuthenticationError(Request.Headers, Response);
            if (errorResult != null) return errorResult;
            return Page();
        }
    }
}
