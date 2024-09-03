document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('deleteButton').addEventListener('click', function () {
        var selectedRows = Array.from(document.querySelectorAll('#transactionTable tr.selected'));

        var idsToDelete = selectedRows.map(function (row) {
            return parseInt(row.querySelector('td:first-child').textContent, 10);
        });

        if (idsToDelete.length > 0) {
            if (confirm('Are you sure that you want to delete these rows?')) {
                fetch('/Home/DeleteSelected', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    },
                    body: JSON.stringify(idsToDelete)
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            selectedRows.forEach(function (row) {
                                row.remove();
                            });
                        } else {
                            alert('Failed to delete selected rows.');
                        }
                    })
                    .catch(() => {
                        alert('Error while deleting selected rows.');
                    });
            }
        } else {
            alert('No rows selected.');
        }
    });
});