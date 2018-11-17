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
            }

        });
        parser.parse();
    });