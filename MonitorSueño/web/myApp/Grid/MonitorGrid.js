define([
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/on",
    "dojo/_base/array",
    "dojo/when",
    "dojo/request/xhr",
    "dojo/json",
    "dojo/_base/json",
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
    on,
    array,
    when,
    request,
    J,
    json,
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
    MonitorHelper,
) {
        return declare([OnDemandGrid, Dgrid,DijitRegistry, Selection, Editor, Keyboard], {

            //collection: null,//Al inicio la collection sera null.
            //farOffRemoval: 500,
            collectionOriginal: null,
            columns: [
                { field: 'nombre', label: 'Nombre' },
                MonitorHelper.formatoActividadColumn({ field: 'Actividad', label: 'Actividad' }),
                { field: 'TiempoActual', label: 'TiempoActual' },
                MonitorHelper.formato24SPlan({ field: 'tiempo_24s', label: '24s' }),
                MonitorHelper.formatoPlan({ field: 'plan', label: 'Plan' }),
                { field: 'tiempo_24a', label: '24a' },
                { field: 'tiempo_24i', label: '24I' },
                { field: 'tiempo_24d', label: '24D' },
                MonitorHelper.formatoRojoColumn({ field: 'rojo', label: '%ROJO' }),
                MonitorHelper.formatoAmarilloColumn({ field: 'amarillo', label: '%AMARILLO' }),
                MonitorHelper.formatoVerdeColumn({ field: 'verde', label: '%VERDE' }),
            ],

           
            _initMonitor: function (nombre) {
                this._initEvents()
                
                console.log("Iniciando Monitor")
                var deferred = request.post("http://app.mexamerik.com/Dream/Sueno/Monitor.svc/consultar", {
                    data:json.toJson({
                        "Nombre": null,
                        "grupo_id": null,
                        "semaforo_id": null,
                        "tipo_actividad_id": null,
                        "tipo_operador_id": null,
                        "usuario_id": null
                    }),
                    handleAs: 'json',
                    headers: {
                        "Content-Type": 'application/json'
                    }
                });
                //debugger;
                when(deferred,
                    lang.hitch(this, function (response) {

                        var monitorStore = new Memory({
                            data: response,
                            idProperty: 'usuarioId'
                        });
                        this.set('collection', monitorStore);
                        this.renderArray(json);
                        this.collectionOriginal = monitorStore;
                        this.refresh();

                    }), function (error) {
                        alert(error);
                    });

            },
            _consultarMonitor: function (nombre,grupo,semaforo,tipoActividad,tipoOperador,usuarioId) {

                var deferred = request.post("http://app.mexamerik.com/Dream/Sueno/Monitor.svc/consultar", {
                    data: json.toJson({
                        "Nombre": nombre,
                        "grupo_id": grupo,
                        "semaforo_id": semaforo,
                        "tipo_actividad_id": tipoActividad,
                        "tipo_operador_id": tipoOperador,
                        "usuario_id": usuarioId
                    }),
                    handleAs: 'json',
                    headers: {
                        "Content-Type": 'application/json'
                    }
                });
                //debugger;
                when(deferred,
                    lang.hitch(this, function (response) {

                        var monitorStore = new Memory({
                            data: response,
                            idProperty: 'usuarioId'
                        });
                        this.set('collection', monitorStore);
                        this.renderArray(json);
                        this.collectionOriginal = monitorStore;
                        this.refresh();

                    }), function (error) {
                        alert(error);
                    });

            },
            _initEvents() {

                this.on(".dgrid-content .dgrid-row:dblclick", lang.hitch(this,function (event) {
                    /**Este evento se ejecutara cuando se le doblo click sobre una fila del gri
                     * el cual sera el encargado de mostrar el panel de bitacora con el registro
                     * seleccionado.
                     * ***/
                    var row = this.row(event);
                    /*var aux = new Dialog({
                        title: "My dialog",
                        content: new NuevoSueno()
                    });*/
                    //this.myDialog.show();
                    this.master.bitacora._setOperadorBusqueda(row.data);
                    
                    /**
                     * Se pasa el objeto seleccionado al filtro de bitacora.
                     * **/
                    this.master.tabContainerWidget.selectChild(this.master.bitacora);

                }));

            }
      
        });
    });  