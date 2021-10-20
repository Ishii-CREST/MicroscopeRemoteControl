
$(document).ready(function () {
    SetDataTable('#image_list_table');
});


$(document).ready(function () {
    SetDataTable('#api_log_table');
});


//datatable定義
function SetDataTable(tableName)
{
    $(tableName).DataTable({
        order: [[0,'asc']],
        searching: false,
        lengthChange: false,
        paging: false,
        "bInfo": false,
        scrollY: 800,
        autoWidth: false,
        destroy:true
    });
};


//接続・未接続時のテキスト色変化(jqueryの再読み込みのスパンが短すぎて追いつかない)
$(function status_ray (){

    if ($('#connection_status').text() === 'DisConnected') {
        $('#connection_status').css('color', 'Red');
    }
    else {
        $('#connection_status').css('color', 'Blue');
    }
})

//main
$(function () {
    $('index_update_button').click(function () {
        $('#own_ajax').load('/NIS/MainContent');
    });
})

//
$(function () { 
    $('.connection_lading_server_li').css("visibility", "hidden");
})
//サーバとの接続アイコンの表示・非表示
$(function () {
    $('.main_status_update_button').click(function () {
        $('.connection_lading_server_li').css("visibility", "visible");
        $('.connection_lading_server_li').delay(20000).queue(function () {
            $(this).css("visibility", "hidden");
        });
    });
})

//retrieved image
$(function () {
    $.ajaxSetup({ cache: false });
    setInterval(function () { $('#ret_ajax').load('/MainPage/RetrievedImageCh1'); }, 10000);
})

//partial viewの更新ではdatatablesの定義が反映されないため、Image List画面ごと更新させてる
$(function () {
    $.ajaxSetup({ cache: false });
    setInterval(function () { $('#imagelist_ajax').load('/MainPage/ImageList'); }, 5000);
})


window.onload = $(function () {
    //対象のImage要素
    var imageElement = $('.current_image');
    if (imageElement.length === 0) {
        return;
    }
    var img = new Image();
    img.src = imageElement.attr('src');
    var width = img.width;
    var height = img.height;
    //画面幅
    var size = GetScreenSize();
    //枚数は取得できないので、5枚という前提で
    var image = size / 5
    
    if (width === height) {
        imageElement.width(image).height(image);
    }
    if (width != height) {
        imageElement.width(image).height((height * image) / width);
    }
})

function GetScreenSize() {
    var screenW = $(window).width();
        return screenW;
}

