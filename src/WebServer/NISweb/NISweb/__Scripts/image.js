
$(document).ready(function () {
    var table = $('.XYZ_adjust_table').DataTable({
        //検索、検索結果非表示
        searching: false,
        lengthChange: false,
        paging: false,
        "bInfo": false,

        scrollY: 120,
        scrollX: true,
        autoWidth:true
    });
    
    for (var i = 1; i < 3; i++) {
        var current_row_no = $('.XYZ_adjust_table > tbody > tr:last td:first').html();
        XYZ_add_row(current_row_no);
    }
});

$(document).ready(function () {
    $('.shooting_interval_table').DataTable({
        searching: false,
        lengthChange: false,
        paging: false,
        "bInfo": false,
        
        scrollY: 120,
        scrollX: true,
        autoWidth: true
    });
    
    for (var i = 1; i < 3; i++) {
        var current_row_no = $('.shooting_interval_table > tbody > tr:last td:first').html();
        shooting_add_row(current_row_no);
    }
});



$(document).ready(function () {
    $('.image_list_table').DataTable({
        searching: false,
        lengthChange: false,
        paging: false,
        "bInfo": false,
        
        autoWidth: false
    });
});

//add row to top table
$(document).on("click", ".XYZ_adjust_add_row_button", function (e) {
    
    //最終行のrow_No.を取得
    var current_row_no = $('.XYZ_adjust_table > tbody > tr:last td:first').html();
    //次行のrow_No.
    XYZ_add_row(current_row_no);
});


function XYZ_add_row(current_row_no)
{
    //次行のrow_No.
    var next_row_no = parseInt(current_row_no);
    ++next_row_no;
    
    $('.XYZ_adjust_table > tbody > tr:first').clone(true).appendTo(
        $('.XYZ_adjust_table > tbody')
    );

    $('.XYZ_adjust_table > tbody > tr:last td:first').html(next_row_no);
}

//add row to bottom table
$(document).on("click", ".shooting_interval_add_row_button", function (e) {

    //最終行のrow_No.を取得
    var current_row_no = $('.shooting_interval_table > tbody > tr:last td:first').html();
    //次行のrow_No.
    shooting_add_row(current_row_no);
});

function shooting_add_row(current_row_no) {
    //次行のrow_No.
    var next_row_no = parseInt(current_row_no);
    ++next_row_no;

    $('.shooting_interval_table > tbody > tr:first').clone(true).appendTo(
        $('.shooting_interval_table > tbody')
    );

    $('.shooting_interval_table > tbody > tr:last td:first').html(next_row_no);
}


//実験
////Toggle between showing and hiding to top table
//$(document).ready(
//    function () {
//        $('.XYZ_adjust_table_div').hide();
//        $('#XYZ_adjust_table_location').change(function () {
//            if ($(this).is(':checked')) {
//                $('.XYZ_adjust_table_div').show();
//            }
//            else {
//                $('.XYZ_adjust_table_div').hide();
//            }
//        });
//    }
//);

////Toggle between showing and hiding to bottom table
//$(function () {
//    $('.shooting_interval_table_div').hide();
//    $('#shooting_interval_table_location').change(function () {
//        if ($(this).is(':checked')) {
//            $('.shooting_interval_table_div').show();
//        }
//        else {
//            $('.shooting_interval_table_div').hide();
//        }
//    });
//})

//マウス座標取得
$(document).on("click", "#target_img", function (e) {

    //最終行のrow_No.を取得
    var mouseX = e.clientX;
    var mouseY = e.clientY;
    
    //画像
    var img = $('#target_img').get(0);
    var bounds = img.getBoundingClientRect();
    
    var calcX = mouseX - (bounds.left);
    var calcY = mouseY - (bounds.top);
    
    alert('x:' + calcX + '\r\n' + 'y:' + calcY);
});

$(function (){

    if ($('#connection_status').text() == 'Disconnected') {
        $('#connection_status').css('color', 'Red');
    }
    else {
        $('#connection_status').css('color', 'Blue');
    }
})
