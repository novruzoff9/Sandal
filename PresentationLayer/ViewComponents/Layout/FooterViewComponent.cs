using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Layout;

public class FooterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
