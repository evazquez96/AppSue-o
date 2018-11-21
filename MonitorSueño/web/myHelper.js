function createDate(cadena) {
    /***Crea una fecha a partir del formato dd/mm/yyyy hh:mm:ss***/
    var anio = cadena.substr(6, 4);
    var mes = cadena.substr(0, 2);
    var dia = cadena.substr(3, 2);
    var hora = cadena.substr(11, 2);
    var minuto = cadena.substr(14, 2);
    var segundo = cadena.substr(17, 2);
    return new Date(
        anio,
        mes,
        dia,
        hora,
        minuto,
        segundo
    );
}