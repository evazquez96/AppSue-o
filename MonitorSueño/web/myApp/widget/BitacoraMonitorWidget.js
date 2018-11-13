define(["dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/json",
    "dojo/request/iframe",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/layout/ContentPane",
    "dijit/layout/BorderContainer",
    "dijit/layout/TabContainer",
    "dijit/form/TextBox",
    "dijit/form/Button",
    "dijit/form/DateTextBox",
    "dijit/form/TimeTextBox",
    "/web/myApp/Grid/BitacoraGrid.js",
    "/web/myApp/widget/FiltroMonitorWidget.js",
    "dijit/Dialog",//Dialog que mostrar los registros que tienen errores.
    "dojo/text!/web/myApp/widget/templates/BitacoraMonitorWidget.html",
    "dojo/on",
    "dojo/parser",
    "dojo/dom-style",
    "dojo/request/xhr",
    "dojo/when",
    "dojo/Deferred",
    "dstore/Memory",
    "dgrid/OnDemandGrid",
    "dgrid/ColumnSet",
    "dgrid/extensions/DijitRegistry",
    "dojo/domReady!"
],
    function (
        declare,
        lang,
        JSON,
        iframe,
        _WidgetBase,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,
        ContentPane,
        BorderContainer,
        TabContainer,
        TextBox,
        Button,
        DateTextBox,
        TimeTextBox,
        BitacoraGrid,
        Filtro,
        Dialog,
        template,
        on,
        parser,
        domStyle,
        request,
        when,
        Deferred,
        Memory,
        OnDemandGrid,
        ColumnSet,
        DijitRegistry
    ) {
        return declare([ContentPane,_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin], {

            templateString: template,
            title: "Bitacora",
            bitacoraGrid: new BitacoraGrid({

            }),
            postCreate: function () {
                var domNode = this.domNode;
                this.inherited(arguments);
                this._initGridBitacora();
            },
            
            constructor: function (arguments) {
                lang.mixin(this, arguments);
                /**
                La línea anterior permite manipular los objetos que se
                pasan como argumentos en el constructo.
                */
            },
            _initGridBitacora: function () {
                this.gridBitacora.addChild(this.bitacoraGrid);
            }

            
            
        });
        parser.parse();
    });