namespace Tudo_List.Domain.Models
{
    public class Image
    {
        public string Name { get; set; } = null!;
        public byte[] Data { get; set; } = [];
        public string ContentType { get; set; } = null!;
    }

}
