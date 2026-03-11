using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("TipoUsuario")]
public partial class TipoUsuario
{
    [Key]
    [Column("idTipoUsuario")]
    public int IdTipoUsuario { get; set; }

    [Column("titulo")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Titulo { get; set; }

    [JsonIgnore]
    [InverseProperty("IdTipoUsuarioNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
