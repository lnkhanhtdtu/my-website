using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ViewBag.Status = statusCode;
            switch (statusCode)
            {
                case 404:
                    ViewBag.Message = "Oops! Trang không tìm thấy";
                    break;
                case 500:
                    ViewBag.Message = "Đã xảy ra lỗi máy chủ nội bộ";
                    break;
                case 403:
                    ViewBag.Message = "Truy cập bị từ chối";
                    break;
                default:
                    ViewBag.Message = "Đã xảy ra lỗi";
                    break;
            }

            return View("NotFound");
        }
    }
}
