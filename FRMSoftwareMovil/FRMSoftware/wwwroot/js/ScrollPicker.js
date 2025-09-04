// Función para inicializar el selector scrollable
window.initializePicker = () => {
    var estadoPicker = new IScroll('#estadoSelector', {
        scrollX: false,
        scrollY: true,
        momentum: true,
        click: true
    });
};
