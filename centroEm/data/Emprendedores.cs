using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Emprendedor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_emprendedor { get; set; }

    [Required]
    [StringLength(25)]
    public string nombre { get; set; }

    [Required]
    [StringLength(100)]
    public string email { get; set; }

    [StringLength(20)]
    public string telefono { get; set; }

    [StringLength(255)]
    public string nombre_proyecto { get; set; }

    public string descripcion_proyecto { get; set; }

    [StringLength(50)]
    public string sector { get; set; }

    public DateTime fecha_registro { get; set; }
}
