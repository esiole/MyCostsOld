let triggeredForm;
$('.deleteButton').click(function () {
    triggeredForm = $(event.target).closest("form");
});
$('#modalOk').click(function () {
    triggeredForm.submit();
    triggeredForm = undefined;
});
