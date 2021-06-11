var hplappointment = (function (scope) {
    scope.appointmentOnLoad = function () {
        hplc.ajaxCall("GET", "/admin/appointmentlists?type=list", {}, "text", function (d) {
            var appointmentList = JSON.parse(d);
            if (appointmentList !== "null") {
                var fields = [
                    { name: "AppointmentId", css: "hidden", },
                    { name: "AppointmentNumber", css: "hidden", },
                    { name: "Name", type: "text", title: "Patient Name", editing: false, sorting: false, filtering: true, width: 50 },
                    { name: "Age", css: "hidden", },
                    { name: "Address", type: "text", title: "Address", editing: false, sorting: false, filtering: false, width: 50 },
                    { name: "ContactNumber", type: "text", title: "Contact Number", editing: false, sorting: true, filtering: false, width: 50 },
                    { name: "EmailId", css: "hidden", },
                    { name: "AppointmentDate", css: "hidden", },
                    { name: "AppointmentTime", css: "hidden", },
                    { name: "Description", css: "hidden", },
                    {
                        width: 20,
                        itemTemplate: function (value, args) {
                            var $customeEditButton = $('<a class="btn btn-primary"><i class="fa fa-edit"></i></a>')
                                .click(function (e) {
                                    updateAppointment('Edit', args);
                                    return false;
                                });
                            return $("<div class='display-flex'></div>").append($customeEditButton);
                        }
                    }
                ];
                var options = {
                    autoload: true,
                    controller: {
                        AppointmentLists: appointmentList.AppointmentLists,
                        loadData: function (filter) {
                            return $.grep(this.AppointmentLists, function (appointmentList) {
                                return (!filter.Name || appointmentList.Name.toLowerCase().indexOf(filter.Name.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, hplc.grid_options);
                $("#appointmentList").jsGrid(options);
                hplc.filteOnKeyPress("#appointmentList");
            }
            var formSubmitHandler = $.noop;
            formSubmitHandler = function () {
            };

            var updateAppointment = function (dilogType, OPDAppointment) {
                if (OPDAppointment.AppointmentId > 0) {
                    $('#AppointmentModal').modal('show');
                    $("#btnSubmit").text("Update")
                    $("#AppointmentId").val(OPDAppointment.AppointmentId);
                    $("#AppointmentNumber").val(OPDAppointment.AppointmentNumber);
                    $("#name").val(OPDAppointment.Name);
                    $("#age").val(OPDAppointment.Age);
                    $("#contactnumber").val(OPDAppointment.ContactNumber);
                    $("#email").val(OPDAppointment.EmailId);
                    $("#appointmentdate").val(OPDAppointment.AppointmentDate);
                    $("#appointmenttime").val(OPDAppointment.AppointmentTime);
                    $("#Address").html(OPDAppointment.Address);
                    $("#description").html(OPDAppointment.Description);
                }
                else {
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };

    scope.opdAppointmentOnLoad = function () {
        hplc.ajaxCall("GET", "/admin/opdappointmentlists?type=list", {}, "text", function (d) {
            var opdAppointmentList = JSON.parse(d);
            if (opdAppointmentList !== "null") {
                var fields = [
                    { name: "OPDId", css: "hidden", },
                    { name: "Name", type: "text", title: "Patient Name", editing: false, sorting: false, filtering: true, width: 50 },
                    { name: "Age", css: "hidden", },
                    { name: "Address", type: "text", title: "Address", editing: false, sorting: false, filtering: false, width: 50 },
                    { name: "ContactNumber", type: "text", title: "Contact Number", editing: false, sorting: true, filtering: false, width: 50 },
                    { name: "EmailId", css: "hidden", },
                    { name: "AppointmentDate", css: "hidden", },
                    { name: "AppointmentTime", css: "hidden", },
                    { name: "Description", css: "hidden", },
                    {
                        width: 20,
                        itemTemplate: function (value, args) {
                            var $customeEditButton = $('<a class="btn btn-primary"><i class="fa fa-edit"></i></a>')
                                .click(function (e) {
                                    editOPDAppointment('Edit', args);
                                    return false;
                                });
                            var $printAppointmentSlip = $('<a style="margin-left:20px" class="btn btn-primary"><i class="fa fa-print"></i></a>')
                                .click(function (e) {
                                    printAppointmentSlip('Edit', args);
                                    return false;
                                });
                            return $("<div class='display-flex'></div>").append($customeEditButton).append($printAppointmentSlip);
                        }
                    }
                ];
                var options = {
                    filtering: true,
                    editing: true,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    controller: {
                        OPDAppointmentLists: opdAppointmentList.OPDAppointmentLists,
                        loadData: function (filter) {
                            return $.grep(this.OPDAppointmentLists, function (opdAppointmentList) {
                                return (!filter.Name || opdAppointmentList.Name.toLowerCase().indexOf(filter.Name.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, hplc.grid_options);
                $("#OPDAppointmentList").jsGrid(options);
                hplc.filteOnKeyPress("#OPDAppointmentList");
            }
            var formSubmitHandler = $.noop;
            formSubmitHandler = function () {
            };
            var editOPDAppointment = function (dilogType, OPDAppointment) {
                if (OPDAppointment.AppointmentId > 0) {
                    $("#btnSubmit").text("Update")
                    $("#OPDId").val(OPDAppointment.OPDId);
                    $("#name").val(OPDAppointment.Name);
                    $("#age").val(OPDAppointment.Age);
                    $("#contactnumber").val(OPDAppointment.ContactNumber);
                    $("#email").val(OPDAppointment.EmailId);
                    $("#appointmentdate").val(OPDAppointment.AppointmentDate);
                    $("#appointmenttime").val(OPDAppointment.AppointmentTime);
                    $("#opdcharge").val(OPDAppointment.OPDCharge);
                    $("#Address").html(OPDAppointment.Address);
                    $("#description").html(OPDAppointment.Description);
                }
                else {
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };
    return scope;
})(hplm || {});

$(function () {
    $("#manage-opd-appointment").validate({
    });
});