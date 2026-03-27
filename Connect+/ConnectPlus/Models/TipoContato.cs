using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.Models;

[Table("TipoContato")]
public partial class TipoContato
{
    [Key]
    public Guid IdTipoContato { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [InverseProperty("IdTipoContatoNavigation")]
    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}
