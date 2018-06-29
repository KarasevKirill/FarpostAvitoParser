namespace Parser.Interfaces
{
    interface ILot
    {
        string Title { get; set; }

        string Price { get; set; }

        string Link { get; set; }

        string City { get; set; }

        string GetData();
    }
}
