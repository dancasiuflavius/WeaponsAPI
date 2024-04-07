using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeaponsAPI.Weapons.Model
{
    [Table("weapons")]
    public class Weapon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int Id { get; set; }
        [Required]
        [Column("name")]

        public string Name { get; set; }
        [Required]
        [Column("category")]

        public string Category { get; set; }
        [Required]
        [Column("price")]

        public double price { get; set; }
        
    }
}
