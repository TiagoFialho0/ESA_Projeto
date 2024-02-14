// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var toggleButtons = document.querySelectorAll('.toggle-details-btn');

    toggleButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var row = this.closest('.expandable-row');
            var hiddenRow = row.nextElementSibling;
            hiddenRow.classList.toggle('show-details');
        });
    });
});