using Microsoft.AspNetCore.Mvc;
using DemoApplication.Models;
using DemoApplication.ViewModels;

namespace DemoApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public TestController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> students=_studentRepository.GetAllStudents();

            return View(students);
            //return View("Index");
            //return View("~/Views/Home/Index.cshtml");
        }

        public JsonResult getStu(int Id)
        {
            //return _studentRepository.GetStudent(Id).Name;

            Student model = _studentRepository.GetStudent(Id);
            return Json(model);
        }
        

        public IActionResult Details()
        {
            Student model = _studentRepository.GetStudent(1);

            ViewData["PageTitle"] = "ViewData Details";
            ViewData["Student"] = model;


            ViewBag.PageTitle = "ViewBag Details";
            ViewBag.Student = model;

            return View(model);
        }

        public IActionResult normDetails()
        {
            TestnormDetailsViewModel testnormDetailsViewModel = new TestnormDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(2),
                PageTitle = "ViewModel Details"
            };
            return View(testnormDetailsViewModel);
        }

    }
}
