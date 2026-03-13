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
    public Guid IdPresenca { get; set; }

    [Column("situacao")]
    public bool Situacao { get; set; }

    [Column("idEvento")]
    public Guid? IdEvento { get; set; }

    [Column("idUsuario")]
    public Guid? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Presencas")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Presencas")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
