// Получение карт пользователя
//<script></script>
function Getcards() {
    var token = sessionStorage.getItem(data.access_token);
    sessionStorage.getItem(data.access_token);
    $.ajax({
        url: '/api/cards',
        type: 'GET',
        contentType: "application/json",
        success: function (cards) {
            var rows = "";
            $.each(cards, function (index, c) {
                rows += row(c);
            })
            $(tableMain).find('tbody').append(rows);
        }
    });
}

//Отображение формы создания карты
function show(elementId) {
    var div = document.getElementById(elementId);
    div.style.display = 'block';
    //document.getElementById(elementId).style.display = "block";
}

// создание строки для таблицы
var row = function (c) {
    return "<tr data-rowid='" + c.cardNumber + "'><td style= \'display: none;\' >" + c.idClient + "</td>" +
        "<td>" + c.cardNumber + "</td> <td>" + c.cardOwner + "</td>" +
        "<td>" + c.expireDate + "</td> <td>" + c.cvv + "</td>" +
        "<td><a class='editLink' data-id='" + c.cardNumber + "'>Изменить</a> | " +
        "<a class='removeLink' data-id='" + c.cardNumber + "'>Удалить</a></td></tr>";
}

function hideCardForm() {
    var div = document.getElementById('cardForm');
    div.style.display = 'none';
}

// сброс формы
function reset() {
    var form = document.forms["cardForm"];
    form.reset();
    form.elements["id"].value = 0;
}

// Добавление карты
function CreateCard(_idClient, _cardNumber, _cardOwner, _expireDate, _cvv) {
    $.ajax({
        url: "api/cards",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            IdClient: _idClient,
            CardNumber: _cardNumber,
            CardOwner: _cardOwner,
            ExpireDate: _expireDate,
            Cvv: _cvv
        }),
        success: function (c) {
            hideCardForm();
            $(tableMain).find('tbody').append(rows);
        }
    })
}

// сброс значений формы
$("#reset").click(function (e) {
    e.preventDefault();
    reset();
})

// нажимаем на кнопку Сохранить
$("form").submit(function (e) {
    e.preventDefault();
    var idClient = "a8bdf886-acbe-43fc-a9c4-0c4c186273a4";
    var cardNumber = this.elements["CardNumber"].value;
    var cardOwner = this.elements["CardOwner"].value;
    var expireDate = this.elements["ExpireDate"].value;
    var cvv = this.elements["Cvv"].value;
    //if (id == 0)
    CreateCard(idClient, cardNumber, cardOwner, expireDate, cvv);
   // else
    //    EditCase(id, title, reportingForm, done, creationDate, reportDate);
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function () {
    var id = $(this).data("id");
    GetCase(id);
})
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function () {
    var id = $(this).data("id");
    DeleteCase(id);
    location.reload();
})

Getcards();
