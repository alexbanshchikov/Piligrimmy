
function GetClients() {
    var timerId = setInterval(function () {
        if ((document.getElementById("ArrivalPoint").value !== '') && (document.getElementById("DestinationPoint").value !== '')) { 

            GetCost();
            clearInterval(timerId);
            
        }
    }, 2000);
}
function GetCost() {
    $.ajax({
        url: '/api/orders/calc?From=' + document.getElementById("ArrivalPoint").value + '&To=' + document.getElementById("DestinationPoint").value,
        type: 'GET',
        contentType: "application/json",
        success: function (result) {
            var costDok = document.getElementById('cost');
            costDok.innerHTML = "Стоимость поездки: " + result;
        }
    });
    
}

$('#ordering').click(function (e) {
    if ((document.getElementById("ArrivalPoint").value !== '') && (document.getElementById("DestinationPoint").value !== ''))
    {
        if (document.getElementById('ordering').innerText === "Заказать")
        {
            if (sessionStorage.getItem("accessToken") === null)
            {
                window.location.href = "/Authorisation.html";
            }
            else
            {
                document.getElementById("ArrivalPoint").disabled = true;
                document.getElementById("DestinationPoint").disabled = true;
                $('#dialog').dialog();
                document.getElementById('id1').innerHTML = "Ожидайте уведомление на электронную почту";
                document.getElementById('ordering').innerText = "Отменить";
                $.ajax({
                    url: '/api/orders',
                    type: 'POST',
                    contentType: "application/json",
                    data: JSON.stringify({
                        IdClient: sessionStorage.getItem("idClient"),
                        ArrivalPoint: document.getElementById("ArrivalPoint").value,
                        DestinationPoint: document.getElementById("DestinationPoint").value
                    })
                });
            }
        }
        else
        {
            location.reload();
        }
    }
    else
    {
        alert("Заполните поля");
    }
});

GetClients();