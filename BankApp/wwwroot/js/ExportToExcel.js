$(document).ready(function () {
    $('#exportButton').click(function () {

        var selectedRows = $('#transactionTable').find('tr:has(input:checked');
        var rowsToExport;

        if (selectedRows.length === 0) {
            rowsToExport = $('#transactionTable tbody tr');
        } else {
            rowsToExport = selectedRows;
        }

        var exportTable = document.createElement('table');
        exportTable.innerHTML = `
            <thead>
                <tr>
                    <th>RB</th>
                    <th>Datum prijema/datum izvrsenja</th>
                    <th>Broj naloga</th>
                    <th>Primalac</th>
                    <th>Sifra placanja/svrha</th>
                    <th>Duguje</th>
                    <th>Ime File-a</th>
                </tr>
            </thead>
        `;

        var tbody = document.createElement('tbody');
        rowsToExport.each(function () {
            var clonedRow = $(this).clone();
            clonedRow.find('td:first').remove();
            tbody.appendChild(clonedRow[0]);
        });

        exportTable.appendChild(tbody);

        var wb = XLSX.utils.table_to_book(exportTable, { sheet: "Selected Data" });
        XLSX.writeFile(wb, 'transactions.xlsx');
    });
});