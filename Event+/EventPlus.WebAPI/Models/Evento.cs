using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Evento")]
public partial class Evento
{
    [Key]
    [Column("idEvento")]
    public Guid IdEvento { get; set; }

    [Column("nome")]
    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [Column("descricao", TypeName = "text")]
    public string Descricao { get; set; } = null!;

    [Column("dataEvento", TypeName = "datetime")]
    public DateTime DataEvento { get; set; }

    [Column("idInstituicao")]
    public Guid? IdInstituicao { get; set; }

    [Column("idTipoEvento")]
    public Guid? IdTipoEvento { get; set; }

    [InverseProperty("IdEventoNavigation")]
    public virtual ICollection<ComentarioEvento> ComentarioEventos { get; set; } = new List<ComentarioEvento>();

    [ForeignKey("IdInstituicao")]
    [InverseProperty("Eventos")]
    public virtual Instituicao? IdInstituicaoNavigation { get; set; }

    [ForeignKey("IdTipoEvento")]
    [InverseProperty("Eventos")]
    public virtual TipoEvento? IdTipoEventoNavigation { get; set; }

    [InverseProperty("IdEventoNavigation")]

    [JsonIgnore]
    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
}
