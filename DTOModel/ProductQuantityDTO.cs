using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class ProductQuantityDTO
    {

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
   
    public class QuantityReponseDTO
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
