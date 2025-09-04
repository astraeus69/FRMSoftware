function generarGraficaProduccionLlaves(labels, values) {
    const ctx = document.getElementById('chartCajasPorLlave').getContext('2d');

    // Destruir gráfico anterior si ya existe
    if (window.graficaLlaves) {
        window.graficaLlaves.destroy();
    }

    window.graficaLlaves = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: JSON.parse(labels),
            datasets: [{
                label: 'Cajas',
                data: JSON.parse(values),
                backgroundColor: '#53A048',
                borderColor: '#53A048',
                borderWidth: 1
            }]
        },
        options: {
            indexAxis: 'y', // BARRAS HORIZONTALES
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Cajas'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Llaves'
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return `${context.parsed.x} cajas`;
                        }
                    }
                },
                // Activar los DataLabels para mostrar los valores sobre las barras
                datalabels: {
                    display: true, // Muestra los números
                    color: '#000', // Color del texto
                    align: 'right', // Alineación de los valores
                    formatter: function (value) {
                        return value; // Muestra el valor numérico tal cual
                    }
                }
            }
        },
        plugins: [ChartDataLabels] // Asegúrate de usar este plugin
    });
}


function descargarPDF_ProduccionLlaves() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF({ orientation: "landscape" });

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
        doc.text("Reporte de producción por llave", doc.internal.pageSize.width / 2, 35, { align: "center" });

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

                    if (texto.includes('Llave:')) {
                        // Solo las filas de encabezado de grupo
                        data.cell.styles.fillColor = [212, 237, 218]; // Verde claro
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'left';
                    }

                    if (texto.includes('Total general de cajas producidas:')) {
                        // Fila total general
                        data.cell.styles.fillColor = [204, 229, 255]; // Azul clarito
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'right';
                    }
                }
            }
        });

        // Gráfico
        const canvas = document.getElementById("chartCajasPorLlave");
        const imgData = canvas.toDataURL("image/png");

        const canvasWidth = canvas.width;
        const canvasHeight = canvas.height;
        const aspectRatio = canvasHeight / canvasWidth;

        const pageWidth = doc.internal.pageSize.getWidth();
        const pageHeight = doc.internal.pageSize.getHeight();
        const lastY = doc.lastAutoTable.finalY;
        const marginX = 15;

        const imgWidth = pageWidth - 2 * marginX;
        const imgHeight = imgWidth * aspectRatio;

        let finalY = lastY + 10;
        if (finalY + imgHeight > pageHeight - 10) {
            doc.addPage();
            finalY = 20;
        }

        // Título del gráfico
        doc.setFontSize(12);
        doc.setFont("helvetica", "bold");
        doc.text("Gráfica de producción de cajas por llave", pageWidth / 2, finalY, { align: "center" });
        finalY += 5; // Espacio entre el título y la imagen

        doc.addImage(imgData, "PNG", marginX, finalY, imgWidth, imgHeight);

        // Guardar PDF
        doc.save("reporte_produccion_por_llave.pdf");
    } else {
        alert("jsPDF AutoTable no está disponible");
    }
}



function descargarExcel_ProduccionLlaves() {
    // Crear una hoja de Excel
    var ws = XLSX.utils.table_to_sheet(document.querySelector("table"));
    var wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Rep. Prod. Llave");

    // Descargar el archivo Excel
    XLSX.writeFile(wb, "replante_produccion_por_llave.xlsx");
}
