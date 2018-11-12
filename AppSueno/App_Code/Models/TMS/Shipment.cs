using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Shipment
{
    public virtual long Id { get; set; }
    public virtual int Operation_Type { get; set; }
    public virtual String External_Id { get; set; } //No. de Solicitud
    public Shipment()
    {

    }
}