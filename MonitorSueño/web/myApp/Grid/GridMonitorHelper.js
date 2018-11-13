define([
    'dojo/_base/declare',
    'dojo/_base/lang',
    "dojo/dom-style",
    "dojo/domReady!"
], function (
    declare,
    lang,
    domStyle,
    ) {

        return {

            centrarContenido: function (value) {
                var div = document.createElement("div");
                div.innerHTML = value;
                div.style.setProperty("text-align", "center", "important");
                return div;
            },

            formato24SPlan: function (column) {
                column.renderCell = function (object, value, cell, options, headers) {
                    var div = document.createElement("div");
                    domStyle.set(div, "text-align", "center");
                    div.innerHTML=value
                    var z = value.substring(0,2);
                    var aux = parseInt(z)
                    if (aux > 6) {
                        domStyle.set(div, "background-color", "green");
                        domStyle.set(cell, "background-color", "green");
                    } else if (aux > 5 && aux < 6) {
                        domStyle.set(div, "background-color", "yellow");
                        domStyle.set(cell, "background-color", "yellow");
                    } else {
                        domStyle.set(div, "background-color", "red");
                        domStyle.set(cell, "background-color", "red");
                    }
                    return div;
                }

                return column;

            },
            formatoActividadColumn: function (column) {

                column.renderCell = function (object, value, cell, options, headers) {
                    var div = document.createElement("div");
                    domStyle.set(div, "text-align", "center");
                   
                    switch (value) {
                        case -1:
                            div.innerHTML = "ASIGNADO";
                            break;
                        case 2:
                            div.innerHTML = "ACTIVO";
                            break;
                        case 3:
                            div.innerHTML = "DESCANSO";
                            break;
                        case 4:
                            div.innerHTML = "INACTIVO";
                            break;
                        case 1:
                            div.innerHTML = "SUEÑO";
                            break;

                    }
                    return div;
                }

                return column;
            },

            formatoRojoColumn: function (column) {
                column.renderHeaderCell = function (node) {
                    var div = document.createElement('div');
                    /**Creamos un nuevo div donde se pondra el encabezado de la columna**/
                    div.innerHTML = column.label;//Se pone la etiqueta de la columna
                    domStyle.set(div, "background-color", "red");
                    domStyle.set(node, "background-color", "red");

                    domStyle.set(div, "text-align", "center");
                    return div;
                }
                return column;//Se regresa la columna ya con los estilos
            },

            formatoAmarilloColumn: function (column) {
                column.renderHeaderCell = function (node) {
                    var div = document.createElement('div');
                    /**Creamos un nuevo div donde se pondra el encabezado de la columna**/
                    div.innerHTML = column.label;//Se pone la etiqueta de la columna
                    domStyle.set(div, "background-color", "yellow");
                    domStyle.set(node, "background-color", "yellow");
                    domStyle.set(div, "text-align", "center");
                    return div;
                }
                return column;//Se regresa la columna ya con los estilos
            },
            formatoVerdeColumn: function (column) {
                column.renderHeaderCell = function (node) {
                    var div = document.createElement('div');
                    /**Creamos un nuevo div donde se pondra el encabezado de la columna**/
                    div.innerHTML = column.label;//Se pone la etiqueta de la columna
                    domStyle.set(div, "background-color", "Green");
                    domStyle.set(node, "background-color", "green");
                    domStyle.set(div, "text-align", "center");
                    return div;
                }
                return column;//Se regresa la columna ya con los estilos
            }


        }

    });