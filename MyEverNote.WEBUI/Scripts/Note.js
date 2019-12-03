
$(function () {

    $('#modal_NoteText').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        noteid = btn.data("note-id");

        $("#modal_NoteText_body").load("/Notes/ShowNotes/" + noteid);
    })

});