using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Instituicao")]
public partial class Instituicao
{
    [Key]
    [Column("idInstituicao")]
    public int IdInstituicao { get; set; }

    [Column("nomeFantasia")]
    [StringLength(100)]
    [Unicode(false)]
    public string? NomeFantasia { get; set; }

    [Column("endereco")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Endereco { get; set; }

    [Column("CNPJ")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Cnpj { get; set; }

    [JsonIgnore]
    [InverseProperty("IdInstituicaoNavigation")]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
