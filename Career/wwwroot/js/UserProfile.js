$(document).ready(function () {
    $.ajax({
        url: getPostedJobs,
        type: 'GET',
        success: function (result) {
            $('#postedJobs').html(result);
        }
    });

    $.ajax({
        url: getApplications,
        type: 'GET',
        success: function (result) {
            $('#jobsApplications').html(result);
        }
    });

});

