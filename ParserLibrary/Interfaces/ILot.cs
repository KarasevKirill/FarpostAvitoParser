namespace ParserLibrary
{
    public interface ILot
    {
        string Title { get; set; }

        string Price { get; set; }

        string Link { get; set; }

        string City { get; set; }

        string[] Trashwords { get; set; }

        bool UseTrashwords { get; set; }

        string GetData();

        void RemoveTrashwords();
    }
}
