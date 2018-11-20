define(["dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/json",
    "dojo/store/Memory",
    "dojo/_base/array",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/layout/ContentPane",
    "dijit/layout/BorderContainer",
    "dojox/layout/TableContainer",
    "dijit/form/Select",
    "dijit/form/ValidationTextBox",
    "dijit/form/Button",
    "/web/myApp/Grid/MonitorGrid.js",
    "dijit/Dialog",//Dialog que mostrar los registros que tienen errores.
    "dojo/text!/web/myApp/widget/templates/FiltroMonitorWidget.html",
    "dojo/on",
    "dojo/parser",
    "dojo/dom-style",
    "dojo/request/xhr",
    "dojo/when",
    "dojo/Deferred",
    "dstore/Memory",
    "dgrid/OnDemandGrid",
    "dgrid/ColumnSet",
    "dgrid/extensions/DijitRegistry"
],
    function (
        declare,
        lang,
        json,
        Memory,
        array,
        _WidgetBase,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,
        ContentPane,
        BorderContainer,
        TableContainer,
        Select,
        ValidationTextBox,
        Button,
        MonitorGrid,
        Dialog,
        template,
        on,
        parser,
        domStyle,
        xhr,
        when,
        Deferred,
        Memory,
        OnDemandGrid,
        ColumnSet,
        DijitRegistry
    ) {
        return declare([_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin], {

            templateString: template,
            
            constructor: function (arguments) {
                lang.mixin(this, arguments);
                /**
                La línea anterior permite manipular los objetos que se
                pasan como argumentos en el constructo.
                */
            },

            postCreate: function () {
                var domNode = this.domNode;
                this.inherited(arguments);
                
                this._initEvents();
            },
            _initEvents: function () {
                /**Agregara los eventos a los widgets de los filstros***/
                on(this.operadorWidget, "keyup", lang.hitch(this, function (event) {
                    var value = this.operadorWidget.displayedValue;
                    var temp = new Memory({
                        idProperty: 'usuarioId'
                    });
                    var s = this.master.monitorGrid;
                    var z = this.master.monitorGrid.collectionOriginal.filter({
                        nombre: new RegExp(value, "i")
                    }).forEach(function (object) {
                        temp.put(object)
                    });
                    this.master.monitorGrid.set("collection",temp)
                }));

                on(this.semaforoSelectWidget, "change", lang.hitch(this, function () {
                    var actividad = this.actividadSelectWidget.get("value") == "null" ? null : this.actividadSelectWidget.get("value");
                    var semaforo = this.semaforoSelectWidget.get("value") == "null" ? null : parseInt(this.semaforoSelectWidget.get("value"));
                    var tipoOperador = this.tipoOperadorWidget.get("value") == "null" ? null : parseInt(this.tipoOperadorWidget.get("value"));
                    var grupo = this.grupoSelectWidget.get("value") == "null" ? null : parseInt(this.grupoSelectWidget.get("value"));
                    this.master.monitorGrid._consultarMonitor(
                        null,
                        grupo,
                        semaforo,
                        actividad,
                        tipoOperador,
                        null);
                    
                }));
                on(this.grupoSelectWidget, "change", lang.hitch(this, function () {
                    var actividad = this.actividadSelectWidget.get("value") == "null" ? null : this.actividadSelectWidget.get("value");
                    var semaforo = this.semaforoSelectWidget.get("value") == "null" ? null : parseInt(this.semaforoSelectWidget.get("value"));
                    var tipoOperador = this.tipoOperadorWidget.get("value") == "null" ? null : parseInt(this.tipoOperadorWidget.get("value"));
                    var grupo = this.grupoSelectWidget.get("value") == "null" ? null : parseInt(this.grupoSelectWidget.get("value"));
                    this.master.monitorGrid._consultarMonitor(
                        null,
                        grupo,
                        semaforo,
                        actividad,
                        tipoOperador,
                        null);
                   
                }))
                on(this.actividadSelectWidget, "change", lang.hitch(this, function () {
                    var actividad = this.actividadSelectWidget.get("value") == "null" ? null : this.actividadSelectWidget.get("value");
                    var semaforo = this.semaforoSelectWidget.get("value") == "null" ? null : parseInt(this.semaforoSelectWidget.get("value"));
                    var tipoOperador = this.tipoOperadorWidget.get("value") == "null" ? null : parseInt(this.tipoOperadorWidget.get("value"));
                    var grupo = this.grupoSelectWidget.get("value") == "null" ? null : parseInt(this.grupoSelectWidget.get("value"));
                    this.master.monitorGrid._consultarMonitor(
                        null,
                        grupo,
                        semaforo,
                        actividad,
                        tipoOperador,
                        null);
                }));
                on(this.tipoOperadorWidget, "change", lang.hitch(this, function () {
                    var actividad = this.actividadSelectWidget.get("value") == "null" ? null : this.actividadSelectWidget.get("value");
                    var semaforo = this.semaforoSelectWidget.get("value") == "null" ? null : parseInt(this.semaforoSelectWidget.get("value"));
                    var tipoOperador = this.tipoOperadorWidget.get("value") == "null" ? null : parseInt(this.tipoOperadorWidget.get("value"));
                    var grupo = this.grupoSelectWidget.get("value") == "null" ? null : parseInt(this.grupoSelectWidget.get("value"));
                    this.master.monitorGrid._consultarMonitor(
                        null,
                        grupo,
                        semaforo,
                        actividad,
                        tipoOperador,
                        null);
                }));
            }

        });
        parser.parse();
    });