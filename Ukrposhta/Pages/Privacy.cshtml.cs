using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ukrposhta.Pages
{
    public class PrivacyPageModel : PageModel
    {
        private readonly ILogger<PrivacyPageModel> _logger;

        public PrivacyPageModel(ILogger<PrivacyPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
