using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("TipoEvento")]
public partial class TipoEvento
{
    [Key]
    [Column("idTipoEvento")]
    public int IdTipoEvento { get; set; }

    [Column("titulo")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Titulo { get; set; }

    [JsonIgnore]
    [InverseProperty("IdTipoEventoNavigation")]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
