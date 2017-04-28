var notDisaplay = [];
function getList(url, pageIndex, searchContent,orderBy, orderType) {
    currentUrl = url;
    currentPage = pageIndex;
    console.log(searchContent);
    if (searchContent == null || searchContent == undefined) {
        currentSearchContent = "";
    }
    else {
        currentSearchContent = searchContent;
    }                                          
    if (orderBy == null || orderBy == undefined) {
        orderBy = 0;
    }
    if (orderType == null || orderType == undefined) {
        orderType = 0;
    }
    $.post(url, { pageIndex: pageIndex, perpage: 10, orderBy: orderBy, orderType: orderType, searchContent: searchContent }, function (data) {
        generateTable(data);
        loadPagination(data.totalCount,pageIndex);
    })
}
function generateTable(data) {

    var s = currentUrl.split('/');
    var domain = "/" + s[1] + "/";
    var content = '<table class="table table-hover datatable files-list dataTable" id="tbfilelist" aria-describedby="tbfilelist_info"><thead>'
          + '<tr class="primary" role="row">'
          + '<th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label="" style="width: 35px;" ></th>';
    $.each(data.tableHeaders, function (index) {
        var tableHeader = data.tableHeaders[index];
        content += '<th class="sorting" role="columnheader" tabindex="0"  rowspan="1" colspan="1" onclick="sort(' + index + ')" >' + tableHeader + '<i class="fa fa-sort"></i></th>';
    }
    )
    content += '<th class="sorting" role="columnheader" tabindex="0"  rowspan="1" colspan="1">操作</th></tr></thead><tbody role="alert" aria-live="polite" aria-relevant="all">'
    $.each(data.data, function (index) {
        var obj = data.data[index];
        content += '<tr class="sorting"><td><input type="checkbox" name="Selected" value='+obj.Id+'></td>';
        $.each(obj, function (key) {
            console.log("111");
            if (key == "Content" || key=="Title") {
                content += '<td ><div style="width:500px;text-overflow:ellipsis;overflow:hidden;height:100px;">' + obj[key] + '</div></td>';
            }
            else{
                content += '<td>' + obj[key] + '</td>';
            }
            
        });
        ///目前的方法治标不治本，很尴尬，复合主键的话需要另外做一个处理更好
        if (obj.SchoolId != undefined && obj.NewTypeId != undefined) {
            content += '<td style="width:150px;"><a href="' + domain + 'Edit?schoolId=' + obj.SchoolId + '&newTypeId=' + obj.NewTypeId + '"><i class="fa fa-edit" title="编辑"></i>编辑</a><a href="' + domain + 'Details?schoolId=' + obj.SchoolId + '&newTypeId=' + obj.NewTypeId + '"><i class="fa fa-book" title="详情">详情</i></a><a href="' + domain + 'Delete?schoolId=' + obj.SchoolId + '&newTypeId=' + obj.NewTypeId + '"><i class="fa fa-trash-o fa-fw" title="删除"></i>删除</a></td></tr>';
        }
        else {
            content += '<td style="width:150px;"><a href="' + domain + 'Edit?id=' + obj.Id + '"><i class="fa fa-edit" title="编辑"></i>编辑</a><a href="' + domain + 'Details?id=' + obj.Id + '"><i class="fa fa-book" title="详情">详情</i></a><a href="' + domain + 'Delete?id=' + obj.Id + '"><i class="fa fa-trash-o fa-fw" title="删除"></i>删除</a></td></tr>';
        }
    }
    )
    content += '</tbody></table>';
    $("#content").html(content);
}
function sort(index) {
    $("#content").html("");
    getList(currentUrl, currentPage, currentSearchContent,index);
}
function loadPagination(totalCount, page) {
    var PERPAGE = 10;
    var pageCount = parseInt(totalCount / PERPAGE);
    if (pageCount * PERPAGE <= totalCount) {
        pageCount++;
    }
    var sb = "";
    sb += ' <div class="dataTables_paginate paging_bootstrap">'
+ '<ul class="pagination pagination-sm" style="">'
    if (page == 1) {
        sb += "<li class='first disabled'>"
        + "<a href='#'><i class='fa fa-angle-double-left'></i></a></li>"
        + "<li class='prev disabled'>"
        + "<a href='#'><i class='fa fa-angle-left'></i></a></li>";
    }
    else {
        sb += "<li class='first'>"
        + "<a href='#' onclick = 'getList(" + currentUrl + ",1," + currentSearchContent + ")'><i class='fa fa-angle-double-left'></i></a></li>"
        + "<li class='prev'>"
        + "<a href='#' onclick = 'getList("+currentUrl+", "+(page-1)+","+currentSearchContent+")'><i class='fa fa-angle-left'></i></a></li>";
    }
    for (var i = 1; i <= pageCount; i++) {
        if (i == page) {
            sb += "<li class='active'><a href='#'>" + i + "</a></li>";
        }
        else {
            sb += "<li><a href='#' onclick='onclick = 'getList(" + currentUrl + ", " + i + "," + currentSearchContent + ")''>" + i + "</a></li>";
        }
    }
    if (page == pageCount) {
        sb += "<li class='next disabled'>"
    + "<a href='#'><i class='fa fa-angle-right'></i></a></li>"
    + "<li class='last disabled'>"
    + "<a href='#'><i class='fa fa-angle-double-right '></i></a></li>";
    }
    else {
        sb += "<li class='next'>"
    + "<a href='#' onclick=' getList(" + currentUrl + ", " + (page + 1) + "," + currentSearchContent + ")'><i class='fa fa-angle-right'></i></a></li>"
    + "<li class='last'>"
    + "<a href='#' onclick = 'getList(" + currentUrl + ", " + pageCount + "," + currentSearchContent + ")'><i class='fa fa-angle-double-right'></i></a></li>";
    }
    //sb+="<li class='pagi-input'>"
    //+ "<label class='control-label'>跳转至</label><input class='form-control'></li>";
    sb += "</ul></div>"
    $("#pagination").html(sb);
}
