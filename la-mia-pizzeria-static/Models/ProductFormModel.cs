namespace la_mia_pizzeria_static.Models
{
    public class ProductFormModel
    {
        public Product Product { get; set; }
        public List<Category>? Categories { get; set; }

        public ProductFormModel() { }

        public ProductFormModel(Product product, List<Category>? categories)
        {
            Product = product;
            Categories = categories;
        }
    }
}
