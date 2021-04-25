var hplhm = (function (scope) {
    scope.hospitalMasterOnLoad = function () {
        hplc.ajaxCall("GET", "/admin/hospitallists?type=list", {}, "text", function (d) {
            var hospitalMasterList = JSON.parse(d);
            if (hospitalMasterList !== "null") {
                var fields = [
                    { name: "HospitalId", css: "hidden", },
                    { name: "Website", css: "hidden", },
                    { name: "Type", css: "hidden", },
                    { name: "Description", css: "hidden", },
                    { name: "HospitalName", type: "text", title: "Hospital Name", sorting: true, filtering: true, width: 50 },
                    { name: "Address", type: "text", title: "Address", sorting: false, filtering: false, width: 50 },
                    { name: "ContactNumber", type: "text", title: "Contact Number", sorting: false, filtering: false, width: 50 },
                    { name: "EmailId", type: "text", title: "Email Id", sorting: false, filtering: false, width: 50 },
                ];
                var options = {
                    filtering: true,
                    editing: true,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    rowClick: function (args) {
                        editHospitalMaster("Edit", args.item)
                    },
                    controller: {
                        hospitalLists: hospitalMasterList.HospitalLists,
                        loadData: function (filter) {
                            return $.grep(this.hospitalLists, function (hospitalList) {
                                return (!filter.HospitalName || hospitalList.HospitalName.toLowerCase().indexOf(filter.HospitalName.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, hplc.grid_options);
                $("#hospitalMasterList").jsGrid(options);
                hplc.filteOnKeyPress("#hospitalMasterList");
            }

            var formSubmitHandler = $.noop;
            formSubmitHandler = function () {
                var hospitalMaster = {};
                manageHospitalMaster(hospitalMaster)
            };
            var editHospitalMaster = function (dilogType, hospitalMaster) {
                if (hospitalMaster.HospitalID > 0) {
                    $("#HospitalId").val(hospitalMaster.HospitalID)
                    $("#Type").val(hospitalMaster.Type)
                    $("#HospitalName").val(hospitalMaster.HospitalName)
                    $("#HospitalAddress").val(hospitalMaster.Address)
                    $("#ContactNumber").val(hospitalMaster.ContactNumber)
                    $("#EmailId").val(hospitalMaster.EmailId)
                    $("#WebsiteName").val(hospitalMaster.Website)
                    $("#Description").val(hospitalMaster.Description)
                    $("#btnSubmit").text("Update")
                }
                else {
                    $("#HospitalId").val("0");
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };

    return scope;
})(hplhm || {});

$(function () {
    $("#manage-hospital-form").validate({
    });
});