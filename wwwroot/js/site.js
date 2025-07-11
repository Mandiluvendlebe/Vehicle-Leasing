async function downloadPDF() {
    const selected = document.getElementById("reportFilter").value;

    let content = document.createElement("div");
    if (selected === "all") {
        content.appendChild(document.getElementById("report-supplier").cloneNode(true));
        content.appendChild(document.getElementById("report-branch").cloneNode(true));
        content.appendChild(document.getElementById("report-client").cloneNode(true));
        content.appendChild(document.getElementById("report-make").cloneNode(true));
    } else {
        content.appendChild(document.getElementById("report-" + selected).cloneNode(true));
    }

    document.body.appendChild(content);
    content.style.position = "absolute";
    content.style.left = "-9999px";

    const canvas = await html2canvas(content);
    const imgData = canvas.toDataURL("image/png");

    const { jsPDF } = window.jspdf;
    const pdf = new jsPDF();

    const imgProps = pdf.getImageProperties(imgData);
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;

    pdf.addImage(imgData, "PNG", 0, 0, pdfWidth, pdfHeight);
    pdf.save("VehicleReport.pdf");

    content.remove();
}

function exportToExcel() {
    const selected = document.getElementById("reportFilter").value;
    let reportHtml = "";

    if (selected === "all") {
        reportHtml += document.getElementById("report-supplier").outerHTML;
        reportHtml += document.getElementById("report-branch").outerHTML;
        reportHtml += document.getElementById("report-client").outerHTML;
        reportHtml += document.getElementById("report-make").outerHTML;
    } else {
        reportHtml = document.getElementById("report-" + selected).outerHTML;
    }

    const blob = new Blob(
        ["<html><head><meta charset='utf-8'></head><body>" + reportHtml + "</body></html>"],
        { type: "application/vnd.ms-excel" }
    );

    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = "VehicleReport.xls";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}


