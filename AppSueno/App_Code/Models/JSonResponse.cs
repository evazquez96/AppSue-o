using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de JSonResponse
/// </summary>
public class JSonResponse
{
    public List<JsonFormat> list { get; set; }
    public Driver driver { get; set; }
    public Vehicle vehicle { get; set; }
    public String Operation_Type_Alias{ get; set; }
    public String Marca_y_Modelo { get; set; }
    /**
     *  Si Operation_Type=2 entonces Operation_Type_Alias=T3-S2-R4
     *      si no entonces Operation_Type_Alias=T3-S2   
     */

    public JSonResponse()
    {

    }
}