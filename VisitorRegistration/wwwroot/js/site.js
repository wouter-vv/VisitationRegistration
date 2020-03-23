// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


$.noConflict();
jQuery(document).ready(function ($) {
    $(function () {
        $('#SelectedDate').datepicker({ dateFormat: 'dd/mm/yy' });
    });
});

$('#ShowDatePicker').on('change',
    function (event) {
        var selectedDeliveryPoint = event.target.selectedOptions[0].textContent;
        var addressComponent = $('#SelectedDatePartial')[0];
        if (selectedDeliveryPoint === 'Kies een datum') {
            addressComponent.classList.remove('d-none');
            addressComponent.classList.add('d-block');
        } else {
            addressComponent.classList.remove('d-block');
            addressComponent.classList.add('d-none');
        }
    });

$(document).ready( function (event) {
    var selectedDeliveryPoint = $('#ShowDatePicker').selectedOptions[0].textContent;
        var addressComponent = $('#SelectedDatePartial')[0];
    if (selectedDeliveryPoint === 'Kies een datum') {
            addressComponent.classList.remove('d-none');
            addressComponent.classList.add('d-block');
        } else {
            addressComponent.classList.remove('d-block');
            addressComponent.classList.add('d-none');
        }
    });