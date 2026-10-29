using System;

namespace CollectionHierarchy.Domain
{
    public abstract class CollectionItem
    {
        private string _title;
        private int _rating;

        protected int Year { get; }

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidCollectionDataException("Название не может быть пустым.");
                _title = value;
            }
        }

        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 1 || value > 10)
                    throw new InvalidCollectionDataException("Оценка должна быть от 1 до 10.");
                _rating = value;
            }
        }

        protected CollectionItem(string title, int year, int rating)
        {
            Title = title;
            Year = year;
            Rating = rating;
        }

        // Без virtual полиморфный вызов в цикле foreach всегда вызывал бы эту базовую версию.
        // Благодаря virtual/override вызывается реализация конкретного типа.
        public virtual void Describe()
        {
            Console.WriteLine($"Название: {Title}");
            Console.WriteLine($"Год: {Year}");
            Console.WriteLine($"Оценка: {Rating}/10");
        }

        public override string ToString()
        {
            return $"{Title} ({Year}) — {Rating}/10";
        }
    }
}