using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("client")]
public class Client
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("email")]
    public string? Email { get; set; }
}
