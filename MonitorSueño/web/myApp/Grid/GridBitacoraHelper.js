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

            formatoSemaforoColumn: function (column) {
                column.renderCell = function (object, value, cell, options, headers) {
                    var div = document.createElement("div");
                    switch (value) {

                        case "1":
                            /**
                             * Verde
                             * **/
                            domStyle.set(cell, "background-color","green");
                            break;
                        case "2":
                            /**
                             * Amarillo
                             * **/
                            domStyle.set(cell, "background-color", "yellow");
                            break;
                        default:
                            domStyle.set(cell, "background-color", "red");
                            /**
                             * Rojo
                             * **/
                            break;

                    }
                    return div;
                }
                return column;

            },
            defaultFormatColumn: function (column) {

                column.renderCell = function (object, value, cell, options, headers) {

                    var div = document.createElement("div");
                    div.innerHTML = value;
                    domStyle.set(div,"text-align","center")
                    return div;
                }

                return column;
            },
            diferenciaFormatColumn: function (column) {

                column.renderCell = function (object, value, cell, options, headers) {

                    var div = document.createElement("div");
                    var inicio
                    for (c in object.fechaInicio) {
                        //console.log(c)
                    }
                    var inicio = new Date(object.fechaInicio);
                    var fin = object.fechaFin;
                    div.innerHTML = value;
                    domStyle.set(div, "text-align", "center")
                    return div;
                }

                return column;
            }


        }

    });