$('#registration').click(function (e) {   
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
});