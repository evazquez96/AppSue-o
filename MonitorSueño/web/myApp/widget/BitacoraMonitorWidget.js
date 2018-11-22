define(["dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/_base/json",
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
        json,
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
            title: "Bitacora",
            infoOperador:null,
            bitacoraGrid: null,
            postCreate: function () {
                var domNode = this.domNode;
                this.inherited(arguments);
                this.bitacoraGrid = new BitacoraGrid({
                    master:this
                })
                this._initGridBitacora();
                this.bitacoraGrid._initEvents();
                on(this.buscarWidget, "click", lang.hitch(this, function (event) {
                    if (this.infoOperador == null)
                        alert("No se a seleccionado un operador");
                    else
                        this._consultarSueno(this.infoOperador);
                }));
            },
            
            constructor: function (arguments) {
                lang.mixin(this, arguments);
                /**
                La línea anterior permite manipular los objetos que se
                pasan como argumentos en el constructo.
                */
                
            },
            onShow: function () {
                this.iniciarFecha();
            },
            _initGridBitacora: function () {
                this.gridBitacora.addChild(this.bitacoraGrid);
            },
            _setOperadorBusqueda: function (object) {
                /**Cambia el valor del TexBox del operador**/
                this.operadorWidget.set("value", object.nombre);
                this.infoOperador = object;
                /**
                 * Una vez que se de doble click al operador que
                 * se quiere consultar, se mandara a consumir el 
                 * servicio con los eventos del operador.
                 * **/
                this._consultarSueno(object);
            },
            iniciarFecha: function () {
                var actual = new Date();
                /**
                 * Obtiene la fecha actual y la fija en el 
                 * widget de la fecha inicio de la búsqueda.
                 * **/
                /**
                 * Fija la fecha actual.
                 * **/
                var b = this.formato(actual);
                this.fechaFinWidget.set("value", b)

                actual.setDate(actual.getDate() - 7);
                /***
                 * Resta 7 días a la fecha actual.
                 * **/
                b = this.formato(actual);
                this.fechaInicioWidget.set("value", b)
                var hora = "T"+actual.getHours() + ":" + actual.getMinutes() + ":" + actual.getSeconds();
                this.fechaInicioTimeWidget.set("value", hora);
                /**
                 * Cambia la hora actual del sistema
                 * **/
                //debugger;
                /**
                 * Fija la fecha fin de la busqueda.
                 * **/
            }
            ,
            formato:function(date) {
                var d = new Date(date)
                month = '' + (d.getMonth() + 1)
                day = '' + d.getDate()
                year = d.getFullYear();

                if(month.length < 2) month = '0' + month;
                if(day.length < 2) day = '0' + day;

                return [year, month, day].join('-');
            },
            _consultarSueno: function (object) {

                var context = this;
                
                var fechaInicio = this.fechaInicioWidget.get("displayedValue") + " " + this.fechaInicioTimeWidget.get("displayedValue");
                var fechaFin = this.fechaFinWidget.get("displayedValue") + " " + this.fechaFinTimeWidget.get("displayedValue");
                var inicio = createDate(fechaInicio).getTime()/1000;
                /**Con el getTime() se obtiene el número de milisegundos**/
                var fin = createDate(fechaFin).getTime()/1000;
                /*
                var inicio =new Date(this.fechaInicioWidget.value).getTime() / 1000;
                var fin = new Date(this.fechaFinWidget.value).getTime() / 1000;
                */
                var url = "http://localhost:63915/api/GetSuenos/" + object.usuarioId + "/" + inicio + "/" + fin;
               
                var deferred = xhr.get(url, {
                    /*data: json.toJson({
                        "usuario_id": object.usuarioId,
                        "fechaFinal": JSON.stringify("/Date(" + inicio + "000-0500)/"),//this.fechaInicioWidget.get("value"),
                        "fechaInicial": "/Date(" + inicio + "000-0500)/"//this.fechaFinWidget.value
                    })*/
                    handleAs:'json'
                });

                when(deferred,
                    lang.hitch(this, function (response) {
                        /**La promesa se cumplio**/
                        var suenosStore = new Memory({
                            data: response,
                            //id: ['NumEmpleado', 'Fecha'].join("#")
                            idProperty: 'auto_increment'
                        });
                        this.bitacoraGrid.set('collection', suenosStore);
                        this.bitacoraGrid.renderRow(response);
                        this.bitacoraGrid.refresh();
                        //alert(response)
                    }),
                    lang.hitch(this, function (error) {
                        /**La promesa fue rechazada.***/
                        alert(error)
                    }));

            }
    
        });
        parser.parse();
    });