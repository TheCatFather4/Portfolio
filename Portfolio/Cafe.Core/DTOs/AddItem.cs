using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Core.DTOs
{
    public class AddItem
    {
        public int ItemId { get; set; }
        public byte Quantity { get; set; }
    }
}
