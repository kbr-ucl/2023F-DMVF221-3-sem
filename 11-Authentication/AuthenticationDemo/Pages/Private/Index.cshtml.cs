using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationDemo.Pages.Private;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}