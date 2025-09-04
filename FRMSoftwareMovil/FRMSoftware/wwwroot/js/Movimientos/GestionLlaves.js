function descargarPDF_GestionLlaves() {
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
        doc.text("Movimiento de gestión de llaves", doc.internal.pageSize.width / 2, 35, { align: "center" });

        const tabla = document.querySelector(".table");

        doc.autoTable({
            html: tabla,
            startY: 45,
            useCss: false,
            styles: {
                font: 'helvetica',
                fontSize: 8,
                textColor: [0, 0, 0],
                cellPadding: 3,
                overflow: 'linebreak',
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
        });

        doc.save("movimiento_gestion_llaves.pdf");
    } else {
        alert("jsPDF AutoTable no está disponible");
    }
}

function descargarExcel_GestionLlaves() {
    const tabla = document.querySelector(".table");
    const ws = XLSX.utils.table_to_sheet(tabla);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "GestionLlaves");
    XLSX.writeFile(wb, "movimiento_gestion_llaves.xlsx");
}
