namespace SolutionsForBuisnesTestTask.Domain.ViewModels
{
    public class SaveOrderVM
    {
        public int Id { get; set; }

        public string Number { get; set; }
        public string Date { get; set; }
        public int ProviderId { get; set; }

        public List<SaveItemVM> Items { get; set;}
    }

    public class SaveItemVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
    }
}
