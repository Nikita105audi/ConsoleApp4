namespace CollectionHierarchy.Domain
{
    public class Book : CollectionItem
    {
        public string Author { get; }
        public int Pages { get; }

        public Book(string title, int year, int rating, string author, int pages)
            : base(title, year, rating)
        {
            Author = author;
            Pages = pages;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine($"Страниц: {Pages}");
        }
    }
}