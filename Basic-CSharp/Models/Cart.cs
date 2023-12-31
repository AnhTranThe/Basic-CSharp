﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_CSharp.Models
{
    [Table("CARTS")]
    public class Cart
    {

        public Guid CartId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public virtual ICollection<CartDetail> cartDetails { get; set; } = new List<CartDetail>();

    }
}
