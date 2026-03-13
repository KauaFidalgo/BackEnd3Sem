using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("ComentarioEvento")]
public partial class ComentarioEvento
{
    [Key]
    [Column("idComentarioEvento")]
    public Guid IdComentarioEvento { get; set; }

    [Column("descricao")]
    [StringLength(200)]
    public string Descricao { get; set; } = null!;

    [Column("dataComentarioEvento", TypeName = "datetime")]
    public DateTime DataComentarioEvento { get; set; }

    [Column("exibe")]
    public bool Exibe { get; set; }

    [Column("idEvento")]
    public Guid? IdEvento { get; set; }

    [Column("idUsuario")]
    public Guid? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("ComentarioEventos")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("ComentarioEventos")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
