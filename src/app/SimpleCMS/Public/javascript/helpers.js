function deleteRecord(url, confirm_text) {
    if (!confirm(confirm_text)) return false;
    $.ajax({
        type: 'POST',
        url: url,
        success: refreshPage
    });
}
function refreshPage() {
    window.location.reload();
}