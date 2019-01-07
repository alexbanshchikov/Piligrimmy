
function GetClients() {
    $.ajax({
        url: '/api/orders',
        type: 'GET',
        contentType: "application/json",
        success: function (clients) {

        }
    });
}

GetClients();