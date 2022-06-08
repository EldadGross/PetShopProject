using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Data.Model
{
    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = null!;
        [StringLength(20)]
        public string Password { get; set; } = null!;
    }
}
