namespace Cargo.Core.Domain.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JsonValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}
