function generarGraficaViajesCajasEstatus(labels, values) {
    const ctx = document.getElementById('chartViajesPorEstatusCajas').getContext('2d');

    const parsedLabels = JSON.parse(labels);
    const parsedValues = JSON.parse(values);

    const coloresFijos = ['#d4edda', '#cce5ff', '#f8d7da']; // Verde, azul, rojo

    if (window.graficaViajesEstatus) {
        window.graficaViajesEstatus.destroy();
    }

    window.graficaViajesEstatus = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: parsedLabels,
            datasets: [{
                label: 'Cajas',
                data: parsedValues,
                backgroundColor: coloresFijos.slice(0, parsedLabels.length),
                borderColor: coloresFijos.slice(0, parsedLabels.length),
                borderWidth: 1
            }]
        },
        options: {
            indexAxis: 'y',
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
                        text: 'Estatus de viaje'
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
                datalabels: {
                    display: true,
                    color: '#000',
                    align: 'right',
                    formatter: function (value) {
                        return value;
                    }
                }
            }
        },
        plugins: [ChartDataLabels]
    });
}

function descargarPDF_ViajesCajasEstatus() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF({ orientation: "landscape", format: "legal" });

    if (doc.autoTable) {
        const logo = "images/LogoFRM.png";
        doc.addImage(logo, "PNG", 15, 15, 20, 15);

        doc.setFontSize(10);
        doc.setFont("helvetica", "bold");
        doc.text("Frutillos Rojos de México S. de P. R. de R. L.", 35, 20);
        doc.setFontSize(8);
        doc.setFont("helvetica", "normal");
        doc.text("Calle Zapotlán 193, Lomas de San Cayetano, 49040, Ciudad Guzmán, Jalisco, México", 35, 26);

        doc.setFontSize(10);
        doc.setFont("helvetica", "bold");
        const fecha = new Date().toLocaleDateString();
        doc.text(fecha, doc.internal.pageSize.width - 15, 20, { align: "right" });

        doc.setFontSize(16);
        doc.setFont("helvetica", "bold");
        doc.text("Reporte de viajes de cajas por estatus", doc.internal.pageSize.width / 2, 35, { align: "center" });

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

                    if (texto.includes('Estado de aprobación: Aceptado')) {
                        data.cell.styles.fillColor = [212, 237, 218]; // #d4edda - Verde claro
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'left';
                    }

                    if (texto.includes('Estado de aprobación: Pendiente')) {
                        data.cell.styles.fillColor = [204, 229, 255]; // #cce5ff - Azul claro
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'left';
                    }

                    if (texto.includes('Estado de aprobación: Rechazado')) {
                        data.cell.styles.fillColor = [248, 215, 218]; // #f8d7da - Rojo claro
                        data.cell.styles.textColor = [0, 0, 0];
                        data.cell.styles.fontStyle = 'bold';
                        data.cell.styles.halign = 'left';
                    }
                }
            }
        });

        const canvas = document.getElementById("chartViajesPorEstatusCajas");
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

        doc.setFontSize(12);
        doc.setFont("helvetica", "bold");
        doc.text("Gráfica viajes de cajas por estatus", pageWidth / 2, finalY, { align: "center" });
        finalY += 5;

        doc.addImage(imgData, "PNG", marginX, finalY, imgWidth, imgHeight);

        doc.save("reporte_viajes_cajas_por_estatus.pdf");
    } else {
        alert("jsPDF AutoTable no está disponible");
    }
}

function descargarExcel_ViajesCajasEstatus() {
    const ws = XLSX.utils.table_to_sheet(document.querySelector("table"));
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Rep. Viajes Cajas Est.");
    XLSX.writeFile(wb, "reporte_viajes_cajas_por_estatus.xlsx");
}
