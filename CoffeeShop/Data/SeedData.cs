using CoffeeShop.Helper;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace CoffeeShop.Data
{
    public class SeedData
    {
        public async static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var createScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = createScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                var userManager =createScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var userRole =  createScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await userRole.RoleExistsAsync(UserRoles.CustomerRole))
                {
                   await  userRole.CreateAsync(new IdentityRole(UserRoles.CustomerRole));
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(new List<AppUser>{
                        new AppUser()
                        {
                            Id = "2d16c229-9d00-ce9b-5a0d-d7f47513a9f3",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890", 
                            CreatedAt = DateTime.Now, 
                            IsDeleted = false,  
                            UpdatedAt = DateTime.Now,   
                        },
                         new AppUser
                        {
                            Id = "6b84487c-290a-22cc-40d5-e548fdb826e2",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890",
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            UpdatedAt = DateTime.Now,
                        },
                          new AppUser
                        {
                            Id = "2da1b50a-628c-4c13-b4a5-377f984c97c0",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890",
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            UpdatedAt = DateTime.Now,
                        },
                           new AppUser
                        {
                            Id = "c8aa2e99-8f18-7397-8938-b62b374c57ec",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890",
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            UpdatedAt = DateTime.Now,
                        },
                            new AppUser
                        {
                            Id = "81cc184b-df68-818a-ec9a-6cbe6959ddb0",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890",
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            UpdatedAt = DateTime.Now,
                        },
                             new AppUser
                        {
                            Id = "48e2a27a-d4dc-70da-cdfc-cf5728d7a0c4",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890",
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                            UpdatedAt = DateTime.Now,
                        }
                    }) ;
                    
                    await context.SaveChangesAsync();   

                }    

                if(!context.Customers.Any()) 
                {
                    context.Customers.AddRange(
                        new List<Customer>()
                        {
                            new Customer() 
                            {
                                Id = Guid.NewGuid().ToString(),         
                                CreatedAt = DateTime.Now,       
                                IsDeleted = false,  
                                UpdatedAt = DateTime.Now,  
                                Address = "1, Lagos",
                                AppUserId = "2d16c229-9d00-ce9b-5a0d-d7f47513a9f3"
                            },
                            new Customer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreatedAt = DateTime.Now,
                                IsDeleted = false,
                                UpdatedAt = DateTime.Now,
                                Address = "1, Lagos",
                                AppUserId = "6b84487c-290a-22cc-40d5-e548fdb826e2"
                            },
                            new Customer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreatedAt = DateTime.Now,
                                IsDeleted = false,
                                UpdatedAt = DateTime.Now,
                                Address = "1, Lagos",
                                AppUserId = "2da1b50a-628c-4c13-b4a5-377f984c97c0"
                            },
                            new Customer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreatedAt = DateTime.Now,
                                IsDeleted = false,
                                UpdatedAt = DateTime.Now,
                                Address = "1, Lagos",
                                AppUserId = "6b84487c-290a-22cc-40d5-e548fdb826e2"
                            },
                            new Customer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreatedAt = DateTime.Now,
                                IsDeleted = false,
                                UpdatedAt = DateTime.Now,
                                Address = "1, Lagos",
                                AppUserId = "81cc184b-df68-818a-ec9a-6cbe6959ddb0"
                            },
                            new Customer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreatedAt = DateTime.Now,
                                IsDeleted = false,
                                UpdatedAt = DateTime.Now,
                                Address = "1, Lagos",
                                AppUserId = "48e2a27a-d4dc-70da-cdfc-cf5728d7a0c4"
                            }
                        }
                        );
                    context.SaveChanges();      
                }

                if (!context.ColdDrinks.Any())
                {
                   
                    context.ColdDrinks.AddRange(new List<ColdDrink>()         
                    {
                        new ColdDrink()
                        {
                           Id = "978b5a4a-3171-334a-42c1-94479d28a59a",
                           Name = "Coke",
                           Description = "cold to drink",
                           Price = 10,
                           ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1686985979/pexels-edward-eyer-2228889_tx0sme.jpg",
                           CustomerId = "0d7d22ba-b667-4c0e-b67f-89cd7a8ef366"


                        },

                        new ColdDrink()
                        {
                           Id = "586b4865-a995-e467-6762-c2e5c36507e7",
                           Name = "Coke",
                           Description = "cold to drink",
                           Price = 10,
                           ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1686985979/pexels-edward-eyer-2228889_tx0sme.jpg",
                           CustomerId = "1f1c97e0-df78-4891-98e7-f4aba4b1389c"

                        },

                       new ColdDrink()
                            {
                                Id = "2da1b50a-628c-4c13-b4a5-377f984c97c0",
                                Name = "Coke",
                                Description = "cold to drink",
                                Price = 10,
                                ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1686985979/pexels-edward-eyer-2228889_tx0sme.jpg",
                                 CustomerId = "1f1c97e0-df78-4891-98e7-f4aba4b1389c"

                            },

                        new ColdDrink()
                            {
                                Id = "c8aa2e99-8f18-7397-8938-b62b374c57ec",
                                Name = "Coke",
                                Description = "cold to drink",
                                Price = 10,
                                ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1686985979/pexels-edward-eyer-2228889_tx0sme.jpg",
                                 CustomerId = "1f1c97e0-df78-4891-98e7-f4aba4b1389c"

                            },

                        new ColdDrink()
                            {
                                Id = "70c851a5-39ab-f335-d33d-da3258ac5263",
                                Name = "Coke",
                                Description = "cold to drink",
                                Price = 10,
                                ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1686985979/pexels-edward-eyer-2228889_tx0sme.jpg",
                                 CustomerId = "1f1f8510-c8bb-4b53-9202-7b4ea47af266"

                            },

                        new ColdDrink()
                            {
                                Id = "8481a583-de99-d419-d7c1-51919be11da9",
                                Name = "Coke",
                                Description = "cold to drink",
                                Price = 10,
                                ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1686985979/pexels-edward-eyer-2228889_tx0sme.jpg",
                                 CustomerId = "1f1f8510-c8bb-4b53-9202-7b4ea47af266"

                            }

                    }
                        );
                    await context.SaveChangesAsync();

                }

                   if(!context.Foods.Any())
                {
                    
                        context.Foods.AddRange(new List<Food>()
                    {
                        new Food()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Rice",
                            Description = "Good for your",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687193904/ntachi-2-1110x1110-eited2-_pqh3iw.png",
                             CustomerId = "bec1771a-1204-46d1-ba84-630d0b330c4d"
                        },

                         new Food()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Rice",
                            Description = "Good for your",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687193904/ntachi-2-1110x1110-eited2-_pqh3iw.png",
                             CustomerId = "bec1771a-1204-46d1-ba84-630d0b330c4d"
                        },

                         new Food()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Rice",
                            Description = "Good for your",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687193904/ntachi-2-1110x1110-eited2-_pqh3iw.png",
                             CustomerId = "d5b49510-d5b4-495b-b99c-0153fd1f7542"
                        },

                        new Food()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Rice",
                            Description = "Good for your",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687193904/ntachi-2-1110x1110-eited2-_pqh3iw.png",
                             CustomerId ="d5b49510-d5b4-495b-b99c-0153fd1f7542"
                        },


                        new Food()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Rice",
                            Description = "Good for your",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687193904/ntachi-2-1110x1110-eited2-_pqh3iw.png",
                             CustomerId = "d5b49510-d5b4-495b-b99c-0153fd1f7542"
                        },

                    new Food()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Rice",
                            Description = "Good for your",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687193904/ntachi-2-1110x1110-eited2-_pqh3iw.png",
                             CustomerId = "d5b49510-d5b4-495b-b99c-0153fd1f7542"
                         }

                    }) ;
                    context.SaveChanges();
                }
                if (!context.HotDrinks.Any())
                {
                    
                    context.HotDrinks.AddRange(new List<HotDrink>()
                    {
                        new HotDrink()
                        {
                            Id = "5ddbee2d-a5ee-5a72-81ae-d2bef21b45ae",
                            Name = "Tea",
                            Description = "good for you",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687196526/mugcup_byavtn.png",
                            CustomerId = "e19cc8c8-e336-45d4-943f-d214c1eb4fa3"

                        },
                         new HotDrink()
                        {
                            Id = "0d8b88e4-3b00-db04-b608-65064c298de3",
                            Name = "Tea",
                            Description = "good for you",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687196526/mugcup_byavtn.png",
                            CustomerId = "e19cc8c8-e336-45d4-943f-d214c1eb4fa3"

                        },
                          new HotDrink()
                        {
                            Id = "61c22d56-ae5c-1bf5-e82c-996a295c8621",
                            Name = "Tea",
                            Description = "good for you",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687196526/mugcup_byavtn.png",
                            CustomerId = "e19cc8c8-e336-45d4-943f-d214c1eb4fa3"

                        },
                           new HotDrink()
                        {
                            Id = "7e2a29ca-f7a8-ce90-6745-c0b3f241823a",
                            Name = "Tea",
                            Description = "good for you",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687196526/mugcup_byavtn.png",
                            CustomerId = "0d7d22ba-b667-4c0e-b67f-89cd7a8ef366"

                        },
                            new HotDrink()
                        {
                            Id = "ca5079c0-4732-86c1-1012-de777ba82d03",
                            Name = "Tea",
                            Description = "good for you",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687196526/mugcup_byavtn.png",
                            CustomerId = "0d7d22ba-b667-4c0e-b67f-89cd7a8ef366"

                        },
                             new HotDrink()
                        {
                            Id = "bfa9a616-eb0e-987a-7337-03436f5e9c85",
                            Name = "Tea",
                            Description = "good for you",
                            Price = 10,
                            ImageURL = "https://res.cloudinary.com/dcrh4ouul/image/upload/v1687196526/mugcup_byavtn.png",
                            CustomerId = "0d7d22ba-b667-4c0e-b67f-89cd7a8ef366"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Orders.Any())
                {
                         
                    context.Orders.AddRange(
                        new List<Order>()
                        {
                            new Order()
                            {
                              Id = "97ecdee3-dce9-ef45-49f7-7586820435a5",
                              PhoneNumber = "09182828282",
                              Name = "John",
                              OrderDate = DateTime.Now,
                              TotalAmount = 250,
                              OrderReceipt = "97ecdee3-dce9-ef45-49f7-7586820435a5" + "Customer - John"+ "product - Bread"+ "quantity - 5" + "price - 50" + "total amount - 250",
                              Email = "victory@gmail.com",
                              CustomerId = "1f1c97e0-df78-4891-98e7-f4aba4b1389c"
                            },
                             new Order()
                            {
                              Id = "06d6a894-7e47-2fc8-6563-2ae52ee997bb",
                              PhoneNumber = "09182828282",
                              Name = "John",
                              OrderDate = DateTime.Now,
                              TotalAmount = 250,
                              OrderReceipt = "97ecdee3-dce9-ef45-49f7-7586820435a5" + "Customer - John"+ "product - Bread"+ "quantity - 5" + "price - 50" + "total amount - 250",
                              Email = "victory@gmail.com",
                              CustomerId ="1f1c97e0-df78-4891-98e7-f4aba4b1389c"
                            },
                              new Order()
                            {
                              Id = "bc0ff503-183b-e114-13c2-52b04fea7a68",
                              PhoneNumber = "09182828282",
                              Name = "John",
                              OrderDate = DateTime.Now,
                              TotalAmount = 250,
                              OrderReceipt = "97ecdee3-dce9-ef45-49f7-7586820435a5" + "Customer - John"+ "product - Bread"+ "quantity - 5" + "price - 50" + "total amount - 250",
                              Email = "victory@gmail.com",
                              CustomerId = "1f1f8510-c8bb-4b53-9202-7b4ea47af266"
                            },
                               new Order()
                            {
                              Id = "63778b0a-abd0-8375-7e31-5b5add5855fc",
                              PhoneNumber = "09182828282",
                              Name = "John",
                              OrderDate = DateTime.Now,
                              TotalAmount = 250,
                              OrderReceipt = "97ecdee3-dce9-ef45-49f7-7586820435a5" + "Customer - John"+ "product - Bread"+ "quantity - 5" + "price - 50" + "total amount - 250",
                              Email = "victory@gmail.com",
                              CustomerId = "1f1f8510-c8bb-4b53-9202-7b4ea47af266"
                            },
                                new Order()
                            {
                              Id = "6c7c2f0f-b70e-fbcc-9abe-fd308d99f7e0",
                              PhoneNumber = "09182828282",
                              Name = "John",
                              OrderDate = DateTime.Now,
                              TotalAmount = 250,
                              OrderReceipt = "97ecdee3-dce9-ef45-49f7-7586820435a5" + "Customer - John"+ "product - Bread"+ "quantity - 5" + "price - 50" + "total amount - 250",
                              Email = "victory@gmail.com",
                              CustomerId = "bec1771a-1204-46d1-ba84-630d0b330c4d"
                            },
                                 new Order()
                            {
                              Id = "062f6948-6a96-f1a0-9baf-6d55c86c96d2",
                              PhoneNumber = "09182828282",
                              Name = "John",
                              OrderDate = DateTime.Now,
                              TotalAmount = 250,
                              OrderReceipt = "97ecdee3-dce9-ef45-49f7-7586820435a5" + "Customer - John"+ "product - Bread"+ "quantity - 5" + "price - 50" + "total amount - 250",
                              Email = "victory@gmail.com",
                              CustomerId = "bec1771a-1204-46d1-ba84-630d0b330c4d"
                            }
                        }
                        );
                    context.SaveChanges();

                }
                if (!context.OrderItems.Any())
                {
                    context.OrderItems.AddRange(
                        new List<OrderItem>()
                        {
                           new OrderItem()
                           {
                               Id = "bd1b4485-a4aa-f378-6a56-e1c890fa3e6",
                               Product = "Bread",
                               Price = 100,
                               Quantity = 2,
                               OrderId = "97ecdee3-dce9-ef45-49f7-7586820435a5",

                           },
                            new OrderItem()
                           {
                               Id = "bc83d93c-1308-9d20-c993-8cd7c6c41221",
                               Product = "Bread",
                               Price = 100,
                               Quantity = 2,
                               OrderId = "06d6a894-7e47-2fc8-6563-2ae52ee997bb",

                           },
                             new OrderItem()
                           {
                               Id = "1f732a5b-3aac-0fe6-b589-8d55461efd8f",
                               Product = "Bread",
                               Price = 100,
                               Quantity = 2,
                               OrderId = "bc0ff503-183b-e114-13c2-52b04fea7a68",

                           },
                              new OrderItem()
                           {
                               Id = "e17e1c35-bb29-769b-4596-7ba10f22ebf5",
                               Product = "Bread",
                               Price = 100,
                               Quantity = 2,
                               OrderId = "63778b0a-abd0-8375-7e31-5b5add5855fc",

                           },
                               new OrderItem()
                           {
                               Id = "7cf3eb7b-56ec-b846-9d7a-8101e900cbbb",
                               Product = "Bread",
                               Price = 100,
                               Quantity = 2,
                               OrderId = "6c7c2f0f-b70e-fbcc-9abe-fd308d99f7e0",

                           },
                                new OrderItem()
                           {
                               Id = "b31fe78f-7c3c-aa76-238e-76b898d1b9a7",
                               Product = "Bread",
                               Price = 100,
                               Quantity = 2,
                               OrderId = "062f6948-6a96-f1a0-9baf-6d55c86c96d2",

                           }
                        }
                        );
                }
                context.SaveChanges();
            }


        }

        public static async Task SeedUserAndRoleAsync(IApplicationBuilder applicationBuilder)
        {
            using(var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var role = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                //create roles
                if(!await role.RoleExistsAsync(UserRoles.AdminRole))
                {
                   await role.CreateAsync(new IdentityRole(UserRoles.AdminRole));
                }
                if(!await role.RoleExistsAsync(UserRoles.CustomerRole))
                {
                    await role.CreateAsync(new IdentityRole(UserRoles.CustomerRole));
                }

                //create users

                var userService = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string email = "john@gmail.com";
                var userObject = await userService.FindByEmailAsync(email);
                if (userObject == null)
                {
                    AppUser user = new AppUser()
                    {
                        Id = "8fd24320-3f79-6765-26b2-ad0c6df5ea09",
                        UserName = "victor1234",
                        PhoneNumber = "08147483748",
                        FullName = "Joseph John",
                        Email = "john@gmail.com"
                    };
                    await userService.CreateAsync(user, "Eye12897@");

                    await userService.AddToRoleAsync(user, UserRoles.CustomerRole);

                }


            }
        }
    }
}
