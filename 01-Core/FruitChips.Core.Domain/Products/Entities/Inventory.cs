using Core.Domain.Entities;

namespace FruitChips.Core.Domain.Products.Entities
{
    public class Inventory : BaseEntity<long>
    {
        public int Number { get; set; }
        public string DocNo { get; set; }

        public DateTime DocDate { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }

    }
}
