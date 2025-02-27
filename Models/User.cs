using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApi.Models
{
    [Table("user_entity")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
    
        [Required]
        [StringLength(40)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;
    
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("first_last_name")]
        public string FirstLastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("second_last_name")]
        public string SecondLastName { get; set; } = string.Empty;
        
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}