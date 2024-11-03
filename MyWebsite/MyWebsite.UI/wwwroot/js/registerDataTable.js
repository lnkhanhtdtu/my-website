function registerDataTable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        scrollY: 480,
        scrollX: true,
        processing: true,
        serverSide: true,
        columns: columns,
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json'
        },
        language: {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Hiển thị _MENU_ dòng",
            "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
            "sInfo": "Đang hiển thị _START_ đến _END_ trong tổng số _TOTAL_ dòng",
            "sInfoEmpty": "Đang hiển thị 0 đến 0 trong tổng số 0 dòng",
            "sInfoFiltered": "(được lọc từ _MAX_ dòng)",
            "sInfoPostFix": "",
            "sSearch": "<i class=\"ri-search-line\"></i> Tìm:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "<i class=\"ri-skip-left-fill\"></i>",
                "sPrevious": "<i class=\"ri-arrow-left-s-fill\"></i>",
                "sNext": "<i class=\"ri-arrow-right-s-fill\"></i>",
                "sLast": "<i class=\"ri-skip-right-fill\"></i>"
            }
        },
        drawCallback: function (settings) {
            if ($(elementName).DataTable().data().length === 0) {
                $(elementName + '_paginate').hide();
            } else {
                $(elementName + '_paginate').show();
            }
        }
    });
}