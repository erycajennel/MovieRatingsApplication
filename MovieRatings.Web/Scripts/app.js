$(document).ready(function () {
    initMovies();
    initRatings();

    $('.releaseDate').datepicker();
});

function initMovies() {
    $.ajax({
        url: "/api/Movies",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Title + '</td>';
                html += '<td>' + formatDate(new Date(item.ReleaseDate)) + '</td>';
                html += '<td>' + item.Rating.Code + '</td>';
                html += '<td><a href="x" onclick="return getMovieById(' + item.Id + ')">Edit</a> <span> | </span>' 
                    + '  <a href="#" id="btnConfirm" data-id="' + item.Id +'" data-toggle="modal" data-target="#confirmModal"> Delete</a>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function initRatings() {
    $.ajax({
        url: "/api/Ratings",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var $dropdown = $("#rating");
            $.each(result, function () {
                $dropdown.append($("<option />").val(this.Id).text(this.Code));
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getMovieById(Id) {
    $.ajax({
        url: "/api/Movies/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result.Id);
            $('#title').val(result.Title);
            $('#releaseDate').val(formatDate(new Date(result.ReleaseDate)));
            $('#rating').val(result.Rating.Id);

            $('#movieModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

$('#btnDelete').click(function (e) {
    var Id = $('#deleteMovieId').val();
    $.ajax({
        url: "/api/Movies/" + Id,
        type: "DELETE",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            initMovies();
            $('#confirmModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
})

$('#btnAdd').click(function (e) {
    var movie = {
        Title: $('#title').val(),
        ReleaseDate: $('#releaseDate').val(),
        RatingId: $('#rating').val()
    };
    $.ajax({
        url: "/api/Movies",
        data: JSON.stringify(movie),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            initMovies();
            $('#movieModal').modal('hide');
        },
        error: function (jqXhr, textStatus, errorThrown) {
             handleError(JSON.parse(jqXhr.responseText));
        }
    });
})


$('#btnUpdate').click(function (e) {
    var movie = {
        Id: $('#id').val(),
        Title: $('#title').val(),
        ReleaseDate: $('#releaseDate').val(),
        RatingId: $('#rating').val()
    };
    $.ajax({
        url: "/api/Movies/" + movie.Id,
        data: JSON.stringify(movie),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            initMovies();
            $('#movieModal').modal('hide');
        },
        error: function (jqXhr, textStatus, errorThrown) {
            handleError(JSON.parse(jqXhr.responseText));
        }
    });
})


function clearFields() {
    $('#id').val("");
    $('#title').val("");
    $('#releaseDate').val("");
    $('#rating').val(1);
    $('#errorDiv').val("");
    $('#errorDiv').hide();
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function handleError(response) {
    var errors = [];
    var errorsString = "";

    if (response != null) {

        var modelState = response.ModelState;
        for (var key in modelState) {
            if (modelState.hasOwnProperty(key)) {
                errorsString = (errorsString == "" ? "" : errorsString + "<br/>") + modelState[key];
                errors.push(modelState[key]);
            }
        }
    }
    
    if (errorsString != "") {
        $("#errorDiv").html(errorsString).show();
    }
}

function formatDate(dateToFormat) {
    if (isNaN(dateToFormat)) {
        return "";
    }
    var newDate = (dateToFormat.getMonth() + 1) + "/"
        + dateToFormat.getDate() + "/"
        + dateToFormat.getFullYear()
    return newDate;
}

$('body').on('click', '#btnConfirm', function () {
    document.getElementById("deleteMovieId").value = $(this).attr('data-id');
});

$('#movieModal').on('hidden.bs.modal', function () {
    clearFields();
})

