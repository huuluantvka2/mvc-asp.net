using Microsoft.EntityFrameworkCore;

namespace TodosMVC.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                //tìm kiếm xem đã có dữ liệu ở bảng Todo hay chưa, nếu có thì không cần thêm dữ liệu mẫu nữa
                if (context.Todos.Any())
                {
                    return;
                }
                context.Todos.AddRange(
                    new Todo()
                    {
                        Title = "Học ASP.Net Core",
                        Description = "Học cách Seeding Data",
                        CreatedDate = DateTime.Now,
                        Status = true,
                    },
                    new Todo()
                    {
                        Title = "Học Front-end ReactJS",
                        Description = "Tìm hiểu Life Circle",
                        CreatedDate = DateTime.Now,
                        Status = false,
                    },
                    new Todo()
                    {
                        Title = "Học Front-end Angular",
                        Description = "Tìm kiểu Validation dữ liệu ở Form",
                        CreatedDate = DateTime.Now,
                        Status = true,
                    },
                    new Todo()
                    {
                        Title = "Học AWS",
                        Description = "Triển khai VPS trong môi trường thực tế",
                        CreatedDate = DateTime.Now,
                        Status = true,
                    },
                    new Todo()
                    {
                        Title = "Học AWS",
                        Description = "Cài MySQL trên môi trường Linux",
                        CreatedDate = DateTime.Now,
                        Status = false,
                    }
                    );
                context.SaveChanges(); //lưu vào database
            };
        }
    }
}
