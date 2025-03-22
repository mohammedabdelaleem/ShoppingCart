var dataTable;

$(document).ready(function () {
    LoadData();
});

function LoadData() {
    dataTable = $("#my-tbl").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAllDataJson",
            "type": "GET",
            "dataType": "json",
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
                        <a onClick=DeleteItem("/Admin/Product/DeleteProduct/${data}") class="btn btn-danger btn-sm">Delete</a>
                    `;
                },
                "orderable": false
            }
        ]
    });
}




// for Delete Confirmitation Toaster

function DeleteItem(url) {

    //////////// from SweetAlert ////////////

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            //////////// After Confirm ////////////
            $.ajax({
                url: url,
                type:"Delete",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);

                    }
                }
            })

            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });

}





