using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Usuario
/// </summary>
public class Usuario
{

    public virtual int Id { get; set; }
    public virtual String nombre { get; set; }
    public virtual int Driver_Type_Id { get; set; }
    public virtual int Group_Id { get; set; }
    public virtual Monitor monitor { get; set; }
    public virtual int activo { get; set; }
    public Usuario()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}