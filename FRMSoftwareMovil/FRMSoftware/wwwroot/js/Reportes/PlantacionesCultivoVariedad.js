const colores = [
    '#A8DADC', // turquesa claro
    '#F4A261', // naranja suave
    '#E5989B', // rosa pastel
    '#B5E48C', // verde lima claro
    '#FFB4A2', // salmón claro
    '#BDE0FE', // azul cielo
    '#CDB4DB', // lila pastel
    '#FFD6A5', // durazno claro
    '#A2D2FF', // azul pastel
    '#D9ED92', // verde limón pastel
    '#FFC8DD', // rosa chicle
    '#C6DEF1'  // azul hielo
];

function generarGraficaPlantacionesPorCultivoVariedad(idCanvas, labels, values, colorIndex) {
    const ctx = document.getElementById(idCanvas)?.getContext('2d');
    if (!ctx) return;

    if (window[`grafica_${idCanvas}`]) {
        window[`grafica_${idCanvas}`].destroy();
    }

    const color = colores[colorIndex % colores.length];

    window[`grafica_${idCanvas}`] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: JSON.parse(labels),
            datasets: [{
                label: 'Cantidad de plantas',
                data: JSON.parse(values),
                backgroundColor: color,
                borderColor: color,
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Variedad'
                    }
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Cantidad de plantas'
                    }
                }
            },
            plugins: {
                legend: { display: false },
                tooltip: {
                    callbacks: {
                        label: context => `${context.parsed.y} plantas`
                    }
                },
                datalabels: {
                    display: true,
                    color: '#000',
                    align: 'top',
                    formatter: value => value
                }
            }
        },
        plugins: [ChartDataLabels]
    });
}

function descargarPDF_PlantacionesPorCultivoVariedad() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF({ orientation: "landscape" });

    if (doc.autoTable) {
        // Insertar el Logo
        const logo = "images/LogoFRM.png";
        doc.addImage(logo, "PNG", 15, 15, 20, 15); // Posición y tamaño del logo

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
        doc.text("Reporte de plantaciones por cultivo y variedad", doc.internal.pageSize.width / 2, 35, { align: "center" });

        // Insertar tabla
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

                    if (texto.includes('Cultivo:')) {
                        // Solo las filas de encabezado de grupo
                        data.cell.styles.fillColor = [212, 237, 218]; // Verde claro
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'left';
                    }
                }
            }
        });

        let lastY = doc.lastAutoTable.finalY;
        const marginX = 15;
        const pageHeight = doc.internal.pageSize.getHeight();
        const pageWidth = doc.internal.pageSize.getWidth();

        // Insertar todas las gráficas que existen (por cada canvas de cultivo)
        const canvases = document.querySelectorAll("canvas[id^='chartCultivo_']");

        canvases.forEach((canvas, index) => {
            const imgData = canvas.toDataURL("image/png");

            // Obtener el tamaño original del gráfico para calcular la relación de aspecto
            const canvasWidth = canvas.width;
            const canvasHeight = canvas.height;
            const aspectRatio = canvasHeight / canvasWidth;  // Calculamos la relación de aspecto


            // Verificar si cabe en la página actual, si no crear nueva
            if (lastY + 90 > pageHeight - 20) {
                doc.addPage();
                lastY = 20;
            }

            doc.setFontSize(12);
            doc.setFont("helvetica", "bold");
            const nombreCultivo = canvas.dataset.nombre;
            doc.text("Gráfica de variedades - " + nombreCultivo, pageWidth / 2, lastY + 10, { align: "center" });

            // Aquí aseguramos que usamos los colores asignados al canvas
            const chart = Chart.getChart(canvas.id); // Obtener el gráfico de Chart.js
            const colores = chart.data.datasets[0].backgroundColor; // Obtener los colores de las barras del gráfico

            // Usamos todo el ancho disponible, y la altura se ajustará proporcionalmente
            const imgWidth = pageWidth - 2 * marginX;
            const imgHeight = imgWidth * aspectRatio; // Ajustamos la altura proporcionalmente

            // Agregar la imagen al PDF asegurando la proporción
            doc.addImage(imgData, "PNG", marginX, lastY + 15, imgWidth, imgHeight);

            lastY += imgHeight + 15; // Actualizamos la posición Y
        });

        // Guardar el PDF
        doc.save("reporte_plantaciones_por_cultivo.pdf");
    } else {
        alert("jsPDF AutoTable no está disponible");
    }
}

function descargarExcel_PlantacionesPorCultivoVariedad() {
    var ws = XLSX.utils.table_to_sheet(document.querySelector("table"));
    var wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Plant. Cult. Var.");
    XLSX.writeFile(wb, "reporte_plantaciones_por_cultivo.xlsx");
}
