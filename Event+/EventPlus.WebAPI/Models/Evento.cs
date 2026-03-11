using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Evento")]
public partial class Evento
{
    [Key]
    [Column("idEvento")]
    public int IdEvento { get; set; }

    [Column("nome")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Nome { get; set; }

    [Column("descricao", TypeName = "text")]
    public string? Descricao { get; set; }

    [Column("data")]
    public DateOnly? Data { get; set; }

    [Column("idInstituicao")]
    public int? IdInstituicao { get; set; }

    [Column("idTipoEvento")]
    public int? IdTipoEvento { get; set; }

    [JsonIgnore]
    [InverseProperty("IdEventoNavigation")]
    public virtual ICollection<ComentarioEvento> ComentarioEventos { get; set; } = new List<ComentarioEvento>();

    [ForeignKey("IdInstituicao")]
    [InverseProperty("Eventos")]
    public virtual Instituicao? IdInstituicaoNavigation { get; set; }

    [ForeignKey("IdTipoEvento")]
    [InverseProperty("Eventos")]
    public virtual TipoEvento? IdTipoEventoNavigation { get; set; }

    [JsonIgnore]
    [InverseProperty("IdEventoNavigation")]
    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
}
