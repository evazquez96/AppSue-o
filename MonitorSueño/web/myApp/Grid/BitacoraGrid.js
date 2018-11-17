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

            //collection: null,//Al inicio la collection sera null.
            //farOffRemoval: 500,
            nuevoSueno: new NuevoSueno({

            }),
            myDialog: null,
            columns: [
                { field: 'Automatica', label: 'Automatica' },
                BitacoraHelper.formatoSemaforoColumn({ field: 'color_id', label: 'Semaforo' }),
                { field: 'duracion', label: 'Duración' },
                { field: 'tipoActividad', label: 'Actividad' },
                { field: 'usuario', label: 'Usuario' },
                { field: 'comentarios', label: 'Comentarios' },
                { field: 'fecha_inicio', label: 'Fecha Inicio' },
                { field: 'fecha_fin', label: 'Fecha Fin' },
                { field: 'id', label: 'Id' },
            ],


            _initEvents: function () {
                this.myDialog = new Dialog({
                    title: "Nuevo Sueño",
                    content: this.nuevoSueno
                });
                this.on(".dgrid-content .dgrid-row:dblclick", lang.hitch(this, function (event) {
                    var row = this.row(event);
                    this.myDialog.show();
                   // alert("click");
                }));

            }

        });
    });