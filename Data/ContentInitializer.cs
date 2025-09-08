using AmazonApiServer.Models;
using AmazonApiServer.Services;
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
						new Role { Id = Guid.NewGuid(), Name = "Customer" }
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
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_1"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = new DateTime(2024, 01, 15)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Bob",
						LastName = "Smith",
						ProfilePhoto = "/images/users/bob.jpg",
						Email = "bob.smith@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_2"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = new DateTime(2023, 11, 20)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Clara",
						LastName = "Evans",
						ProfilePhoto = "/images/users/clara.jpg",
						Email = "clara.evans@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_3"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2024, 03, 11)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "David",
						LastName = "Lee",
						ProfilePhoto = "/images/users/david.jpg",
						Email = "david.lee@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_4"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = false,
						RegistrationDate = new DateTime(2022, 12, 30)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Emma",
						LastName = "Wilson",
						ProfilePhoto = "/images/users/emma.jpg",
						Email = "emma.wilson@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_5"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = new DateTime(2023, 08, 01)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Frank",
						LastName = "Baker",
						ProfilePhoto = "/images/users/frank.jpg",
						Email = "frank.baker@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_6"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = false,
						RegistrationDate = new DateTime(2024, 02, 17)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Grace",
						LastName = "Turner",
						ProfilePhoto = "/images/users/grace.jpg",
						Email = "grace.turner@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_7"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2024, 04, 08)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Henry",
						LastName = "Davis",
						ProfilePhoto = "/images/users/henry.jpg",
						Email = "henry.davis@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_8"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2023, 06, 25)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Isla",
						LastName = "Brown",
						ProfilePhoto = "/images/users/isla.jpg",
						Email = "isla.brown@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_9"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2024, 05, 18)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Jack",
						LastName = "Miller",
						ProfilePhoto = "/images/users/jack.jpg",
						Email = "jack.miller@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_10"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = false,
						RegistrationDate = new DateTime(2023, 01, 05)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Karen",
						LastName = "Walker",
						ProfilePhoto = "/images/users/karen.jpg",
						Email = "karen.walker@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_11"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = true,
						RegistrationDate = new DateTime(2023, 03, 15)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Leo",
						LastName = "White",
						ProfilePhoto = "/images/users/leo.jpg",
						Email = "leo.white@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_12"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2022, 09, 10)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Mia",
						LastName = "Clark",
						ProfilePhoto = "/images/users/mia.jpg",
						Email = "mia.clark@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_13"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2024, 06, 10)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Nick",
						LastName = "King",
						ProfilePhoto = "/images/users/nick.jpg",
						Email = "nick.king@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_14"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer"),
						IsActive = true,
						RegistrationDate = new DateTime(2023, 12, 01)
					},
					new User
					{
						Id = Guid.NewGuid(),
						FirstName = "Olivia",
						LastName = "Green",
						ProfilePhoto = "/images/users/olivia.jpg",
						Email = "olivia.green@example.com",
						PasswordHash = PasswordHasher.HashPassword("hashed_pw_15"),
						Role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"),
						IsActive = false,
						RegistrationDate = new DateTime(2024, 07, 01)
					}
				);
				await context.SaveChangesAsync();
			}

			Guid UsaId = Guid.Parse("11111111-1111-1111-1111-111111111111");

			if (!context.Countries.Any())
			{
				await context.Countries.AddRangeAsync
				(
					// Африка (54 страны)
					new Country { Id = Guid.NewGuid(), Name = "Algeria", Code = "DZ" },
					new Country { Id = Guid.NewGuid(), Name = "Angola", Code = "AO" },
					new Country { Id = Guid.NewGuid(), Name = "Benin", Code = "BJ" },
					new Country { Id = Guid.NewGuid(), Name = "Botswana", Code = "BW" },
					new Country { Id = Guid.NewGuid(), Name = "Burkina Faso", Code = "BF" },
					new Country { Id = Guid.NewGuid(), Name = "Burundi", Code = "BI" },
					new Country { Id = Guid.NewGuid(), Name = "Cabo Verde", Code = "CV" },
					new Country { Id = Guid.NewGuid(), Name = "Cameroon", Code = "CM" },
					new Country { Id = Guid.NewGuid(), Name = "Central African Republic", Code = "CF" },
					new Country { Id = Guid.NewGuid(), Name = "Chad", Code = "TD" },
					new Country { Id = Guid.NewGuid(), Name = "Comoros", Code = "KM" },
					new Country { Id = Guid.NewGuid(), Name = "Congo", Code = "CG" },
					new Country { Id = Guid.NewGuid(), Name = "Democratic Republic of the Congo", Code = "CD" },
					new Country { Id = Guid.NewGuid(), Name = "Djibouti", Code = "DJ" },
					new Country { Id = Guid.NewGuid(), Name = "Egypt", Code = "EG" },
					new Country { Id = Guid.NewGuid(), Name = "Equatorial Guinea", Code = "GQ" },
					new Country { Id = Guid.NewGuid(), Name = "Eritrea", Code = "ER" },
					new Country { Id = Guid.NewGuid(), Name = "Eswatini", Code = "SZ" },
					new Country { Id = Guid.NewGuid(), Name = "Ethiopia", Code = "ET" },
					new Country { Id = Guid.NewGuid(), Name = "Gabon", Code = "GA" },
					new Country { Id = Guid.NewGuid(), Name = "Gambia", Code = "GM" },
					new Country { Id = Guid.NewGuid(), Name = "Ghana", Code = "GH" },
					new Country { Id = Guid.NewGuid(), Name = "Guinea", Code = "GN" },
					new Country { Id = Guid.NewGuid(), Name = "Guinea-Bissau", Code = "GW" },
					new Country { Id = Guid.NewGuid(), Name = "Ivory Coast", Code = "CI" },
					new Country { Id = Guid.NewGuid(), Name = "Kenya", Code = "KE" },
					new Country { Id = Guid.NewGuid(), Name = "Lesotho", Code = "LS" },
					new Country { Id = Guid.NewGuid(), Name = "Liberia", Code = "LR" },
					new Country { Id = Guid.NewGuid(), Name = "Libya", Code = "LY" },
					new Country { Id = Guid.NewGuid(), Name = "Madagascar", Code = "MG" },
					new Country { Id = Guid.NewGuid(), Name = "Malawi", Code = "MW" },
					new Country { Id = Guid.NewGuid(), Name = "Mali", Code = "ML" },
					new Country { Id = Guid.NewGuid(), Name = "Mauritania", Code = "MR" },
					new Country { Id = Guid.NewGuid(), Name = "Mauritius", Code = "MU" },
					new Country { Id = Guid.NewGuid(), Name = "Morocco", Code = "MA" },
					new Country { Id = Guid.NewGuid(), Name = "Mozambique", Code = "MZ" },
					new Country { Id = Guid.NewGuid(), Name = "Namibia", Code = "NA" },
					new Country { Id = Guid.NewGuid(), Name = "Niger", Code = "NE" },
					new Country { Id = Guid.NewGuid(), Name = "Nigeria", Code = "NG" },
					new Country { Id = Guid.NewGuid(), Name = "Rwanda", Code = "RW" },
					new Country { Id = Guid.NewGuid(), Name = "Sao Tome and Principe", Code = "ST" },
					new Country { Id = Guid.NewGuid(), Name = "Senegal", Code = "SN" },
					new Country { Id = Guid.NewGuid(), Name = "Seychelles", Code = "SC" },
					new Country { Id = Guid.NewGuid(), Name = "Sierra Leone", Code = "SL" },
					new Country { Id = Guid.NewGuid(), Name = "Somalia", Code = "SO" },
					new Country { Id = Guid.NewGuid(), Name = "South Africa", Code = "ZA" },
					new Country { Id = Guid.NewGuid(), Name = "South Sudan", Code = "SS" },
					new Country { Id = Guid.NewGuid(), Name = "Sudan", Code = "SD" },
					new Country { Id = Guid.NewGuid(), Name = "Tanzania", Code = "TZ" },
					new Country { Id = Guid.NewGuid(), Name = "Togo", Code = "TG" },
					new Country { Id = Guid.NewGuid(), Name = "Tunisia", Code = "TN" },
					new Country { Id = Guid.NewGuid(), Name = "Uganda", Code = "UG" },
					new Country { Id = Guid.NewGuid(), Name = "Zambia", Code = "ZM" },
					new Country { Id = Guid.NewGuid(), Name = "Zimbabwe", Code = "ZW" },

					// Азия (48 стран)
					new Country { Id = Guid.NewGuid(), Name = "Afghanistan", Code = "AF" },
					new Country { Id = Guid.NewGuid(), Name = "Armenia", Code = "AM" },
					new Country { Id = Guid.NewGuid(), Name = "Azerbaijan", Code = "AZ" },
					new Country { Id = Guid.NewGuid(), Name = "Bahrain", Code = "BH" },
					new Country { Id = Guid.NewGuid(), Name = "Bangladesh", Code = "BD" },
					new Country { Id = Guid.NewGuid(), Name = "Bhutan", Code = "BT" },
					new Country { Id = Guid.NewGuid(), Name = "Brunei", Code = "BN" },
					new Country { Id = Guid.NewGuid(), Name = "Cambodia", Code = "KH" },
					new Country { Id = Guid.NewGuid(), Name = "China", Code = "CN" },
					new Country { Id = Guid.NewGuid(), Name = "Cyprus", Code = "CY" },
					new Country { Id = Guid.NewGuid(), Name = "Georgia", Code = "GE" },
					new Country { Id = Guid.NewGuid(), Name = "India", Code = "IN" },
					new Country { Id = Guid.NewGuid(), Name = "Indonesia", Code = "ID" },
					new Country { Id = Guid.NewGuid(), Name = "Iran", Code = "IR" },
					new Country { Id = Guid.NewGuid(), Name = "Iraq", Code = "IQ" },
					new Country { Id = Guid.NewGuid(), Name = "Israel", Code = "IL" },
					new Country { Id = Guid.NewGuid(), Name = "Japan", Code = "JP" },
					new Country { Id = Guid.NewGuid(), Name = "Jordan", Code = "JO" },
					new Country { Id = Guid.NewGuid(), Name = "Kazakhstan", Code = "KZ" },
					new Country { Id = Guid.NewGuid(), Name = "Kuwait", Code = "KW" },
					new Country { Id = Guid.NewGuid(), Name = "Kyrgyzstan", Code = "KG" },
					new Country { Id = Guid.NewGuid(), Name = "Laos", Code = "LA" },
					new Country { Id = Guid.NewGuid(), Name = "Lebanon", Code = "LB" },
					new Country { Id = Guid.NewGuid(), Name = "Malaysia", Code = "MY" },
					new Country { Id = Guid.NewGuid(), Name = "Maldives", Code = "MV" },
					new Country { Id = Guid.NewGuid(), Name = "Mongolia", Code = "MN" },
					new Country { Id = Guid.NewGuid(), Name = "Myanmar", Code = "MM" },
					new Country { Id = Guid.NewGuid(), Name = "Nepal", Code = "NP" },
					new Country { Id = Guid.NewGuid(), Name = "North Korea", Code = "KP" },
					new Country { Id = Guid.NewGuid(), Name = "Oman", Code = "OM" },
					new Country { Id = Guid.NewGuid(), Name = "Pakistan", Code = "PK" },
					new Country { Id = Guid.NewGuid(), Name = "Palestine", Code = "PS" },
					new Country { Id = Guid.NewGuid(), Name = "Philippines", Code = "PH" },
					new Country { Id = Guid.NewGuid(), Name = "Qatar", Code = "QA" },
					new Country { Id = Guid.NewGuid(), Name = "Saudi Arabia", Code = "SA" },
					new Country { Id = Guid.NewGuid(), Name = "Singapore", Code = "SG" },
					new Country { Id = Guid.NewGuid(), Name = "South Korea", Code = "KR" },
					new Country { Id = Guid.NewGuid(), Name = "Sri Lanka", Code = "LK" },
					new Country { Id = Guid.NewGuid(), Name = "Syria", Code = "SY" },
					new Country { Id = Guid.NewGuid(), Name = "Taiwan", Code = "TW" },
					new Country { Id = Guid.NewGuid(), Name = "Tajikistan", Code = "TJ" },
					new Country { Id = Guid.NewGuid(), Name = "Thailand", Code = "TH" },
					new Country { Id = Guid.NewGuid(), Name = "Timor-Leste", Code = "TL" },
					new Country { Id = Guid.NewGuid(), Name = "Turkey", Code = "TR" },
					new Country { Id = Guid.NewGuid(), Name = "Turkmenistan", Code = "TM" },
					new Country { Id = Guid.NewGuid(), Name = "United Arab Emirates", Code = "AE" },
					new Country { Id = Guid.NewGuid(), Name = "Uzbekistan", Code = "UZ" },
					new Country { Id = Guid.NewGuid(), Name = "Vietnam", Code = "VN" },
					new Country { Id = Guid.NewGuid(), Name = "Yemen", Code = "YE" },

					// Европа (44 страны)
					new Country { Id = Guid.NewGuid(), Name = "Albania", Code = "AL" },
					new Country { Id = Guid.NewGuid(), Name = "Andorra", Code = "AD" },
					new Country { Id = Guid.NewGuid(), Name = "Austria", Code = "AT" },
					new Country { Id = Guid.NewGuid(), Name = "Belarus", Code = "BY" },
					new Country { Id = Guid.NewGuid(), Name = "Belgium", Code = "BE" },
					new Country { Id = Guid.NewGuid(), Name = "Bosnia and Herzegovina", Code = "BA" },
					new Country { Id = Guid.NewGuid(), Name = "Bulgaria", Code = "BG" },
					new Country { Id = Guid.NewGuid(), Name = "Croatia", Code = "HR" },
					new Country { Id = Guid.NewGuid(), Name = "Czech Republic", Code = "CZ" },
					new Country { Id = Guid.NewGuid(), Name = "Denmark", Code = "DK" },
					new Country { Id = Guid.NewGuid(), Name = "Estonia", Code = "EE" },
					new Country { Id = Guid.NewGuid(), Name = "Finland", Code = "FI" },
					new Country { Id = Guid.NewGuid(), Name = "France", Code = "FR" },
					new Country { Id = Guid.NewGuid(), Name = "Germany", Code = "DE" },
					new Country { Id = Guid.NewGuid(), Name = "Greece", Code = "GR" },
					new Country { Id = Guid.NewGuid(), Name = "Hungary", Code = "HU" },
					new Country { Id = Guid.NewGuid(), Name = "Iceland", Code = "IS" },
					new Country { Id = Guid.NewGuid(), Name = "Ireland", Code = "IE" },
					new Country { Id = Guid.NewGuid(), Name = "Italy", Code = "IT" },
					new Country { Id = Guid.NewGuid(), Name = "Kosovo", Code = "XK" },
					new Country { Id = Guid.NewGuid(), Name = "Latvia", Code = "LV" },
					new Country { Id = Guid.NewGuid(), Name = "Liechtenstein", Code = "LI" },
					new Country { Id = Guid.NewGuid(), Name = "Lithuania", Code = "LT" },
					new Country { Id = Guid.NewGuid(), Name = "Luxembourg", Code = "LU" },
					new Country { Id = Guid.NewGuid(), Name = "Malta", Code = "MT" },
					new Country { Id = Guid.NewGuid(), Name = "Moldova", Code = "MD" },
					new Country { Id = Guid.NewGuid(), Name = "Monaco", Code = "MC" },
					new Country { Id = Guid.NewGuid(), Name = "Montenegro", Code = "ME" },
					new Country { Id = Guid.NewGuid(), Name = "Netherlands", Code = "NL" },
					new Country { Id = Guid.NewGuid(), Name = "North Macedonia", Code = "MK" },
					new Country { Id = Guid.NewGuid(), Name = "Norway", Code = "NO" },
					new Country { Id = Guid.NewGuid(), Name = "Poland", Code = "PL" },
					new Country { Id = Guid.NewGuid(), Name = "Portugal", Code = "PT" },
					new Country { Id = Guid.NewGuid(), Name = "Romania", Code = "RO" },
					new Country { Id = Guid.NewGuid(), Name = "Russia", Code = "RU" },
					new Country { Id = Guid.NewGuid(), Name = "San Marino", Code = "SM" },
					new Country { Id = Guid.NewGuid(), Name = "Serbia", Code = "RS" },
					new Country { Id = Guid.NewGuid(), Name = "Slovakia", Code = "SK" },
					new Country { Id = Guid.NewGuid(), Name = "Slovenia", Code = "SI" },
					new Country { Id = Guid.NewGuid(), Name = "Spain", Code = "ES" },
					new Country { Id = Guid.NewGuid(), Name = "Sweden", Code = "SE" },
					new Country { Id = Guid.NewGuid(), Name = "Switzerland", Code = "CH" },
					new Country { Id = Guid.NewGuid(), Name = "Ukraine", Code = "UA" },
					new Country { Id = Guid.NewGuid(), Name = "United Kingdom", Code = "GB" },
					new Country { Id = Guid.NewGuid(), Name = "Vatican City", Code = "VA" },

					// Северная Америка (23 страны)
					new Country { Id = Guid.NewGuid(), Name = "Antigua and Barbuda", Code = "AG" },
					new Country { Id = Guid.NewGuid(), Name = "Bahamas", Code = "BS" },
					new Country { Id = Guid.NewGuid(), Name = "Barbados", Code = "BB" },
					new Country { Id = Guid.NewGuid(), Name = "Belize", Code = "BZ" },
					new Country { Id = Guid.NewGuid(), Name = "Canada", Code = "CA" },
					new Country { Id = Guid.NewGuid(), Name = "Costa Rica", Code = "CR" },
					new Country { Id = Guid.NewGuid(), Name = "Cuba", Code = "CU" },
					new Country { Id = Guid.NewGuid(), Name = "Dominica", Code = "DM" },
					new Country { Id = Guid.NewGuid(), Name = "Dominican Republic", Code = "DO" },
					new Country { Id = Guid.NewGuid(), Name = "El Salvador", Code = "SV" },
					new Country { Id = Guid.NewGuid(), Name = "Grenada", Code = "GD" },
					new Country { Id = Guid.NewGuid(), Name = "Guatemala", Code = "GT" },
					new Country { Id = Guid.NewGuid(), Name = "Haiti", Code = "HT" },
					new Country { Id = Guid.NewGuid(), Name = "Honduras", Code = "HN" },
					new Country { Id = Guid.NewGuid(), Name = "Jamaica", Code = "JM" },
					new Country { Id = Guid.NewGuid(), Name = "Mexico", Code = "MX" },
					new Country { Id = Guid.NewGuid(), Name = "Nicaragua", Code = "NI" },
					new Country { Id = Guid.NewGuid(), Name = "Panama", Code = "PA" },
					new Country { Id = Guid.NewGuid(), Name = "Saint Kitts and Nevis", Code = "KN" },
					new Country { Id = Guid.NewGuid(), Name = "Saint Lucia", Code = "LC" },
					new Country { Id = Guid.NewGuid(), Name = "Saint Vincent and the Grenadines", Code = "VC" },
					new Country { Id = Guid.NewGuid(), Name = "Trinidad and Tobago", Code = "TT" },
					new Country { Id = UsaId, Name = "United States", Code = "US" },

					// Южная Америка (12 стран)
					new Country { Id = Guid.NewGuid(), Name = "Argentina", Code = "AR" },
					new Country { Id = Guid.NewGuid(), Name = "Bolivia", Code = "BO" },
					new Country { Id = Guid.NewGuid(), Name = "Brazil", Code = "BR" },
					new Country { Id = Guid.NewGuid(), Name = "Chile", Code = "CL" },
					new Country { Id = Guid.NewGuid(), Name = "Colombia", Code = "CO" },
					new Country { Id = Guid.NewGuid(), Name = "Ecuador", Code = "EC" },
					new Country { Id = Guid.NewGuid(), Name = "Guyana", Code = "GY" },
					new Country { Id = Guid.NewGuid(), Name = "Paraguay", Code = "PY" },
					new Country { Id = Guid.NewGuid(), Name = "Peru", Code = "PE" },
					new Country { Id = Guid.NewGuid(), Name = "Suriname", Code = "SR" },
					new Country { Id = Guid.NewGuid(), Name = "Uruguay", Code = "UY" },
					new Country { Id = Guid.NewGuid(), Name = "Venezuela", Code = "VE" },

					// Океания (14 стран)
					new Country { Id = Guid.NewGuid(), Name = "Australia", Code = "AU" },
					new Country { Id = Guid.NewGuid(), Name = "Fiji", Code = "FJ" },
					new Country { Id = Guid.NewGuid(), Name = "Kiribati", Code = "KI" },
					new Country { Id = Guid.NewGuid(), Name = "Marshall Islands", Code = "MH" },
					new Country { Id = Guid.NewGuid(), Name = "Micronesia", Code = "FM" },
					new Country { Id = Guid.NewGuid(), Name = "Nauru", Code = "NR" },
					new Country { Id = Guid.NewGuid(), Name = "New Zealand", Code = "NZ" },
					new Country { Id = Guid.NewGuid(), Name = "Palau", Code = "PW" },
					new Country { Id = Guid.NewGuid(), Name = "Papua New Guinea", Code = "PG" },
					new Country { Id = Guid.NewGuid(), Name = "Samoa", Code = "WS" },
					new Country { Id = Guid.NewGuid(), Name = "Solomon Islands", Code = "SB" },
					new Country { Id = Guid.NewGuid(), Name = "Tonga", Code = "TO" },
					new Country { Id = Guid.NewGuid(), Name = "Tuvalu", Code = "TV" },
					new Country { Id = Guid.NewGuid(), Name = "Vanuatu", Code = "VU" }
				);
				await context.SaveChangesAsync();
			}

			if (!context.States.Any())
			{
				await context.States.AddRangeAsync
				(
					new State { Id = Guid.NewGuid(), Name = "Alabama", Code = "AL", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Alaska", Code = "AK", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Arizona", Code = "AZ", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Arkansas", Code = "AR", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "California", Code = "CA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Colorado", Code = "CO", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Connecticut", Code = "CT", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Delaware", Code = "DE", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Florida", Code = "FL", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Georgia", Code = "GA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Hawaii", Code = "HI", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Idaho", Code = "ID", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Illinois", Code = "IL", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Indiana", Code = "IN", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Iowa", Code = "IA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Kansas", Code = "KS", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Kentucky", Code = "KY", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Louisiana", Code = "LA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Maine", Code = "ME", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Maryland", Code = "MD", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Massachusetts", Code = "MA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Michigan", Code = "MI", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Minnesota", Code = "MN", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Mississippi", Code = "MS", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Missouri", Code = "MO", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Montana", Code = "MT", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Nebraska", Code = "NE", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Nevada", Code = "NV", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "New Hampshire", Code = "NH", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "New Jersey", Code = "NJ", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "New Mexico", Code = "NM", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "New York", Code = "NY", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "North Carolina", Code = "NC", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "North Dakota", Code = "ND", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Ohio", Code = "OH", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Oklahoma", Code = "OK", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Oregon", Code = "OR", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Pennsylvania", Code = "PA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Rhode Island", Code = "RI", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "South Carolina", Code = "SC", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "South Dakota", Code = "SD", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Tennessee", Code = "TN", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Texas", Code = "TX", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Utah", Code = "UT", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Vermont", Code = "VT", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Virginia", Code = "VA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Washington", Code = "WA", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "West Virginia", Code = "WV", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Wisconsin", Code = "WI", CountryId = UsaId },
					new State { Id = Guid.NewGuid(), Name = "Wyoming", Code = "WY", CountryId = UsaId }
				);
				await context.SaveChangesAsync();
			}
		}
	}
}