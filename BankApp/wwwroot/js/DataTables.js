$.fn.dataTable.Buttons.defaults.dom.button.className = 'btn btn-primary';

$(document).ready(function () {
    let table = new DataTable('#transactionTable', {
        lengthMenu: [10, 25, 50, 100, { label: 'Show All', value: -1}],
        layout: {
            topStart: {
                buttons: [
                    'pageLength',
                    {
                        extend: 'copy',
                        title: null
                    },
                    {
                        extend: 'excel',
                        title: null
                    },
                    {
                        extend: 'csv',
                        title: null
                    }
                ]
            },
            topEnd: {
                search: {
                    placeholder: 'Search'
                }
            }
        },
        select: true,
        responsive: true
    });

    let selectedIds = [];

    table.on('select', function (e, dt, type, indexes) {
        if (type === 'row') {
            indexes.forEach(index => {
                const rowData = table.row(index).data();
                const rowId = rowData[0];
                if (!selectedIds.includes(rowId)) {
                    selectedIds.push(rowId);
                }
            });
        }
    }).on('deselect', function (e, dt, type, indexes) {
        if (type === 'row') {
            indexes.forEach(index => {
                const rowData = table.row(index).data();
                const rowId = rowData[0];
                selectedIds = selectedIds.filter(id => id !== rowId);
            });
        }
    });

    $('#deleteButton').on('click', function () {
        if (selectedIds.length > 0) {
            if (confirm('Are you sure that you want to delete these rows?')) {
                fetch('/Home/DeleteSelected', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    },
                    body: JSON.stringify(selectedIds)
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error(`HTTP error! status: ${response.status}`);
                        }
                        return response.json();
                    })
                    .then(data => {
                        if (data.success) {
                            table.rows({ selected: true }).remove().draw();
                            selectedIds = [];
                        } else {
                            alert('Failed to delete selected rows.');
                        }
                    })
                    .catch(error => {
                        console.error('There was a problem with the fetch operation:', error);
                        alert('Error while deleting selected rows.');
                    });
            }
        } else {
            alert('No rows selected.');
        }
    });
});
