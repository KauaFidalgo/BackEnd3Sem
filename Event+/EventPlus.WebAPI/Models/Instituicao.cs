using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Instituicao")]
[Index("Cnpj", Name = "UQ__Institui__AA57D6B4F6FF5CC6", IsUnique = true)]
public partial class Instituicao
{
    [Key]
    [Column("idInstituicao")]
    public Guid IdInstituicao { get; set; }

    [Column("nomeFantasia")]
    [StringLength(100)]
    public string NomeFantasia { get; set; } = null!;

    [Column("endereco")]
    [StringLength(200)]
    public string Endereco { get; set; } = null!;

    [Column("CNPJ")]
    [StringLength(14)]
    public string Cnpj { get; set; } = null!;

    [InverseProperty("IdInstituicaoNavigation")]

    [JsonIgnore]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
