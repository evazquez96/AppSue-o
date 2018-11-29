define(["dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/on",
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
    "/web/myApp/Grid/MonitorGrid.js",
    "/web/myApp/widget/FiltroMonitorWidget.js",
    "/web/myApp/widget/BitacoraMonitorWidget.js",
    "dijit/Dialog",//Dialog que mostrar los registros que tienen errores.
    "dojo/text!/web/myApp/widget/templates/MonitorSuenoWidget.html",
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
    "dojox/widget/Standby",
    "dojo/domReady!"
],
    function (
        declare,
        lang,
        on,
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
        MonitorGrid,
        Filtro,
        Bitacora,
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
        DijitRegistry,
        Standby
    ) {
        return declare([_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin], {

            templateString: template,
            filtro: null,
            bitacora: null,
            standby:null,
            iniciarCarga: function () {
                this.standby.set("color", "#5A748F");
                this.standby.show();
            },
            terminarCarga: function () {
                this.standby.hide();
            },
            postCreate: function () {
                var domNode = this.domNode;
                this.inherited(arguments);
                this.standby= new Standby({
                    target: 'contenedor',
                    text:'Cargando'
                })
                /***
                 * La propiedad de standby nos permitira 
                 * agregar un circulo que gire indicando 
                 * que se esta cargando una actividad.
                 * target:indica el id del nodo donde queremos
                 * **/
                document.body.appendChild(this.standby.domNode);
                this.bitacora = new Bitacora({
                    master:this
                });
                this.filtro = new Filtro({
                    master:this
                })
                this.monitorGrid=new MonitorGrid({
                    master:this
                })
                this._createCenterPane();
                this._createTopPane();
                this.monitorGrid._initMonitor("");
                this._createBitacoraPane();
                
                //this._getMonitor();

            },

            constructor: function (arguments) {
                lang.mixin(this, arguments);
                /**
                La línea anterior permite manipular los objetos que se
                pasan como argumentos en el constructo.
                */
            },
            _createTopPane: function () {
                console.log("Creando top pane")
                this.topPaneWidget.addChild(this.filtro)

            },
            _createCenterPane: function () {
                console.log("Creando el panel central")
                this.centerPaneWidget.addChild(this.monitorGrid);
            },

            _createBitacoraPane: function () {
                this.tabContainerWidget.addChild(this.bitacora);

            }

        });
        parser.parse();
    });