$("#checkAll").click(function () {
    $(".check").prop('checked', $(this).prop('checked'));
});


function sortTable() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("myTable");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.getElementsByTagName("TR");
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[1];
            y = rows[i + 1].getElementsByTagName("TD")[1];
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function DecsSortTableByDate() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("myTable");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.getElementsByTagName("TR");
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[2].innerText;
            y = rows[i + 1].getElementsByTagName("TD")[2].innerText;
            var strx = x[0] + x[1] + x[3] + x[4] + x[11] + x[12] + x[14] + x[15] + x[17] + x[18];
            var stry = y[0] + y[1] + y[3] + y[4] + y[11] + y[12] + y[14] + y[15] + y[17] + y[18];
            if (Number(stry) > Number(strx)) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function AcsSortTableByDate() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("myTable");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.getElementsByTagName("TR");
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[2].innerText;
            y = rows[i + 1].getElementsByTagName("TD")[2].innerText;
            var strx = x[0] + x[1] + x[3] + x[4] + x[11] + x[12] + x[14] + x[15] + x[17] + x[18];
            var stry = y[0] + y[1] + y[3] + y[4] + y[11] + y[12] + y[14] + y[15] + y[17] + y[18];
            if (Number(stry) < Number(strx)) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}
