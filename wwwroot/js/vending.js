function onMoneyUnitClick(denomination) {
    var data = {
        'Denomination': denomination
    };
    $.ajax({
        type: "POST",
        url: '/update-balance',
        headers: {
            "Content-Type": "application/json; odata=verbose"
        },
        data: JSON.stringify(data),
        processData: false,
    });
    var balanceContainer = $('#balance-display-container');
    var currentBalance = parseInt(balanceContainer.text());
    balanceContainer.text(denomination + currentBalance);
}