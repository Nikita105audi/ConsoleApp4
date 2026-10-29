using CollectionHierarchy.Domain;
using System.Globalization;

class Program
{
    static CollectionItem[] items = new CollectionItem[0];

    static void Main()
    {
        InitializeDemoData();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Личная коллекция ===");
            Console.WriteLine("1. Показать всю коллекцию");
            Console.WriteLine("2. Добавить позицию");
            Console.WriteLine("3. Найти по названию");
            Console.WriteLine("4. Средняя оценка");
            Console.WriteLine("5. Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        PrintTable();
                        break;
                    case "2":
                        AddItem();
                        break;
                    case "3":
                        SearchByTitle();
                        break;
                    case "4":
                        PrintAverageRating();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }
            }
            catch (InvalidCollectionDataException ex)
            {
                Console.WriteLine($"Ошибка данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    static void InitializeDemoData()
    {
        items = new CollectionItem[10];
        items[0] = new Movie("Брат", 1997, 10, "Алексей Балабанов", 95);
        items[1] = new Movie("Кин-дза-дза!", 1986, 9, "Георгий Данелия", 135);
        items[2] = new Game("Мор. Утопия", 2005, 9, "Ice-Pick Lodge", "Приключение");
        items[3] = new Game("Космические рейнджеры 2", 2004, 10, "Elemental Games", "Стратегия/RPG");
        items[4] = new Book("Пикник на обочине", 1972, 10, "Братья Стругацкие", 192);
        items[5] = new Book("Трудно быть богом", 1964, 9, "Братья Стругацкие", 256);
        items[6] = new Movie("О чём говорят мужчины", 2010, 8, "Дмитрий Дьяченко", 93);
        items[7] = new Game("Мор (Утопия) — У мраморного гнёзда", 2017, 8, "Ice-Pick Lodge", "Визуальная новелла");
        items[8] = new Book("Понедельник начинается в субботу", 1965, 10, "Братья Стругацкие", 320);
        items[9] = new Movie("Брат 2", 2000, 9, "Алексей Балабанов", 122);
    }

    static void PrintTable()
    {
        Console.WriteLine("\nВся коллекция:");
        string header = string.Format(CultureInfo.InvariantCulture, "{0,-4} | {1,-25} | {2,-10} | {3,-6} | {4}",
            "Тип", "Название", "Год", "Оценка", "Специфика");
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length));

        foreach (var item in items)
        {
            if (item == null) continue;

            string type = item switch
            {
                Movie => "Фильм",
                Game => "Игра",
                Book => "Книга",
                _ => "Объект"
            };

            string spec = item switch
            {
                Movie m => $"{m.Director} | {m.DurationMinutes} мин",
                Game g => $"{g.Developer} | {g.Genre}",
                Book b => $"{b.Author} | {b.Pages} стр",
                _ => ""
            };

            string line = string.Format(CultureInfo.InvariantCulture, "{0,-4} | {1,-25} | {2,-10} | {3,-6} | {4}",
                type, item.Title, item.Year, item.Rating, spec);
            Console.WriteLine(line);
        }
    }

    static void AddItem()
    {
        Console.Write("Тип (фильм/игра/книга): ");
        string type = Console.ReadLine()?.Trim().ToLower();

        Console.Write("Название: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Год: ");
        if (!int.TryParse(Console.ReadLine(), out int year)) throw new InvalidCollectionDataException("Год должен быть числом.");

        Console.Write("Оценка (1-10): ");
        if (!int.TryParse(Console.ReadLine(), out int rating)) throw new InvalidCollectionDataException("Оценка должна быть числом.");

        switch (type)
        {
            case "фильм":
                Console.Write("Режиссёр: ");
                string director = Console.ReadLine() ?? "";
                Console.Write("Длительность (мин): ");
                if (!int.TryParse(Console.ReadLine(), out int duration)) throw new InvalidCollectionDataException("Длительность должна быть числом.");
                AddToArray(new Movie(title, year, rating, director, duration));
                break;
            case "игра":
                Console.Write("Разработчик: ");
                string dev = Console.ReadLine() ?? "";
                Console.Write("Жанр: ");
                string genre = Console.ReadLine() ?? "";
                AddToArray(new Game(title, year, rating, dev, genre));
                break;
            case "книга":
                Console.Write("Автор: ");
                string author = Console.ReadLine() ?? "";
                Console.Write("Страниц: ");
                if (!int.TryParse(Console.ReadLine(), out int pages)) throw new InvalidCollectionDataException("Страниц должно быть числом.");
                AddToArray(new Book(title, year, rating, author, pages));
                break;
            default:
                throw new InvalidCollectionDataException("Неизвестный тип позиции.");
        }

        Console.WriteLine("Позиция добавлена.");
    }

    static void AddToArray(CollectionItem item)
    {
        var newArray = new CollectionItem[items.Length + 1];
        items.CopyTo(newArray, 0);
        newArray = item;
        items = newArray;
    }

    static void SearchByTitle()
    {
        Console.Write("Введите часть названия: ");
        string needle = Console.ReadLine()?.ToLower() ?? "";
        bool found = false;

        foreach (var item in items)
        {
            if (item != null && item.Title.ToLower().Contains(needle))
            {
                Console.WriteLine("\n--- Найдено ---");
                item.Describe();
                Console.WriteLine("---------------");
                found = true;
            }
        }

        if (!found) Console.WriteLine("Ничего не найдено.");
    }

    static void PrintAverageRating()
    {
        int count = 0;
        int sum = 0;
        foreach (var item in items)
        {
            if (item != null)
            {
                sum += item.Rating;
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }

        double avg = (double)sum / count;
        Console.WriteLine($"Средняя оценка: {avg:F2}");
    }
}