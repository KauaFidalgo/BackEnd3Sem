using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Presenca")]
public partial class Presenca
{
    [Key]
    [Column("idPresenca")]
    public int IdPresenca { get; set; }

    [Column("situacao")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Situacao { get; set; }

    [Column("idEvento")]
    public int? IdEvento { get; set; }

    [Column("idUsuario")]
    public int? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Presencas")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Presencas")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
