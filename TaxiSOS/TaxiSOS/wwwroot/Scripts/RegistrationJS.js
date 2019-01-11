$('#registration').click(function (e) {
        $.ajax({
            url: '/api/account',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({
                TelephoneNumber: document.getElementById("TelephoneNumber").value,
                Password: document.getElementById("Password").value, 
                Role: "CLIENT",
                City: document.getElementById("Mail").value,
                Email: document.getElementById("city").value
            }),
            success: function (result) {
                alert(result);
            }
        });
});