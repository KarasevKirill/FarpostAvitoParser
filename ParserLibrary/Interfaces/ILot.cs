namespace ParserLibrary
{
    public interface ILot
    {
        string Title { get; set; }

        string Price { get; set; }

        string Link { get; set; }

        string City { get; set; }

        string GetData();

        void Init(string price, string title, string link, string city, string[] trashwords, bool useTrashwords);
    }
}
