$(document).ready(function () {
   
    $("input[name='fullname']").focus(function () {
        $("#nameLabel").html('');
    });
    $("input[name='email']").focus(function () {
        $("#emailLabel").html('');
    });
    $("input[type='password']").focus(function () {
        $("#passwordLabel").html('');
    }); 
   
    $("input[type='text']").keyup(function () {
        ValidateName();

    });
    $("input[type='email']").keyup(function () {
        ValidateEmail();
       
    });
    $("#password").keyup(function () {
        ValidatePassword();
       

    });
    $("#confirm").keyup(function () {
        ValidateConfirm();

    });

    $("#SaveName").click(function () {
        ValidateName();
        if ($("#nameLabel").html().length == 0) {

            $("#form1").submit();

        }


    });

    $("#SaveEmail").click(function () {
        ValidateEmail();
        if ($("#emailLabel").html().length == 0) {
            $("#form2").submit();
        }

    });


    $("#SavePassword").click(function () {
        ValidatePassword();
        ValidateConfirm();
        if ($("#passwordLabel").html().length == 0)
        {
            $("#form3").submit();
        }


    });

    function ValidateEmail() {
        if ($("#email").val().length == 0) {
            $("#emailLabel").html("You can't leave this empty")
        }
        else if (!isValidEmailAddress($("#email").val())) {
            $("#emailLabel").html("Please enter valid email")
        }
        else {
            $("#emailLabel").html("");
        }
    }
    function ValidateName() {
        if ($("#fullname").val().length == 0) {
            $("#nameLabel").html("You can't leave this empty")
        }
        else if (!isValidName($("#fullname").val())) {
            $("#nameLabel").html("Please enter valid username");
        }
        else {
            $("#nameLabel").html("")
        }
    }
    function ValidatePassword() {
        if ($("#password").val().length == 0) {
            $("#passwordLabel").html("You can't leave this empty")
        }
        else if ($("#password").val().length < 6) {
            $("#passwordLabel").html("Min 6 character")
        }
        else {
            $("#passwordLabel").html("")
        }
    }
    function ValidateConfirm() {
        if ($("#confirm").val().length == 0) {
            $("#passwordLabel").html("You can't leave this empty")
        }
        else if ($("#confirm").val().length < 6) {
            $("#passwordLabel").html("Min 6 character")
        }
        else if ($("#confirm").val() != $("#password").val()) {
            $("#passwordLabel").html("Password don't match")
        }
        else {
            $("#passwordLabel").html("")
        }
    }
});

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
};
function isValidName(name) {
    var pattern = new RegExp(/^[a-zA-Z0-9]{4,10}$/);
    return pattern.test(name);
};

