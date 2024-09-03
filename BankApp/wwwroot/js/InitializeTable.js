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
});
