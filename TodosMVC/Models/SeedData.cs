using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TodosMVC.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                //tìm kiếm xem đã có dữ liệu ở bảng Todo hay chưa, nếu có thì không cần thêm dữ liệu mẫu nữa
                if (context.Todos.Any())
                {
                    return;
                }
                var adminID = await EnsureUser(serviceProvider, "123456", "admin@gmail.com");
                await EnsureRole(serviceProvider, "1", "admin");

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, "123456", "manager@gmail.com");
                await EnsureRole(serviceProvider, "2", "manager");
                context.Todos.AddRange(
                    new Todo()
                    {
                        Title = "Học ASP.Net Core",
                        Description = "Học cách Seeding Data",
                        CreatedDate = DateTime.Now,
                        Status = true,
                        OwnerID = "1",
                    },
                    new Todo()
                    {
                        Title = "Học Front-end ReactJS",
                        Description = "Tìm hiểu Life Circle",
                        CreatedDate = DateTime.Now,
                        Status = false,
                        OwnerID = "1",
                    },
                    new Todo()
                    {
                        Title = "Học Front-end Angular",
                        Description = "Tìm kiểu Validation dữ liệu ở Form",
                        CreatedDate = DateTime.Now,
                        Status = true,
                        OwnerID = "1",
                    },
                    new Todo()
                    {
                        Title = "Học AWS",
                        Description = "Triển khai VPS trong môi trường thực tế",
                        CreatedDate = DateTime.Now,
                        Status = true,
                        OwnerID = "1",
                    },
                    new Todo()
                    {
                        Title = "Học AWS",
                        Description = "Cài MySQL trên môi trường Linux",
                        CreatedDate = DateTime.Now,
                        Status = false,
                        OwnerID = "2",
                    }
                    );

                context.SaveChanges(); //lưu vào database
            };
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new User
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            Role IR;
            if (!await roleManager.FindByIdAsync(role))
            {

                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
