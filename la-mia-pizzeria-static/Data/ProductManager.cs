using la_mia_pizzeria_static.Models;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Data
{
    public static class ProductManager
    {
        public static int CountProducts()
        {
            using ProductContext db = new ProductContext();
            return db.Products.Count();
        }
        public static int CountCategories()
        {
            using ProductContext db = new ProductContext();
            return db.Categories.Count();
        }
        public static List<Product> GetProducts()
        {
            using (ProductContext db = new ProductContext())  
            return db.Products.ToList();
        }

        public static List<Category> GetCategories()
        {
            using (ProductContext db = new ProductContext())
                return db.Categories.ToList();
        }

        public static Product GetProduct(int id)
        {
            using (ProductContext db = new ProductContext())  
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertProduct(Product product)
        {
            using ProductContext db = new ProductContext();
            product.Image ??= "/img/Marghe-pizza-bufala.webp";
            db.Products.Add(product);
            db.SaveChanges();
        }
        public static void InsertCategory(Category category)
        {
            using ProductContext db = new ProductContext();        
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public static bool UpdateProduct(int id, Action<Product> edit)
        {
            using ProductContext db = new ProductContext();
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return false;

            edit(product);

            db.SaveChanges();

            return true;
        }

        public static bool DeleteProduct(int id)
        {
            using ProductContext db = new ProductContext();
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return false;

            db.Products.Remove(product);
            db.SaveChanges();

            return true;
        }

        public static void Seed()
        {
            if (CountProducts() == 0)
            {
                InsertProduct(new Product("BOVINO PIEMONTESE", "Polpette con passata di pomodoro San Marzano Dop, Parmigiano Reggiano 30 mesi, basilico fresco e pane cotto al forno a legna.", "~/img/Marghe-polpette.webp", 6.50));
                InsertProduct(new Product("STRACCIATELLA PUGLIESE", "Servita su tegame freddo con pepe nero e olio extravergine d’oliva biologico.", "~/img/Marghe-stracciatella.webp", 4.50));
                InsertProduct(new Product("BURRATA PUGLIESE", "Servita con accompagnamento di datterini rossi e gialli a km 0 e olio extravergine d’oliva biologico.", "~/img/Marghe-burrata.webp", 7.50));

                InsertProduct(new Product("BUFALA", "Passata di pomodoro San Marzano. Dop, bufala campana Dop, pepe nero, basilico fresco, olio extravergine d’oliva biologico.", "~/img/Marghe-pizza-bufala.webp", 8.50));
                InsertProduct(new Product("NORMA", "Passata di pomodoro San Marzano Dop, provola affumicata d’Agerola, melanzane al forno, pomodorini semi dry, ricotta salata, basilico fresco, olio extra vergine d’oliva biologico.", "~/img/Marghe-Norma.webp", 10.50));
                InsertProduct(new Product("NAPOLI", "Passata di pomodoro San Marzano Dop, fior di latte d’Agerola, alici di Cetara, olive caiazzane, capperi di Salina, origano, basilico fresco, olio extravergine d’oliva biologico.", "~/img/marghe-napoli.webp", 9.50));
                InsertProduct(new Product("VEGANA", "Creazione della settimana con prodotti stagionali, ingredienti freschi e naturali. Perfetta per gli amanti della cucina vegetale.", "~/img/Marghe-Vegana.webp", 7.50));
                InsertProduct(new Product("DIAVOLA GIALLA", "Passata di pomodorini del Piannolo del Vesuvio gialli, fior di latte d’Agerola, salamino piccante di Secondigliano, fili di peperoncino, nduja, basilico fresco, olio extravergine d’oliva", "~/img/Marghe-Diavola.webp", 12.50));
                InsertProduct(new Product("MELANZANE E PORCHETTA", "Crema di melanzane cotte al forno, fior di latte, provola affumicata, porchetta d’Ariccia, taralli pugliesi, crema di pomodorini, rosmarino, olio extravergine d’oliva", "~/img/Marghe-Melanzane-e-porchetta.webp", 11.50));

                InsertProduct(new Product("TIRAMUSÙ", "Assapora il nostro Tiramisù con Ricotta di Bufala: savoiardi inzuppati nel caffè, ricotta di bufala cremosa e mascarpone, il tutto completato da una spolverata di cacao amaro", "~/img/Marghe-tiramisu.webp", 5.50));
                InsertProduct(new Product("SICILIA SCOMPOSTA", "Gusta il nostro Cannolo Siciliano Scomposto: croccante cialda, ricotta di pecora vellutata e gocce di cioccolato fondente. Un dolce che celebra gli autentici sapori siciliani in una presentazione moderna.", "~/img/Marghe-cannolo.webp", 4.50));
                InsertProduct(new Product("TORTA DEL GIORNO", "Prova la nostra Torta del Giorno: ingredienti freschi e stagionali selezionati con cura per offrirti ogni giorno una nuova delizia. Un dolce sorprendente che celebra i sapori autentici e la creatività del nostro chef.", "~/img/Marghe-torta-del-giorno.webp", 5.00));

                InsertProduct(new Product("BIBITE IN VETRO", "Coca Cola – Coca Cola Zero Fanta – Sprite", "~/img/Marghe-bibite-in-vetro.webp", 3.50));
                InsertProduct(new Product("ACQUA PANNA", "Bottiglia in vetro (50 cl).", "~/img/Marghe-Acqua-Panna.webp", 3.00));
                InsertProduct(new Product("BIRRA IN BOTTIGLIA", "Birra Moretti La Rossa 33cl.", "~/img/Marghe-Birra-bottiglia-rossa.webp", 4.00));
                InsertProduct(new Product("BIRRA IN BOTTIGLIA", "Birra Moretti La Bianca 33cl", "~/img/Marghe-Birra-bottiglia-bianca.webp", 4.00));
                InsertProduct(new Product("SPROSECCO DI VALDOBBIADENE", "DOCG extra dry colbelo - Casa Vinicola Merotto.", "~/img/Marghe-Prosecco-di-Valdobbiadene.webp", 24.50));
                InsertProduct(new Product("LE VEDUTE", "Note fruttate e floreali, questo vino offre un equilibrio perfetto tra freschezza e complessità.", "~/img/Marghe-vino-vedute.webp", 35.50));
            }

            if (CountCategories() == 0)
            {
                InsertCategory(new Category("ANTIPASTI"));
                InsertCategory(new Category("PIZZE"));
                InsertCategory(new Category("DOLCI"));
                InsertCategory(new Category("BEVANDE"));
                
            }
        }

    }
}
