
$(document).ready(function () {
    $.ajax({
        url: getProfessions,
        type: 'GET',
        data: { "industryId": 1 },
        success: function (result) {
            $('#profSelect').html(result);
        }
    });
});

$("#Industry").on('change', function () {
    $.ajax({
        url: getProfessions,
        type: 'GET',
        data: { "industryId": $(this).val()},
        success: function (result) {
            $('#profSelect').html(result);
        }
    });
})

