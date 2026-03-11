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
    public int IdComentarioEvento { get; set; }

    [Column("descricao", TypeName = "text")]
    public string? Descricao { get; set; }

    [Column("data")]
    public DateOnly? Data { get; set; }

    [Column("exibe")]
    public bool? Exibe { get; set; }

    [Column("idEvento")]
    public int? IdEvento { get; set; }

    [Column("idUsuario")]
    public int? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("ComentarioEventos")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("ComentarioEventos")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
