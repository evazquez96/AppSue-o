function createDate(cadena) {
    /***Crea una fecha a partir del formato dd/mm/yyyy hh:mm:ss***/
    var anio = cadena.substr(6, 4);
    var dia = cadena.substr(0, 2);
    var mes = cadena.substr(3, 2);
    var hora = cadena.substr(11, 2);
    var minuto = cadena.substr(14, 2);
    var segundo = cadena.substr(17, 2);
    return new Date(
        anio,
        parseInt(mes)-1,
        dia,
        hora,
        minuto,
        segundo
    );
}

function centerHeaderCell(text,domStyle) {
    var div = document.createElement('div');
    /**Creamos un nuevo div donde se pondra el encabezado de la columna**/
    div.innerHTML = text;//Se pone la etiqueta de la columna
    domStyle.set(div, "text-align", "center");

    return div;
}