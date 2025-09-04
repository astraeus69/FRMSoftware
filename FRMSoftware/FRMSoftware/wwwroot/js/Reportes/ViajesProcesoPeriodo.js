function generarGraficaViajesProcesoPeriodo(labels, values) {
    const ctx = document.getElementById('chartProcesoPorPeriodo').getContext('2d');

    // Destruir gráfico anterior si ya existe
    if (window.graficaViajes) {
        window.graficaViajes.destroy();
    }

    window.graficaViajes = new Chart(ctx, {
        type: 'line',
        data: {
            labels: JSON.parse(labels),
            datasets: [{
                label: 'Proceso',
                data: JSON.parse(values),
                backgroundColor: 'rgba(83, 160, 72, 0.2)',
                borderColor: '#53A048',
                borderWidth: 2,
                fill: true,
                tension: 0.2, // suaviza la línea
                pointBackgroundColor: '#53A048',
                pointBorderColor: '#000',
                pointRadius: 4
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Proceso'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Fecha'
                    }
                }
            },
            plugins: {
                legend: {
                    display: true
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return `${context.parsed.y} kg`;
                        }
                    }
                },
                datalabels: {
                    display: true,
                    color: '#000',
                    align: 'top',
                    formatter: function (value) {
                        return value;
                    }
                }
            }
        },
        plugins: [ChartDataLabels]
    });
}

// Variables globales para los viajes aceptados y rechazados
let aceptadosGlobalesProceso = 0;
let rechazadosGlobalesProceso = 0;

function generarGraficaEstadoViajesProceso(aceptados, rechazados) {
    // Actualizar las variables globales con los valores actuales
    aceptadosGlobalesProceso = aceptados;
    rechazadosGlobalesProceso = rechazados;

    const canvas = document.getElementById("chartEstadoViajesProceso");

    // Definir tamaño del canvas antes de generar el gráfico
    canvas.width = 600;
    canvas.height = 600;

    const ctx = canvas.getContext("2d");

    // Mostrar resumen textual
    const total = aceptados + rechazados;
    const resumen = `Aceptados: ${aceptados} viajes   |   Rechazados: ${rechazados} viajes   |   Total: ${total} viajes`;

    // Añadir el resumen al DOM y aplicar margen superior para crear espacio
    const resumenElement = document.getElementById("resumenEstadoViajesProceso");
    resumenElement.innerHTML = resumen;
    resumenElement.style.marginBottom = "10px"; // Ajusta el margen según necesites

    // Destruir gráfico anterior si ya existe
    if (window.graficaEstadoViajes) {
        window.graficaEstadoViajes.destroy();
    }

    window.graficaEstadoViajes = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Aceptados', 'Rechazados'],
            datasets: [{
                data: [aceptados, rechazados],
                backgroundColor: ['#53A048', '#FF0000'],
                borderColor: ['#1e7e34', '#c82333'],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false, // Para que use el tamaño definido arriba
            layout: {
                padding: {
                    top: 30 // Aumenta este valor para agregar más espacio en el top del gráfico
                }
            },
            plugins: {
                legend: {
                    position: 'bottom'
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return `${context.label}: ${context.parsed} viajes`;
                        }
                    }
                }
            }
        }
    });
}

function descargarPDF_ViajesProcesoPorPeriodo() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF({ orientation: "landscape", format: "legal" });

    if (doc.autoTable) {
        // Insertar el Logo
        const logo = "images/LogoFRM.png";
        doc.addImage(logo, "PNG", 15, 15, 20, 15);

        // Información de la empresa
        doc.setFontSize(10);
        doc.setFont("helvetica", "bold");
        doc.text("Frutillos Rojos de México S. de P. R. de R. L.", 35, 20);
        doc.setFontSize(8);
        doc.setFont("helvetica", "normal");
        doc.text("Calle Zapotlán 193, Lomas de San Cayetano, 49040, Ciudad Guzmán, Jalisco, México", 35, 26);

        // Fecha
        doc.setFontSize(10);
        doc.setFont("helvetica", "bold");
        const fecha = new Date().toLocaleDateString();
        doc.text(fecha, doc.internal.pageSize.width - 15, 20, { align: "right" });

        // Título del reporte
        doc.setFontSize(16);
        doc.setFont("helvetica", "bold");
        doc.text("Reporte de viajes de proceso por periodo", doc.internal.pageSize.width / 2, 35, { align: "center" });

        // Tabla
        const tabla = document.querySelector("table");
        doc.autoTable({
            html: tabla,
            startY: 40,
            useCss: false,
            styles: {
                font: 'helvetica',
                fontSize: 6,
                textColor: [0, 0, 0],
                cellPadding: 3,
                overflow: 'linebreak',
                wordBreak: 'normal',
                halign: 'left',
                valign: 'middle',
                lineWidth: 0.1,
                lineColor: [200, 200, 200],
            },
            headStyles: {
                fillColor: [0, 0, 0],
                textColor: [255, 255, 255],
                fontStyle: 'bold',
            },
            alternateRowStyles: {
                fillColor: [245, 245, 245],
            },
            tableWidth: 'auto',
            didParseCell: function (data) {
                if (data.section === 'body') {
                    const texto = data.cell.text?.join?.(' ') || '';

                    if (texto.includes('Total general de proceso del periodo:')) {
                        // Fila total general
                        data.cell.styles.fillColor = [204, 229, 255]; // Azul clarito
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'right';
                    }
                }
            }
        });

        // Gráfico de Cajas por Periodo
        const canvasCajas = document.getElementById("chartProcesoPorPeriodo");
        const imgDataCajas = canvasCajas.toDataURL("image/png");

        const canvasWidthCajas = canvasCajas.width;
        const canvasHeightCajas = canvasCajas.height;
        const aspectRatioCajas = canvasHeightCajas / canvasWidthCajas;

        const pageWidth = doc.internal.pageSize.getWidth();
        const pageHeight = doc.internal.pageSize.getHeight();
        const marginX = 15;

        const imgWidthCajas = pageWidth - 2 * marginX;
        const imgHeightCajas = imgWidthCajas * aspectRatioCajas;

        // Añadir nueva página para el gráfico de Cajas
        doc.addPage();

        // Título del gráfico de Cajas
        doc.setFontSize(12);
        doc.setFont("helvetica", "bold");
        doc.text("Gráfica de proceso por periodo", pageWidth / 2, 20, { align: "center" });

        doc.addImage(imgDataCajas, "PNG", marginX, 30, imgWidthCajas, imgHeightCajas);

        // Mostrar resumen textual para el gráfico de Estado de Viajes
        const total = aceptadosGlobalesProceso + rechazadosGlobalesProceso;
        const resumen = `Aceptados: ${aceptadosGlobalesProceso} viajes   |   Rechazados: ${rechazadosGlobalesProceso} viajes   |   Total: ${total} viajes`;

        // Añadir nueva página para el gráfico de Estado de Viajes
        doc.addPage();

        // Título del gráfico de Estado de Viajes
        doc.setFontSize(12);
        doc.setFont("helvetica", "bold");
        doc.text("Gráfica de viajes de proceso aceptados vs rechazados", pageWidth / 2, 20, { align: "center" });

        // Calcular el ancho del texto para centrarlo
        doc.setFontSize(10);
        doc.setFont("helvetica", "normal");
        const resumenWidth = doc.getStringUnitWidth(resumen) * doc.internal.getFontSize() / doc.internal.scaleFactor;
        const centerX = (pageWidth - resumenWidth) / 2;

        // Mostrar resumen centrado
        doc.text(resumen, centerX, 30);  // Aumenta la posición Y para dar más espacio

        // Gráfico de Viajes Aceptados vs Rechazados
        const canvasViajes = document.getElementById("chartEstadoViajesProceso");
        const imgDataViajes = canvasViajes.toDataURL("image/png", 3);

        const canvasWidthViajes = canvasViajes.width;
        const canvasHeightViajes = canvasViajes.height;
        const aspectRatioViajes = canvasHeightViajes / canvasWidthViajes;

        const imgWidthViajes = pageWidth - 2 * marginX;
        const imgHeightViajes = imgWidthViajes * aspectRatioViajes;

        doc.addImage(imgDataViajes, "PNG", marginX, 40, imgWidthViajes, imgHeightViajes); // Aumenta la posición Y

        // Guardar PDF
        doc.save("reporte_viajes_proceso_por_periodo.pdf");
    } else {
        alert("jsPDF AutoTable no está disponible");
    }
}



function descargarExcel_ViajesProcesoPorPeriodo() {
    // Crear una hoja de Excel
    var ws = XLSX.utils.table_to_sheet(document.querySelector("table"));
    var wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Rep. Viajes Proc. Per.");

    // Descargar el archivo Excel
    XLSX.writeFile(wb, "viajes_proceso_por_periodo.xlsx");
}
