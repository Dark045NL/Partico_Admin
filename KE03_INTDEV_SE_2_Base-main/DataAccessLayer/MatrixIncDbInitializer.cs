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
                    new Customer { Name = "Neo", Address = "123 Elm St", Active = true },
                    new Customer { Name = "Morpheus", Address = "456 Oak St", Active = true },
                    new Customer { Name = "Trinity", Address = "789 Pine St", Active = true }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges(); // 📌 Nodig vóór orders
            }

            // 🛒 Orders aan klanten koppelen
            if (!context.Orders.Any())
            {
                var customers = context.Customers.ToList(); // Nu gevuld door vorige SaveChanges()

                var orders = new Order[]
                {
                    new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01") },
                    new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01") },
                    new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01") },
                    new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01") }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }

            // 🛠️ Producten geïnspireerd op de Matrix
            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product
                    {
                        Name = "Nebuchadnezzar",
                        Description = "Hovercraft waarmee Morpheus en crew opereren buiten de Matrix",
                        Price = 10000.00m
                    },
                    new Product
                    {
                        Name = "Jack-in Chair",
                        Description = "Stoel waarmee crewleden ingeplugd worden in de Matrix",
                        Price = 500.50m
                    },
                    new Product
                    {
                        Name = "EMP Device",
                        Description = "Electro-Magnetic Pulse wapen dat Sentinels uitschakelt",
                        Price = 129.99m
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
                    new Part { Name = "Tandwiel", Description = "Voor overbrenging van kracht in motoren en deuren" },
                    new Part { Name = "M5 Boutje", Description = "Bevestigt panelen in hovercrafts zoals de Nebuchadnezzar" },
                    new Part { Name = "Hydraulische cilinder", Description = "Voor het openen van zware luchtsluizen" },
                    new Part { Name = "Koelvloeistofpomp", Description = "Koelt energie-units aan boord van schepen" }
                };

                context.Parts.AddRange(parts);
                context.SaveChanges();
            }
        }
    }
}
