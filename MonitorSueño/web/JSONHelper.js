function darFormato(item) {
    return {
        Id: item.Id,
        nombre: item.nombre,
        actividad: item.monitor.Actividad_Actual_Id,
        tiempoActual: item.monitor.Tiempo_Actual,
        S24: item.monitor.Tiempo_Sueno,
        plan: item.monitor.Tiempo_Plan,
        A24: item.monitor.Tiempo_Activo,
        I24: item.monitor.Tiempo_Inactivo,
        D24: item.monitor.Tiempo_Descanso,
        rojo: item.monitor.Porcentaje_Rojo,
        amarillo: item.monitor.Porcentaje_Amarillo,
        verde: item.monitor.Porcentaje_Verde

    }

}