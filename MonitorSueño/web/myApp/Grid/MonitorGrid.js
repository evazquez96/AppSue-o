define([
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/on",
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
    on,
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
        return declare([OnDemandGrid, Dgrid,DijitRegistry, Selection, Editor, Keyboard], {

            //collection: null,//Al inicio la collection sera null.
            //farOffRemoval: 500,
            columns: [
                { field: 'nombre', label: 'Nombre' },
                MonitorHelper.formatoActividadColumn({ field: 'actividad', label: 'Actividad' }),
                { field: 'tiempoActual', label: 'TiempoActual' },
                MonitorHelper.formato24SPlan({ field: 'S24', label: '24s' }),
                { field: 'plan', label: 'Plan' },
                { field: 'A24', label: '24a' },
                { field: 'I24', label: '24I' },
                { field: 'D24', label: '24D' },
                MonitorHelper.formatoRojoColumn({ field: 'rojo', label: '%ROJO' }),
                MonitorHelper.formatoAmarilloColumn({ field: 'amarillo', label: '%AMARILLO' }),
                MonitorHelper.formatoVerdeColumn({ field: 'verde', label: '%VERDE' }),
            ],

           
            _initMonitor: function (nombre) {

                var deferred = request.post("http://tms.logsys.com.mx/bitacoradream/Service.asmx/GetMonitor", {
                    data: {
                        "a1": "0",
                        "a2": "10017000",
                        "b1": "0",
                        "b2": "1000",
                        "c1": "0",
                        "c2": "1000",
                        "d1": "0",
                        "d2": "1000",
                        "e1": "0",
                        "e2": "1000",
                        "nombre": nombre
                    },
                    handleAs: 'text',
                    headers: {
                        "Accept": 'application/json'
                    }
                });
                //debugger;
                when(deferred,
                    lang.hitch(this, function (response) {

                        aux = response.replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "")
                        aux = aux.replace('<string xmlns="http://app.mexamerik.com">', "");
                        aux = aux.replace('</string>', "");
                        /**
                         * Lo anterior limpia la cadena ya que por alguna razón no toma la respuesta como
                         * archivo JSON
                         * **/
                        var json = JSON.parse(aux);
                        /**Una vez limpia la cadena, la convertimos a JSON**/
                        //console.log(json)
                        var aux = []

                        array.forEach(json, function (item) {
                           
                            aux.push(darFormato(item));
                            //console.log(item)
                        });
                        jstring = JSON.stringify(aux);

                        var monitorStore = new Memory({
                            data: JSON.parse(jstring),
                            //id: ['NumEmpleado', 'Fecha'].join("#")
                            idProperty: 'Id'
                        });
                        this.set('collection', monitorStore);
                        this.renderArray(json);
                        this.refresh();

                    }), function (error) {
                        alert(error);
                    })

            },
            _initEvents() {

            }
      
        });
    });