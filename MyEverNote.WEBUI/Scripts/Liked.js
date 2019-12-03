
$(function () {

    var nodeids = [];


    $("div[data-note-id]").each(function (i, e) {


        nodeids.push($(e).data("note-id"));
    });

    $.ajax({

        method: "POST",
        url: "/Notes/GetLiked",
        data: { ids: nodeids }
    }).done(function (data) {
        if (data.result != null && data.result.length > 0) {


            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i];
                var likedNote = $("div[data-note-id=" + id + "]");
                var btn = likedNote.find("button[data-liked]");
                var span = btn.find("span.like-star");


                btn.data("liked", true);
                span.removeClass("glyphicon-star-empty");
                span.addClass("glyphicon-star");

            }
        }

    }).fail(function () {

        alert("Beğeniler Yüklenemedi !!");
    });




    $("button[data-liked").click(function () {

        var btn = $(this);
        var liked = btn.data("liked");
        var noteid = btn.data("note-id");
        var spanstar = btn.find("span.like-star");
        var spanCount = btn.find("span.like-count");


        $.ajax({

            method: "POST",
            url: "/Notes/SetLikeState",
            data: { "noteid": noteid, "liked": !liked },

        }).done(function (data) {

            if (data.hassError) {

                alert(data.errormessage);

            }
            else {
                liked = !liked;
                btn.data("liked", liked);
                spanCount.text(data.result);


                spanstar.removeClass("glyphicon-star-empty");
                spanstar.addClass("glyphicon-star");


                if (liked) {
                    spanstar.addClass("glyphicon-star");
                } else {
                    spanstar.addClass("glyphicon-star-empty");
                }

            }


        }).fail(function () {

            alert("sisteme login olunmalı");
        });

    });
});
