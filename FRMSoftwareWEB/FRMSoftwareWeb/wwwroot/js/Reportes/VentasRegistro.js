function generarGraficaVentasPorPeriodo(labels, values, tipoTabla) {
    const ctx = document.getElementById('chartVentasPorPeriodo').getContext('2d');

    // Determinar la etiqueta según el tipo de tabla
    const labelText = tipoTabla === 1 ? 'Cajas' : 'Proceso (kg)';

    // Destruir gráfico anterior si ya existe
    if (window.graficaVentas) {
        window.graficaVentas.destroy();
    }

    window.graficaVentas = new Chart(ctx, {
        type: 'line',
        data: {
            labels: JSON.parse(labels),
            datasets: [{
                label: labelText,
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
                        text: labelText
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
                            return tipoTabla === 1
                                ? `${context.parsed.y} cajas`
                                : `${context.parsed.y} kg`;
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

function generarGraficaVentasPorTipoBerry(labels, values, tipoTabla) {
    const ctx = document.getElementById('chartVentasPorTipoBerry').getContext('2d');

    // Determinar el título según el tipo de tabla
    const titleText = tipoTabla === 1 ? 'cajas' : 'kilos';
    const parsedLabels = JSON.parse(labels);
    const parsedValues = JSON.parse(values);

    // Guardar en variables globales para uso en PDF
    berryEtiquetasGlobales = parsedLabels;
    berryValoresGlobales = parsedValues;
    tipoBerryTablaActual = tipoTabla;

    // Calcular el total general
    const totalGeneral = parsedValues.reduce((acc, val) => acc + val, 0);

    // Crear texto de resumen
    let resumenHtml = '';
    for (let i = 0; i < parsedLabels.length; i++) {
        const percentage = Math.round((parsedValues[i] / totalGeneral) * 100);
        resumenHtml += `${parsedLabels[i]}: <strong>${parsedValues[i]} ${tipoTabla === 1 ? 'cajas' : 'kg'}</strong> (${percentage}%)`;
        if (i < parsedLabels.length - 1) resumenHtml += ' | ';
    }
    resumenHtml += ` | <strong>Total: ${totalGeneral} ${tipoTabla === 1 ? 'cajas' : 'kg'}</strong>`;

    // Añadir el resumen al DOM
    const resumenElement = document.getElementById("resumenTipoBerry");
    resumenElement.innerHTML = resumenHtml;


    // Destruir gráfico anterior si ya existe
    if (window.graficaTipoBerry) {
        window.graficaTipoBerry.destroy();
    }

    // Colores para los diferentes tipos de berry
    const backgroundColors = [
        '#FF6384', // Frambuesa (rojo)
        '#36A2EB', // Arándano (azul)
        '#FFCE56', // Zarzamora (amarillo)
        '#4BC0C0', // Fresa (verde azulado)
        '#9966FF', // Otro (púrpura)
        '#FF9F40'  // Otro (naranja)
    ];

    window.graficaTipoBerry = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: parsedLabels,
            datasets: [{
                data: parsedValues,
                backgroundColor: backgroundColors.slice(0, parsedLabels.length),
                borderColor: backgroundColors.map(color => color.replace('0.2', '1')),
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom'
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const label = context.label || '';
                            const value = context.parsed || 0;
                            const percentage = Math.round((value / totalGeneral) * 100);
                            return tipoTabla === 1
                                ? `${label}: ${value} cajas (${percentage}%)`
                                : `${label}: ${value} kg (${percentage}%)`;
                        }
                    }
                },
                datalabels: {
                    display: true,
                    color: '#fff',
                    font: {
                        weight: 'bold'
                    },
                    formatter: (value, ctx) => {
                        const percentage = Math.round((value / totalGeneral) * 100);
                        return percentage > 5 ? `${percentage}%` : '';
                    }
                }
            }
        },
        plugins: [ChartDataLabels]
    });
}

// Variables globales para almacenar los datos de berry
let berryValoresGlobales = [];
let berryEtiquetasGlobales = [];
let tipoBerryTablaActual = 1;

function descargarPDF_VentasPorPeriodo() {
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
        doc.text("Reporte de ventas por periodo", doc.internal.pageSize.width / 2, 35, { align: "center" });

        // Obtener tipo de tabla actual desde el botón activo
        const tipoTabla = document.querySelector('.toggle-btn.active').textContent.trim().includes('cajas') ? 1 : 2;
        const subtitulo = tipoTabla === 1 ? "Venta de cajas" : "Venta de proceso";

        doc.setFontSize(12);
        doc.text(subtitulo, doc.internal.pageSize.width / 2, 45, { align: "center" });

        // Tabla
        const tabla = document.querySelector("table");
        doc.autoTable({
            html: tabla,
            startY: 50,
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
            didDrawPage: function (data) {
                // Ajusta el ancho de la tabla al ancho disponible
                data.table.width = doc.internal.pageSize.getWidth() - 30;
            },
            didParseCell: function (data) {
                if (data.section === 'body') {
                    const texto = data.cell.text?.join?.(' ') || '';

                    if (texto.includes('Total') && (texto.includes('cajas') || texto.includes('kilos'))) {
                        // Fila total
                        data.cell.styles.fillColor = [173, 216, 230]; // Azul clarito
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                    }
                }
            }
        });

        // Gráfico de Ventas por Periodo
        const canvasVentas = document.getElementById("chartVentasPorPeriodo");
        const imgDataVentas = canvasVentas.toDataURL("image/png");

        const canvasWidthVentas = canvasVentas.width;
        const canvasHeightVentas = canvasVentas.height;
        const aspectRatioVentas = canvasHeightVentas / canvasWidthVentas;

        const pageWidth = doc.internal.pageSize.getWidth();
        const pageHeight = doc.internal.pageSize.getHeight();
        const marginX = 15;

        const imgWidthVentas = pageWidth - 2 * marginX;
        const imgHeightVentas = imgWidthVentas * aspectRatioVentas;

        // Añadir nueva página para el gráfico de Ventas
        doc.addPage();

        // Título del gráfico de Ventas
        doc.setFontSize(12);
        doc.setFont("helvetica", "bold");
        doc.text("Gráfica de ventas por periodo", pageWidth / 2, 20, { align: "center" });
        doc.setFontSize(10);
        doc.text(subtitulo, pageWidth / 2, 30, { align: "center" });

        doc.addImage(imgDataVentas, "PNG", marginX, 40, imgWidthVentas, imgHeightVentas);

        // Gráfico de Distribución por Tipo de Berry
        const canvasBerry = document.getElementById("chartVentasPorTipoBerry");
        const imgDataBerry = canvasBerry.toDataURL("image/png");

        const canvasWidthBerry = canvasBerry.width;
        const canvasHeightBerry = canvasBerry.height;
        const aspectRatioBerry = canvasHeightBerry / canvasWidthBerry;

        const imgWidthBerry = pageWidth - 2 * marginX;
        const imgHeightBerry = imgWidthBerry * aspectRatioBerry;

        // Añadir nueva página
        doc.addPage();

        // Título centrado
        doc.setFontSize(12);
        doc.setFont("helvetica", "bold");
        doc.text("Distribución de ventas por tipo de berry", pageWidth / 2, 20, { align: "center" });

        // Subtítulo centrado
        doc.setFontSize(10);
        doc.setFont("helvetica", "normal");
        doc.text(subtitulo, pageWidth / 2, 30, { align: "center" });

        // Generar resumen textual
        const totalGeneral = berryValoresGlobales.reduce((acc, val) => acc + val, 0);
        let resumen = "";
        for (let i = 0; i < berryEtiquetasGlobales.length; i++) {
            const cantidad = berryValoresGlobales[i];
            const porcentaje = Math.round((cantidad / totalGeneral) * 100);
            const unidad = tipoBerryTablaActual === 1 ? 'cajas' : 'kg';
            resumen += `${berryEtiquetasGlobales[i]}: ${cantidad} ${unidad} (${porcentaje}%)   `;
        }

        // Calcular ancho del resumen para centrarlo
        doc.setFontSize(9);
        doc.setFont("helvetica", "normal");
        const resumenWidth = doc.getStringUnitWidth(resumen) * doc.internal.getFontSize() / doc.internal.scaleFactor;
        const resumenX = (pageWidth - resumenWidth) / 2;

        // Mostrar resumen centrado
        doc.text(resumen.trim(), resumenX, 40);

        // Insertar gráfico debajo del resumen
        doc.addImage(imgDataBerry, "PNG", marginX, 50, imgWidthBerry, imgHeightBerry);

        // Guardar PDF
        doc.save("reporte_ventas_por_periodo.pdf");
    } else {
        alert("jsPDF AutoTable no está disponible");
    }
}

function descargarExcel_VentasPorPeriodo() {
    // Obtener tipo de tabla actual
    const tipoTabla = document.querySelector('.toggle-btn.active').textContent.trim().includes('cajas') ? 1 : 2;
    const tipoNombre = tipoTabla === 1 ? "cajas" : "proceso";

    // Crear una hoja de Excel
    var ws = XLSX.utils.table_to_sheet(document.querySelector("table"));
    var wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, `Ventas_${tipoNombre}`);

    // Descargar el archivo Excel
    XLSX.writeFile(wb, `ventas_${tipoNombre}_por_periodo.xlsx`);
}