var hpldm = (function (scope) {
    scope.doctorMasterOnLoad = function () {
        hplc.ajaxCall("GET", "/admin/doctorlists?type=list", {}, "text", function (d) {
            var doctorMasterList = JSON.parse(d);
            if (doctorMasterList !== "null") {
                var fields = [
                    { name: "DoctorId", css: "hidden", },
                    { name: "doctorId", css: "hidden", },
                    { name: "DepartmentId", css: "hidden", },
                    { name: "DoctorName", type: "text", title: "Doctor Name", sorting: true, filtering: true, width: 50 },
                    { name: "ContactNumber", type: "text", title: "Contact Number", sorting: false, filtering: false, width: 50 },
                    { name: "Address", type: "text", title: "Address", sorting: false, filtering: false, width: 50 },
                    { name: "EmailId", type: "text", title: "Email Id", sorting: false, filtering: false, width: 50 },
                    { name: "Website", css: "hidden", },
                    { name: "Description", css: "hidden", },
                    { name: "Commission", css: "hidden", },
                ];
                var options = {
                    filtering: true,
                    editing: true,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    rowClick: function (args) {
                        editDoctorMaster("Edit", args.item)
                    },
                    controller: {
                        doctorLists: doctorMasterList.DoctorLists,
                        loadData: function (filter) {
                            return $.grep(this.doctorLists, function (doctorList) {
                                return (!filter.DoctorName || doctorList.DoctorName.toLowerCase().indexOf(filter.DoctorName.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, hplc.grid_options);
                $("#doctorMasterList").jsGrid(options);
                hplc.filteOnKeyPress("#doctorMasterList");
            }

            var formSubmitHandler = $.noop;
            formSubmitHandler = function () {
                var doctorMaster = {};
                managedoctorMaster(doctorMaster)
            };
            var editDoctorMaster = function (dilogType, doctorMaster) {
                if (doctorMaster.DoctorId > 0) {
                    $("#DoctorId").val(doctorMaster.DoctorId)
                    $("#HospitalList").val(doctorMaster.HospitalId)
                    $("#DepartmentList").val(doctorMaster.DepartmentId)
                    $("#DoctorName").val(doctorMaster.DoctorName)
                    $("#ContactNumber").val(doctorMaster.ContactNumber)
                    $("#HospitalAddress").val(doctorMaster.Address)
                    $("#EmailId").val(doctorMaster.EmailId)
                    $("#WebsiteName").val(doctorMaster.Website)
                    $("#Commission").val(doctorMaster.Commission)
                    $("#Description").text(doctorMaster.Description)
                    $("#btnSubmit").text("Update")
                }
                else {
                    $("#DoctorId").val("0");
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };

    return scope;
})(hpldm || {});

$(function () {
    $("#manage-doctor-form").validate({
    });
});