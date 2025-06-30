using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // 👤 Admin-gebruiker met gehasht wachtwoord
            if (!context.Users.Any())
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword("beheer123");

                context.Users.Add(new User
                {
                    Username = "admin",
                    Password = hashedPassword,
                    Role = "Admin"
                });

                context.SaveChanges();
            }

            // 👥 Klanten gebaseerd op Matrix-personages
            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new() { Name = "Neo", Address = "123 Elm St", Active = true },
                    new() { Name = "Morpheus", Address = "456 Oak St", Active = true },
                    new() { Name = "Trinity", Address = "789 Pine St", Active = true }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // 🛠️ Producten geïnspireerd op de Matrix
            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new() {
                        Name = "Nebuchadnezzar",
                        Description = "Hovercraft waarmee Morpheus en crew opereren buiten de Matrix",
                        Price = 10000.00m,
                        Active = true
                    },
                    new() {
                        Name = "Jack-in Chair",
                        Description = "Stoel waarmee crewleden ingeplugd worden in de Matrix",
                        Price = 500.50m,
                        Active = true
                    },
                    new() {
                        Name = "EMP Device",
                        Description = "Electro-Magnetic Pulse wapen dat Sentinels uitschakelt",
                        Price = 129.99m,
                        Active = true
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // 🔩 Onderdelen voor sci-fi toepassingen
            if (!context.Parts.Any())
            {
                var parts = new Part[]
                {
                    new() { Name = "Tandwiel", Description = "Voor overbrenging van kracht in motoren en deuren", Active = true },
                    new() { Name = "M5 Boutje", Description = "Bevestigt panelen in hovercrafts zoals de Nebuchadnezzar", Active = true },
                    new() { Name = "Hydraulische cilinder", Description = "Voor het openen van zware luchtsluizen", Active = true },
                    new() { Name = "Koelvloeistofpomp", Description = "Koelt energie-units aan boord van schepen", Active = true }
                };

                context.Parts.AddRange(parts);
                context.SaveChanges();
            }

            // 🛒 Orders aan klanten koppelen
            if (!context.Orders.Any())
            {
                var customers = context.Customers.ToList();

                var orders = new Order[]
                {
                    new() { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01") },
                    new() { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01") },
                    new() { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01") },
                    new() { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01") }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }

            // 🧾 OrderProduct-koppelingen met hoeveelheden
            if (!context.OrderProducts.Any())
            {
                var orders = context.Orders.ToList();
                var products = context.Products.ToList();

                var orderProducts = new OrderProduct[]
                {
                    new() { OrderId = orders[0].Id, ProductId = products[0].Id, Quantity = 2 },
                    new() { OrderId = orders[0].Id, ProductId = products[1].Id, Quantity = 1 },
                    new() { OrderId = orders[1].Id, ProductId = products[2].Id, Quantity = 3 },
                    new() { OrderId = orders[2].Id, ProductId = products[0].Id, Quantity = 1 },
                    new() { OrderId = orders[3].Id, ProductId = products[1].Id, Quantity = 2 }
                };

                context.OrderProducts.AddRange(orderProducts);
                context.SaveChanges();
            }
        }
    }
}
