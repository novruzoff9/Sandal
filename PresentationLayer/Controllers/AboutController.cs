using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class AboutController : Controller
    {
        private readonly IFAQService _faqService;

        public AboutController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FAQs()
        {
            var faqs = _faqService.GetAllFAQ();
            return View(faqs);
        }

        public IActionResult BecomeVendor()
        {
            return View();
        }
    }
}
