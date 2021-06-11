var hpls = (function (scope) {
    scope.ajaxCall = function (method, url, data, dataType, f, headers = null, asyncHit = true, showWaiting = true) {
        if (showWaiting) {
            tpc.waitToggle();
        }
        $.ajax({
            type: method,
            url: url,
            data: data,
            headers: headers,
            dataType: dataType,
            async: asyncHit,
            success: function (d) {
                f(d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                f("");
            }
        });
    };

    scope.login = function () {
        $("#responsemsg", "#loginform").html("");
        var url = "/home/authenticate";
        var data = $("#loginform").serialize();
        hplc.ajaxCall("POST", url, data, "text", function (d) {
            var jsonResponse = JSON.parse(d);
            if (jsonResponse == "Fail") {
                $("#loginForm").val("");
                $("#msg").text("username and password is wrong..!");
            }
            else {
                window.location.href = "/home/Login";
                $("#msg").hide();
            }
        });
    };
    scope.PatientAppointment = function () {
        $("#responsemsg", "#PatientAppointform").html("");
        var url = "/home/patientappointment";
        var data = $("#PatientAppointform").serialize();
        hplc.ajaxCall("POST", url, data, "text", function (d) {
            var jsonResponse = JSON.parse(d);
            if (jsonResponse == "Fail") {
                $("#appointmentstatusmsg").text("Technical error please try after some time..!");
            }
            else {
                $("#PatientAppointform").val("");
                $("#appointmentstatusmsg").text("Appointment Registered Sucessfully..!");
            }
        });
    };

    return scope;

})(hpls || {});

$(function () {
    $("#loginform").validate({
        submitHandler: function () { hpls.login() },
        rules: {
            loginid: { required: true },
            password: { required: true },
        },
        messages: {
            loginid: "please enter loginid",
            password: "please enter password"
        }
    });

    $("#PatientAppointform").validate({
        submitHandler: function () { hpls.PatientAppointment() },
        rules: {
        },
        messages: {
        }
    });

    $('#loginModal').on('show.bs.modal', function () {
        $("input", ".modal").removeClass('error');
        $("#responsemsg", ".modal").html("");
        $(".error", ".modal").html("");

        $(this).find("form").trigger('reset');
        $(this).find("form").validate().resetForm();
        $(this).find("form .refresh").click();
        $("#msg").text("");
        $("#appointmentstatusmsg").text("");     
    });
    $('#AppointmentModal').on('show.bs.modal', function () {
        $("input", ".modal").removeClass('error');
        $("#responsemsg", ".modal").html("");
        $(".error", ".modal").html("");

        $(this).find("form").trigger('reset');
        $(this).find("form").validate().resetForm();
        $(this).find("form .refresh").click();
        $("#appointmentstatusmsg").text("");
    });

});

