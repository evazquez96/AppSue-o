define(["dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/json",
    "dojo/store/Memory",
    "dojo/_base/array",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/layout/ContentPane",
    "dijit/layout/TabContainer",
    "dijit/MenuBar",
    "dijit/MenuBarItem",
    "dijit/layout/BorderContainer",
    "dojox/layout/TableContainer",
    "dijit/form/Select",
    "dijit/form/ValidationTextBox",
    "dijit/form/Button",
    "dijit/form/SimpleTextarea",
    "/web/myApp/Grid/MonitorGrid.js",
    "dijit/Dialog",//Dialog que mostrar los registros que tienen errores.
    "dojo/text!/web/myApp/widget/templates/NuevoSuenoPaneWidget.html",
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
        TabContainer,
        ManuBar,
        MenuBarItem,
        BorderContainer,
        TableContainer,
        Select,
        ValidationTextBox,
        Button,
        SimpleTextArea,
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
        return declare([ContentPane,_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin], {

            templateString: template,
            informacion: null,
            infoOperador:null,
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
                on(this.btnInsertar, "click", lang.hitch(this, function (event) {
                    var fechaInicio = this.fechaInicioWidget.get("displayedValue") + " " + this.fechaInicioTimeWidget.get("displayedValue");
                    var fechaFin = this.fechaFinWidget.get("displayedValue") + " " + this.fechaFinTimeWidget.get("displayedValue");
                    //var comentarios = this.comentariosWidget.get("value");
                    var usuarioId = parseInt(this.infoOperador.usuarioId);
                    var id = parseInt(this.infoOperador.id);
                    var sqlId = 0;
                    var tipoActividad = 1;
                    var inicio = createDate(fechaInicio).getTime();
                    /**Con el getTime() se obtiene el número de milisegundos**/
                    var fin = createDate(fechaFin).getTime();
                    var comentarios = this.comentariosWidget.get("value") == null ? "Sueño manual" : this.comentariosWidget.get("value");
                    debugger;
                    //[Route("GetSuenos/insert/{comentarios}/{fFin}/{fInicio}/{id}/{sqlId}/{tipoActividad}/{usuarioId}")]
                    var url = "http://localhost:63915/GetSuenos/insert/" +
                        comentarios + "/"+
                        fin+"/"+
                        inicio +
                        "/" + this.informacion.id +
                        "/" + sqlId.toString() +
                        "/" + 5 +
                        "/" + usuarioId.toString();
                    
                    var deferred = xhr.get(url,{
                        /*data:{
                            comentarios: "abc",
                            fFin: fechaFin,
                            fInicio: fechaInicio,
                            id: 5,
                            sqlId: 5,
                            tipoActividad: 5,
                            usuarioId:2
                        }*/
                    });
                    when(deferred, function (response) { }, function (err) { });
                    
                    alert("Inicio : " + fechaInicio);
                    alert("Fin : "+ fechaFin);
                }));
            },
            _setValues(object,infoOperador) {
                console.log(object)
                this.operadorWidget.set("displayedValue", object.usuario);
                this.comentariosWidget.set("value", object.comentarios);
                this.informacion = object;
                this.infoOperador = infoOperador;
                var fechaInicio = object.fechaInicio.substr(0, 10);
                var fechaFin = object.fechaFin.substr(0, 10);
                
                var timeInicio = "T".concat(object.fechaInicio.substr(11, 8));
                var timeFin = "T".concat(object.fechaFin.substr(11, 8));
                this.fechaInicioWidget.set("displayedValue", fechaInicio);
                this.fechaFinWidget.set("displayedValue", fechaFin);
                this.fechaInicioTimeWidget.set("value", timeInicio);
                this.fechaFinTimeWidget.set("value", timeFin);
                
            }

        });
        parser.parse();
    });