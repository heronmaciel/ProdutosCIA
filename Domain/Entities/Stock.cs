using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Stock
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public int Quantity { get; set; }


        public Stock() { }
        public Stock(Guid productId, Guid companyId, int initialQuantity)
        {
            if (initialQuantity < 0)
                throw new ArgumentException("A Quantidade inicial precisa ser maior que 0 (zero).");

            ProductId = productId;
            CompanyId = companyId;
            Quantity = initialQuantity;

        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("A Quantidade precisa ser maior que 0 (zero).");

            Quantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade de estoque para baixar deve ser maior do que 0 (zero).");
            if (Quantity - quantity < 0)
                throw new InvalidOperationException("Estoque insuficiente para a baixa");

        }

    }
}
