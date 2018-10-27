using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class Sample
    {
        public object Id { get; set; }
        [Key]
        public int ApplicationId { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public double Amount { get; set; }
        public DateTime PostingDate { get; set; }
        public bool IsCleared { get; set; }
        public DateTime? ClearedDate { get; set; }
    }
}