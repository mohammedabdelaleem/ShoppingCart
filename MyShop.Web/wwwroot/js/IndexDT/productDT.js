var dataTable;

$(document).ready(function () {
    LoadData();
});

function LoadData() {
    dataTable = $("#my-tbl").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAllDataJson",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data" // Extracts array from the "data" key
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" }, // Correctly references Category.Name
            {
                "data": "id", // Actions column
                "render": function (data) {
                    return `
                        <a href="/Admin/Product/Edit/${data}" class="btn btn-warning btn-sm">Edit</a>
                        <a href="/Admin/Product/Delete/${data}" class="btn btn-danger btn-sm">Delete</a>
                    `;
                },
                "orderable": false
            }
        ]
    });
}
