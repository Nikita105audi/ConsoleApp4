namespace CollectionHierarchy.Domain
{
    public class InvalidCollectionDataException : Exception
    {
        public InvalidCollectionDataException(string message) : base(message) { }
    }
}