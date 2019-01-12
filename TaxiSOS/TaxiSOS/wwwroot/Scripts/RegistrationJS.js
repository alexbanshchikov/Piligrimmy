$('#registration').click(function (e) { 
    if ((document.getElementById("TelephoneNumber").value != '') && (document.getElementById("Password").value != '') &&
        (document.getElementById("city").value != '') && (document.getElementById("Mail").value != ''))
    {
        if (document.getElementById("Password").value == document.getElementById("Password2").value) {
            $.ajax({
                url: '/api/account',
                type: 'POST',
                contentType: "application/json",
                data: JSON.stringify({
                    TelephoneNumber: document.getElementById("TelephoneNumber").value,
                    Password: document.getElementById("Password").value,
                    Role: "CLIENT",
                    City: document.getElementById("city").value,
                    Email: document.getElementById("Mail").value
                }),
                success: function (result) {
                    document.location.href = "http://localhost:53389";
                }
            });
        } else {
            alert("Пароли не совпадают");
        }
    } else {
        alert("Заполните поля");
    }
});