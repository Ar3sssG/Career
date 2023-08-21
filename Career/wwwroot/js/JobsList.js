$(document).ready(function () {
    $.ajax({
        url: getIndustries,
        type: 'GET',
        success: function (result) {
            $('#industriesList').html(result);
        }
    });
});

$("#searchBar").on("keyup", function () {
    var searchStr = $("#searchBar").val();
    $.ajax({
        url: getJobsList,
        type: 'GET',
        data: { "searchText": searchStr },
        success: function (data) {
            $('#listGroup').html(data);
        }
    });
});

//filter by 
const checks = []

function filterBy(data,value) {
    if(data.checked) {

        checks.push(value);
    }
    else {
        for (var i = 0; i < checks.length; i++) {
            if (checks[i] == value) {
                checks.splice(i, 1);
            }
        }
        
    }
    $.ajax({
        url: getJobsList,
        type: 'GET',
        traditional: true,
        data: { "professionType": checks },
        success: function (data) {
            $('#listGroup').html(data);
        }
    });
}

$("#sortPost").on('click', function () {
    $.ajax({
        url: getJobsList,
        type: 'GET',
        data: { "sortType": 1 },
        success: function (data) {
            $('#listGroup').html(data);
        }
    })
})

$("#sortDead").on('click', function () {
    $.ajax({
        url: getJobsList,
        type: 'GET',
        data: { "sortType": 2 },
        success: function (data) {
            $('#listGroup').html(data);
        }
    })
})

