// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var url = window.location.pathname;
if (url.toLowerCase() !== '/admin') {
    var substr = url.split('/');
    var urlaspx = '/' + substr[1] + '/' + substr[2];
    //console.log(url);
    //console.log(urlaspx);
    $('.menu').find('.active').removeClass('active');
    $('.menu .menu-item a').each(function () {
        //console.log(this.href);
        if (this.href.indexOf(url) >= 0 && $(this).attr("href") !== "#") {
            $(this).parent().addClass('active');
            $(this).parents('.menu-item.has-sub').addClass('active');
            //if ($(this).parents('.menu-item').hasClass('sub-menu')) {
            //    //has-sub
            //}
        }
    });
}
function display_kendoui_grid_error(e) {
    if (e.errors) {
        if ((typeof e.errors) == 'string') {
            //single error
            //display the message
            alert(e.errors);
        } else {
            //array of errors
            //source: http://docs.kendoui.com/getting-started/using-kendo-with/aspnet-mvc/helpers/grid/faq#how-do-i-display-model-state-errors?
            var message = "The following errors have occurred:";
            //create a message containing all errors.
            $.each(e.errors, function (key, value) {
                if (value.errors) {
                    message += "\n";
                    message += value.errors.join("\n");
                }
            });
            //display the message
            alert(message);
        }
    } else if (e.errorThrown) {
        alert('Error happened');
    }
}

// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};

function Delete(id) {
    Swal.fire({
        html: 'برای حذف مطمعن هستید؟',
        text: "امکان برگشت وجود ندارد",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'حذف',
        cancelButtonText: 'لغو'
    }).then((result) => {
        if (result.value) {
            DeleteConfirmed(id);
        }
    });
}

function DeleteConfirmed(id) {
    Swal.showLoading();
    var postData = {
        id: id
    };

    $.ajax({
        url: 'Delete',
        type: "POST",
        data: addAntiForgeryToken(postData),
        success: function (data) {
            //console.log("Success");
            //console.log(data);
            if (data.isError) {
                Swal.fire({
                    type: 'error',
                    title: 'بروز خطا',
                    html: data.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            } else {
                var tr = $('[data-id="' + id + '"]').parents('tr');
                dataTableButtons.row(tr).remove().draw();
                Swal.fire({
                    type: 'success',
                    title: 'انجام شد',
                    html: data.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        },
        error: function (error) {
            //console.log("error");
            //console.log(error);
            Swal.fire({
                type: 'error',
                title: 'بروز خطا',
                html: error.message,
                showConfirmButton: false,
                timer: 1500
            });
        }
    });
}

function DeleteFullUrl(controller, action, id) {
    console.log(controller);
    console.log(action);
    console.log(id);
    console.log('/' + controller + '/' + action);
    Swal.fire({
        html: 'برای حذف مطمعن هستید؟',
        text: "امکان برگشت وجود ندارد",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'حذف',
        cancelButtonText: 'لغو'
    }).then((result) => {
        if (result.value) {
            DeleteConfirmedFullUrl('/' + controller + '/' + action, id);
        }
    });
}

function DeleteConfirmedFullUrl(url, id) {
    Swal.showLoading();
    var postData = {
        id: id
    };

    $.ajax({
        url: url,
        type: "POST",
        data: addAntiForgeryToken(postData),
        success: function (data) {
            //console.log("Success");
            //console.log(data);
            if (data.isError) {
                Swal.fire({
                    type: 'error',
                    title: 'بروز خطا',
                    html: data.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            } else {
                var tr = $('[data-id="' + id + '"]').parents('tr');
                dataTableButtons.row(tr).remove().draw();
                Swal.fire({
                    type: 'success',
                    title: 'انجام شد',
                    html: data.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        },
        error: function (error) {
            //console.log("error");
            //console.log(error);
            Swal.fire({
                type: 'error',
                title: 'بروز خطا',
                html: error.message,
                showConfirmButton: false,
                timer: 1500
            });
        }
    });
}

function Published(id, published) {
    Swal.fire({
        html: published ? 'برای غیر فعال کردن مطمعن هستید؟' : 'برای فعال کردن مطمعن هستید؟',
        text: "امکان برگشت وجود ندارد",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'تایید',
        cancelButtonText: 'لغو'
    }).then((result) => {
        if (result.value) {
            PublishedConfirmed(id, published);
        }
    });
}

function PublishedConfirmed(id, published) {
    var postData = {
        id: id
    };

    Swal.showLoading();
    var token = $("[name='__RequestVerificationToken']").val();
    $.ajax({
        url: 'Published',
        type: "POST",
        data: addAntiForgeryToken(postData),
        success: function (data) {
            if (data.isError) {
                showNotification('error', 'بروز خطا', data.message);
            } else {
                var tr = $('[data-id="' + id + '"]').parents('tr');
                var d = dataTableButtons.row(tr).data();
                d.Published = !published;
                dataTableButtons.row(tr).data(d).draw();
                showNotification('success', 'انجام شد', data.message);
            }
        },
        error: function (error) {
            showNotification('error', 'بروز خطا', data.message);
        }
    });
}

