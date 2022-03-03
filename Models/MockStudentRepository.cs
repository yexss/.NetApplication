namespace DemoApplication.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        //prop.ctor

        private List<Student> _studentList;

        public MockStudentRepository()
        {
            _studentList = new List<Student>() { 
                new Student(){Id = 1, Name ="a",ClassName=ClassNameEnum.FirstGrade,Email="Tony@gmail.com"},
                new Student(){Id = 2, Name ="b",ClassName=ClassNameEnum.SecondGrade,Email="hees@qq.com"},
                new Student(){Id = 3, Name ="c",ClassName=ClassNameEnum.ThirdGrade,Email="free@163.com"}
            };
        }

        public Student Add(Student student)
        {
            student.Id=_studentList.Max(x => x.Id)+1;
            _studentList.Add(student);
            return student;
        }

        public Student Delete(int id)
        {
            Student student = _studentList.FirstOrDefault(x => x.Id == id);

            if(student != null)
            {
                _studentList.Remove(student);
            }

            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentList;
        }

        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(a => a.Id == id);
        }

        public Student Update(Student updateStudent)
        {
            Student student=_studentList.FirstOrDefault(x=>x.Id==updateStudent.Id);

            if (student != null)
            {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.ClassName = updateStudent.ClassName;
            }

            return student;
        }
    }
}
