var hplc = (function (scope) {
    scope.ajaxCall = function (method, url, data, dataType, f, headers = null, asyncHit = true) {
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
    scope.grid_options = {
        width: "100%",
        height: "auto",
        filtering: true,
        editing: true,
        sorting: true,
        autoload: true,
        pageSize: 15,
        pageButtonCount: 5,
        paging: true
    };
    scope.filteOnKeyPress = function (grid) {
        $(":input", $(grid)).keydown(function () {
            var self = this;
            if (self.timeout)
                clearTimeout(self.timeout);

            if (self.value.length === 0)
                $(grid).jsGrid('loadData');

            self.timeout = setTimeout(function () {
                $(grid).jsGrid('loadData');
            }, 200);
        });
        $("tr:has(td i.inactive)", $(grid)).addClass("inactive");
    };

    return scope;
})(hplc || {});

function exportInExcel(elem) {
    var table = document.getElementById("rpt-table");
    var html = table.outerHTML;
    var url = 'data:application/vnd.ms-excel,' + escape(html); // Set your html table into url 
    elem.setAttribute("href", url);
    elem.setAttribute("download", "export.xls"); // Choose the file name
    return false;
}