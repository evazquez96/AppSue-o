define([
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/_base/array",
    "dojo/when",
    "dojo/request/xhr",
    "dojo/json",
    "dojo/dom-construct",
    "dojo/Stateful",
    "dstore/Memory",
    "dgrid/OnDemandGrid",
    "dgrid/grid",
    'dgrid/extensions/DijitRegistry',
    "dgrid/Selection",
    "dgrid/Editor",
    "dgrid/Keyboard",
    "dojo/dom-style",
    "dijit/form/ValidationTextBox",
    "dijit/form/CheckBox",
    "dijit/form/DateTextBox",
    "dijit/Dialog",
    "/web/myApp/Grid/GridBitacoraHelper.js",
    "/web/myApp/widget/NuevoSuenoPaneWidget.js",
    "dojo/domReady!"
], function (
    declare,
    lang,
    array,
    when,
    request,
    JSON,
    domConstruct,
    Stateful,
    Memory,
    OnDemandGrid,
    Dgrid,
    DijitRegistry,
    Selection,
    Editor,
    Keyboard,
    domStyle,
    ValidationTextBox,
    CheckBox,
    DateTextBox,
    Dialog,
    BitacoraHelper,
    NuevoSueno
) {
        return declare([OnDemandGrid, Dgrid, DijitRegistry, Selection, Editor, Keyboard], {

            nuevoSueno: new NuevoSueno({

            }),
            myDialog: null,
            master:null,
            columns: [
                BitacoraHelper.defaultFormatColumn({ field: 'fechaInicio', label: 'Fecha Inicio' }),
                BitacoraHelper.defaultFormatColumn({ field: 'fechaFin', label: 'Fecha Fin' }),
                { field: 'Automatica', label: 'Automatica' },
                BitacoraHelper.isMexAppColumn({
                    editor: CheckBox,
                    field: "IsMexApp",
                    label: "MexApp",
                    get: function (item) {
                        // ensure initial rendering matches up with widget behavior
                        //debugger
                        return item.IsMexApp=="0" ? false : true;
                    }
                   
                }),
                BitacoraHelper.formatoSemaforoColumn({ field: 'color_id', label: 'Semaforo' }),
                BitacoraHelper.diferenciaFormatColumn({ field: 'duracion', label: 'Duración' }),
                BitacoraHelper.defaultFormatColumn({ field: 'tipoActividad', label: 'Actividad' }),
                BitacoraHelper.defaultFormatColumn({ field: 'usuario', label: 'Usuario' }),
                BitacoraHelper.defaultFormatColumn({ field: 'comentarios', label: 'Comentarios' }),
                
                //{ field: 'id', label: 'Id' },
            ],

            _initEvents: function () {
                this.myDialog = new Dialog({
                    title: "Nuevo Sueño",
                    content: this.nuevoSueno,
                    style:"width:35%!important;"
                });
                this.on(".dgrid-content .dgrid-row:dblclick", lang.hitch(this, function (event) {
                    var row = this.row(event);
                    //debugger;
                    //this.myDialog.content.iniciarFecha();
                    this.myDialog.content._setValues(row.data, this.master.infoOperador);
                    this.myDialog.content._disabledOrEnabledWidgets(true);
                    /***
                     * Manda el objeto con los valores para cambiarlos en el panel
                     * de la inserción de sueño.
                     * **/
                    this.myDialog.show();
                   // alert("click");
                }));

            }

        });
    });