namespace CollectionHierarchy.Domain
{
    public class Game : CollectionItem
    {
        public string Developer { get; }
        public string Genre { get; }

        public Game(string title, int year, int rating, string developer, string genre)
            : base(title, year, rating)
        {
            Developer = developer;
            Genre = genre;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"Разработчик: {Developer}");
            Console.WriteLine($"Жанр: {Genre}");
        }
    }
}