namespace Aciktim.Models
{
    public class OrderMenu
    {
        public int Omid { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
    }
}
