$(document).ready(function () {
    $('#deleteButton').click(function () {
        var selectedRows = $('#transactionTable').find('tr:has(input:checked)');
        var idsToDelete = selectedRows.map(function () {
            return parseInt($(this).find('td:eq(1)').text(), 10);
        }).get();

        if (idsToDelete.length > 0) {
            // Show confirmation popup
            if (confirm('Are you sure that you want to delete these rows?')) {
                $.ajax({
                    url: '/Home/DeleteSelected',
                    type: 'POST',
                    data: JSON.stringify(idsToDelete),
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response.success) {
                            selectedRows.remove();
                        } else {
                            alert('Failed to delete selected rows.');
                        }
                    },
                    error: function () {
                        alert('Error while deleting selected rows.');
                    }
                });
            }
        } else {
            alert('No rows selected.');
        }
    });
});
