using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Data
{
	public class ContentInitializer
	{
		public static async Task InitializeAsync(ApplicationContext context)
		{
			if (!context.Roles.Any())
			{
				await context.Roles.AddRangeAsync
				(
						new Role { Id = Guid.NewGuid(), Name = "Admin" },
						new Role { Id = Guid.NewGuid(), Name = "Cusmomer" }
				);
				await context.SaveChangesAsync();
			}


			if (!context.Users.Any())
			{
				await context.Users.AddRangeAsync
				(
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Alice",
						LastName = "Johnson",
						ProfilePhoto = "/images/users/alice.jpg",
						Email = "alice.johnson@example.com",
						PasswordHash = "hashed_pw_1",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2024-01-15")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Bob",
						LastName = "Smith",
						ProfilePhoto = "/images/users/bob.jpg",
						Email = "bob.smith@example.com",
						PasswordHash = "hashed_pw_2",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2023-11-20")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Clara",
						LastName = "Evans",
						ProfilePhoto = "/images/users/clara.jpg",
						Email = "clara.evans@example.com",
						PasswordHash = "hashed_pw_3",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2024-03-11")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "David",
						LastName = "Lee",
						ProfilePhoto = "/images/users/david.jpg",
						Email = "david.lee@example.com",
						PasswordHash = "hashed_pw_4",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = false,
						RegistrationDate = DateOnly.Parse("2022-12-30")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Emma",
						LastName = "Wilson",
						ProfilePhoto = "/images/users/emma.jpg",
						Email = "emma.wilson@example.com",
						PasswordHash = "hashed_pw_5",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2023-08-01")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Frank",
						LastName = "Baker",
						ProfilePhoto = "/images/users/frank.jpg",
						Email = "frank.baker@example.com",
						PasswordHash = "hashed_pw_6",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = false,
						RegistrationDate = DateOnly.Parse("2024-02-17")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Grace",
						LastName = "Turner",
						ProfilePhoto = "/images/users/grace.jpg",
						Email = "grace.turner@example.com",
						PasswordHash = "hashed_pw_7",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2024-04-08")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Henry",
						LastName = "Davis",
						ProfilePhoto = "/images/users/henry.jpg",
						Email = "henry.davis@example.com",
						PasswordHash = "hashed_pw_8",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2023-06-25")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Isla",
						LastName = "Brown",
						ProfilePhoto = "/images/users/isla.jpg",
						Email = "isla.brown@example.com",
						PasswordHash = "hashed_pw_9",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2024-05-18")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Jack",
						LastName = "Miller",
						ProfilePhoto = "/images/users/jack.jpg",
						Email = "jack.miller@example.com",
						PasswordHash = "hashed_pw_10",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = false,
						RegistrationDate = DateOnly.Parse("2023-01-05")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Karen",
						LastName = "Walker",
						ProfilePhoto = "/images/users/karen.jpg",
						Email = "karen.walker@example.com",
						PasswordHash = "hashed_pw_11",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2023-03-15")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Leo",
						LastName = "White",
						ProfilePhoto = "/images/users/leo.jpg",
						Email = "leo.white@example.com",
						PasswordHash = "hashed_pw_12",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2022-09-10")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Mia",
						LastName = "Clark",
						ProfilePhoto = "/images/users/mia.jpg",
						Email = "mia.clark@example.com",
						PasswordHash = "hashed_pw_13",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2024-06-10")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Nick",
						LastName = "King",
						ProfilePhoto = "/images/users/nick.jpg",
						Email = "nick.king@example.com",
						PasswordHash = "hashed_pw_14",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = DateOnly.Parse("2023-12-01")
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Olivia",
						LastName = "Green",
						ProfilePhoto = "/images/users/olivia.jpg",
						Email = "olivia.green@example.com",
						PasswordHash = "hashed_pw_15",
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = false,
						RegistrationDate = DateOnly.Parse("2024-07-01")
					}
				);
				await context.SaveChangesAsync();
			}
		}
	}
}
