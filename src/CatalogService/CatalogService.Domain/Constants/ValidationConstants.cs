namespace CatalogService.Domain.Constants
{
    public static class ValidationConstants
    {
        public static string NoEmptyMessage = "This field is required!";

        public static int MaxBrandNameLenght = 50;
        public static int MaxBrandURLLenght = 255;
        public static string MaxBrandNameLenghtMessage = "Brands name should be less than 50 characters";

        public static int MaxCategoryNameLenght = 50;
        public static string MaxCategoryNameLenghtMessage = "Category name should be less than 50 characters";

        public static int MaxProductAttributeNameLenght = 50;
        public static string MaxProductAttributeNameLenghtMessage = "Product attribute name should be less than 50 characters";

        public static int MaxProductAttributeValueLenght = 50;
        public static string MaxProductAttributeValueLenghtMessage = "Product attribute value should be less than 50 characters";

        public static int MaxProductNameLenght = 50;
        public static string MaxProductNameLenghtMessage = "Product name value should be less than 50 characters";

        public static int MaxProductDescriptionLenght = 500;
        public static string MaxProductDescriptionLenghtMessage = "Product description value should be less than 500 characters";

        public static int MinProductPrice= 1;
        public static string MinProductPriceMessage = "Product prict value should be greater than 1 digit!";
    }
}
