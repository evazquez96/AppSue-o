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
    "/web/myApp/Grid/GridMonitorHelper.js",
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
    MonitorHelper
) {
        return declare([OnDemandGrid, Dgrid, DijitRegistry, Selection, Editor, Keyboard], {

            //collection: null,//Al inicio la collection sera null.
            //farOffRemoval: 500,
            columns: [
                { field: 'Automatica', label: 'Automatica' },
                { field: 'Semaforo', label: 'Semaforo' },
                { field: 'duracion', label: 'Duración' },
                { field: 'Actividad', label: 'Actividad' },
                { field: 'usuario', label: 'Usuario' },
                { field: 'comentarios', label: 'Comentarios' },
                { field: 'fecha_inicio', label: 'Fecha Inicio' },
                { field: 'fecha_fin', label: 'Fecha Fin' },
                { field: 'id', label: 'Id' },
            ],


            _initMonitor: function () {

            }

        });
    });