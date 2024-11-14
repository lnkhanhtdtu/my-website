using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Application.DTOs.ViewModels
{
    public class QuotationViewModel
    {
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int? ProductId { get; set; }
        public string? Content { get; set; }
    }
}
