using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("admin")]
public class Admin
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("login")]
    public string? Login { get; set; }

    [Column("password")]
    public string? Password { get; set; }
}
