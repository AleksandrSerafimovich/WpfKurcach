$(document).ready(function () {

    $("#jqGrid").jqGrid({

        url: 'https://localhost:44384/Grid/GetDataTable',
        datatype: "json",
        colNames: ['Название', 'Текст', 'Рейтинг', 'Id'], 
        colModel: [
            { label: 'Title', name: 'Title', index: 'Title', width: 50, search: true , sortable: true, editable: true, edittype: 'text'},
            { label: 'Text', name: 'Text', index: 'Text', width: 300, sortable: true, search: true, editable: true, edittype: 'textarea' },
            { label: 'Rating', name: 'Rating', index: 'Rating', width: 35, align: "center", search: true, sortable: true, editable: false },
            { label: 'Id', name: 'Id', index: 'Id', width: 35, align: "center", hidden: true,  sortable: true, key: true, editable: false }
        ],
        loadOnce: true,
        viewrecords: true,
        autowidth: true,
        height: 400,
        rowNum: 10,
        pager: "#jqGridPager"
    });
    $('#jqGrid').navGrid('#jqGridPager',
        // the buttons to appear on the toolbar of the grid
        { edit: true, add: true, del: true, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            height: 600,
            editCaption: "Редактирование",
            recreateForm: true,
            closeAfterEdit: true,
            errorTextFormat: function (data) {
                return 'Error: ' + data.responseText
            },
            onclickSubmit: function (params) {
                var list = $("#jqGrid");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                params.url = 'https://localhost:44384/Grid/EditData';
            },
            afterSubmit: function (response, postdata) {
                // обновление грида
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, "", 0]
            }
        },
        // options for the Add Dialog
        {
            height: 600,
            closeAfterAdd: true,
            recreateForm: true,
            onclickSubmit: function (params) {
                var list = $("#jqGrid");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                params.url = 'https://localhost:44384/Grid/AddData';
            },
            afterSubmit: function (response, postdata) {
                // обновление грида
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, "", 0]
            }
        },
        // options for the Delete Dailog
        {
            errorTextFormat: function (data) {
                return 'Error: ' + data.responseText
            },
            onclickSubmit: function (params) {
                var list = $("#jqGrid");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                params.url = 'https://localhost:44384/Grid/DeleteData';
            },
            afterSubmit: function (response, postdata) {
                // обновление грида
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, "", 0]
            }
        });
});
