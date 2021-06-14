var hplm = (function (scope) {
    scope.departmentMasterOnLoad = function () {
        hplc.ajaxCall("GET", "/admin/departmentlists?type=list", {}, "text", function (d) {
            var departmentMasterList = JSON.parse(d);
            if (departmentMasterList !== "null") {
                var fields = [
                    { name: "DepartmentId", css: "hidden", },
                    { name: "HospitalId", css: "hidden", },
                    { name: "HospitalName", type: "text", title: "Hospital Name", sorting: false, filtering: true, width: 50 },
                    { name: "DepartmentName", type: "text", title: "Department Name", sorting: true, filtering: true, width: 50 },
                ];
                var options = {
                    filtering: true,
                    editing: true,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    rowClick: function (args) {
                        editDepartmentMaster("Edit", args.item)
                    },
                    controller: {
                        departmentLists: departmentMasterList.DepartmentLists,
                        loadData: function (filter) {
                            return $.grep(this.departmentLists, function (departmentList) {
                                return (!filter.DepartmentName || departmentList.DepartmentName.toLowerCase().indexOf(filter.DepartmentName.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, hplc.grid_options);
                $("#departmentMasterList").jsGrid(options);
                hplc.filteOnKeyPress("#departmentMasterList");
            }

            var formSubmitHandler = $.noop;
            formSubmitHandler = function () {
                var departmentMaster = {};
                manageDepartmentMaster(departmentMaster)
            };
            var editDepartmentMaster = function (dilogType, departmentMaster) {
                if (departmentMaster.DepartmentId > 0) {
                    $("#DepartmentId").val(departmentMaster.DepartmentId)
                    $("#HospitalList").val(departmentMaster.HospitalId)
                    $("#DepartmentName").val(departmentMaster.DepartmentName)
                    $("#btnSubmit").text("Update")
                }
                else {
                    $("#DepartmentId").val("0");
                    $("#btnSubmit").text("Submit")
                }
            };
        });
    };

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
                manageDoctorMaster(doctorMaster)
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
    scope.Testmasterdataonload = function () {
        hplc.ajaxCall("GET", "/admin/TestMasterData?type=list", {}, "text", function (d) {
            var TestMasterList = JSON.parse(d);
            if (TestMasterList !== "null") {
                var fields = [
                    { name: "TestID", css: "hidden", },
                    { name: "TestName", type: "text", title: "Test Name", sorting: true, filtering: true, width: 50  },
                    { name: "Charge",  title: "Amount", sorting: true, filtering: true, width: 50  },
                    { name: "IsDiscription", title: "Type", sorting: true, filtering: true, width: 50  },                  
                ];
                var options = {
                    filtering: true,
                    editing: true,
                    sorting: true,
                    paging: true,
                    autoload: true,
                    rowClick: function (args) {
                        editTestMaster("Edit", args.item)
                    },
                    controller: {
                        TestLists: TestMasterList.TestMasterLists,
                        loadData: function (filter) {
                            return $.grep(this.TestLists, function (testList) {
                                return (!filter.TestName || testList.TestName.toLowerCase().indexOf(filter.TestName.toLowerCase()) > -1);
                            });
                        }
                    },
                    fields: fields
                };
                $.extend(options, hplc.grid_options);
                $("#TestMasterList").jsGrid(options);
                hplc.filteOnKeyPress("#TestMasterList");
            }

            var formSubmitHandler = $.noop;
            //formSubmitHandler = function () {
            //    var TestMasterLists = {};
            //    manageHospitalMaster(TestMasterLists)
            //};
            var editTestMaster = function (dilogType, TestMaster) {
                if (TestMaster.TestID > 0) {
                    var ProcessingUrl = 'TestMaster?TestId=' + TestMaster.TestID;
                    window.open(ProcessingUrl, '_self');
                }
                else {
                   
                }
            };
        });
    };

    return scope;
})(hplm || {});

//$(function () {
//    $("#manage-opd-appointment").validate({
//    });
//});