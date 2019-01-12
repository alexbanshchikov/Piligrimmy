function Getcards() {
    $.ajax({
        url: '/api/cards?id=' + sessionStorage.getItem('id_Client'),
        type: 'GET',
        contentType: "application/json",
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem("accessToken");
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (cards) {
            var rows = "";
            $.each(cards, function (index, c) {
                rows += row(c);
            })
            $(tableMain).find('tbody').append(rows);
        }
    });
}


var tokenKey = "accessToken";
var clientKey = "id_Client";
$('#submitLogin').click(function (e) {
    e.preventDefault();
    var loginData = {
        grant_type: 'password',
        username: $('#emailLogin').val(),
        password: $('#passwordLogin').val()
    };

    $.ajax({
        type: 'POST',
        url: '/token',
        data: loginData
    }).success(function (data) {
        $('.userName').text(data.username);
        $('.id_client').text(data.id_Client);

        sessionStorage.setItem(tokenKey, data.access_token);
        sessionStorage.setItem(clientKey, data.id_Client);
        window.location.href = "/Index.html";
    }).fail(function (data) {
        console.log(data);
    });
});

$('#logOut').click(function (e) { //Обработчик выхода пользователя
    e.preventDefault();
    sessionStorage.removeItem(tokenKey);
});

$('#getDataByLogin').click(function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: '/api/values/getlogin',
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (data) {
            alert(data);
        },
        fail: function (data) {
            console.log(data);
        }
    });
});

$('#getDataByRole').click(function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: '/api/values/getrole',
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (data) {
            alert(data);
        },
        fail: function (data) {
            console.log(data);
        }
    });
});


