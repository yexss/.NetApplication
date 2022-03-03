using DemoApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Models
{
    public class DesuController : Controller
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<DesuController> logger;

        public DesuController(IStudentRepository studentRepository,IWebHostEnvironment webHostEnvironment,ILogger<DesuController> logger)
        {
            _studentRepository = studentRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();

            return View(students);
        }


        public IActionResult Details(int Id)
        {
            //logger.LogTrace("Details Trace");
            //logger.LogDebug("Details LogDebug");
            //logger.LogInformation("Details Information");
            //logger.LogWarning("Details Warning");
            //logger.LogError("Details Error");
            //logger.LogCritical("Details Critical");

            //throw new Exception("发生在Deatils中的异常");

            Student model = _studentRepository.GetStudent(Id);

            if(model == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound",Id);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            //RedirectToActionResult

            if (ModelState.IsValid)
            {
                string? uniqueFileName = null;
                if (model.Photo != null)
                {
                    uniqueFileName = ProcessUploadedFile(model);
                }

                Student newStudent = new Student()
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    Photo = uniqueFileName
                };

                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { Id = newStudent.Id });


                //Student newStudent = _studentRepository.Add(student);
                //return RedirectToAction("Details", new { Id = newStudent.Id });
            }

            return View();
        }

        public JsonResult Delete(int Id)
        {
            Student student=_studentRepository.Delete(Id);
            return Json(student);
        }

        public JsonResult J_Details(int Id)
        {
            Student student = _studentRepository.GetStudent(Id);
            return Json(student);
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            Student student= _studentRepository.GetStudent(Id);

            StudentEditViewModel model = new StudentEditViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhoto = student.Photo
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student=_studentRepository.GetStudent(model.Id);
                student.Name = model.Name;
                student.Email= model.Email;
                student.ClassName= model.ClassName;

                if(model.Photo != null)
                {
                    if(model.ExistingPhoto != null)
                    {
                        string old_filePath = Path.Combine(webHostEnvironment.WebRootPath, "images",model.ExistingPhoto);
                        System.IO.File.Delete(old_filePath);
                    }


                    student.Photo = ProcessUploadedFile(model);
                }
                Student updateStudent = _studentRepository.Update(student);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private string ProcessUploadedFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using(var fileStream= new FileStream(filePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
