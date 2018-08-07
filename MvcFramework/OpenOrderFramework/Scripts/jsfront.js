$(function() {

    $('.ove').on("click", function () {
        console.log($(this));
        console.log($(this).attr("data-id"));
        location.href = "../Movies/Getmovie/" + $(this).attr("data-id");
    });

});