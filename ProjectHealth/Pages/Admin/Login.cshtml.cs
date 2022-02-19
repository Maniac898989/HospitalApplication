using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectHealth.Models.WebModel;

namespace ProjectHealth.Web.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Registration Login { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        } 
    }
}
