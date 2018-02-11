// Write your JavaScript code.

$('#loginForm').on('submit', function (e) {
    e.preventDefault();
    var data = toBodyForm($('#loginForm'));
    var action = '/user/SignIn';
    $.ajax({
        type: "POST",
        url: action,
        data: data,
        contentType: 'application/json',
        accept: "application/json",
        success: function (result) {
            alert("success");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("fail");
        }
    });
});

function toBodyForm(form) {
    var formData = form.serializeArray();
    var formObject = {};
    $.each(formData,
        function (i, v) {
            formObject[v.name] = v.value;
        });
    return JSON.stringify(formObject);
}