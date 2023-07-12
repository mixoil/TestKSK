function changeMoneyUnitAvailability(denomination) {
    var data = {
        'Denomination': denomination
    };
    $('#' + denomination).toggleClass('active');
    $.ajax({
        type: "POST",
        url: '/update-unit-availability',
        headers: {
            "Content-Type": "application/json; odata=verbose"
        },
        data: JSON.stringify(data),
        processData: false,
    });
}