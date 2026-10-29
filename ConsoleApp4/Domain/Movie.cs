namespace CollectionHierarchy.Domain
{
    public class Movie : CollectionItem
    {
        public string Director { get; }
        public int DurationMinutes { get; }

        public Movie(string title, int year, int rating, string director, int durationMinutes)
            : base(title, year, rating)
        {
            Director = director;
            DurationMinutes = durationMinutes;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"Режиссёр: {Director}");
            Console.WriteLine($"Длительность: {DurationMinutes} мин");
        }
    }
}