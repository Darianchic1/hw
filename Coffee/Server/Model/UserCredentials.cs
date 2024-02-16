namespace Coffee;
using System.ComponentModel.DataAnnotations;

public class UserCredentials
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string user { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string pass { get; set; }

}
