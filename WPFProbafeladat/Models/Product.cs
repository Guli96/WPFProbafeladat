using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProbafeladat.Models
{
    public class Product
    {
        public int Id { get; set; }

        /// <summary>
        /// Cikkszám
        /// </summary>
        public string Code { get; set; } 

        /// <summary>
        /// Termék neve
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Leírás
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ár (forintban)
        /// </summary>
        public decimal Price { get; set; }
    }

}
