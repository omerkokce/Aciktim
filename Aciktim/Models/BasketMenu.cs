namespace Aciktim.Models
{
    public class BasketMenu
    {
        public int Bmid { get; set; }
        public int ClientId { get; set; }
        public int MenuId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
    }
}
