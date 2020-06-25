namespace gRPCDotNetCoreClient.Data
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public int Pages { get; set; }

        public float Cost { get; set; }

        public BookDto(int id, string name, string author, float cost, int pages)
        {
            this.Id = id;
            this.Name = name;
            this.Author = author;
            this.Pages = pages;
            this.Cost = cost;

        }
    }
}