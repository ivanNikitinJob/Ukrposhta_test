using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ukrposhta.Pages
{
    public class IndexPageModel : PageModel
    {
        private readonly ILogger<IndexPageModel> _logger;

        public IndexPageModel(ILogger<IndexPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
