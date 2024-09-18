
$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        /*console.log(btn.data('id'));*/


        const swal = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger mx-2",
                cancelButton: "btn btn-light"
            },
            buttonsStyling: false
        });

        swal.fire({
            title: "Are you sure, delete this game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `Games/Delete/${btn.data('id')}`,
                    method: 'Delete',
                    success: function () {
                        swal.fire({
                            title: "Deleted!",
                            text: "Your Game has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire({
                            title: "ooops!",
                            text: "somthing wrong.",
                            icon: "error"
                        });
                    }
                }
                );
              
            }
        });

    });
});