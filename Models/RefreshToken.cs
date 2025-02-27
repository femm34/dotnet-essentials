using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApi.Models

{
    [Table("refresh_token_entity")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        [Column("token")]
        public string Token { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        [Column("user_id")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}

