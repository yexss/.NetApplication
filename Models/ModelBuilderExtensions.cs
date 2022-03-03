using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "fae",
                    ClassName = ClassNameEnum.SecondGrade,
                    Email = "dac@gmail.com",
                    Photo=""
                },
                new Student
                {
                    Id = 2,
                    Name = "cssd",
                    ClassName = ClassNameEnum.FirstGrade,
                    Email = "dfaa@qq.com",
                    Photo =""
                }
            );
        }
    }
}
