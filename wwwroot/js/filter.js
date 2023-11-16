document.addEventListener('DOMContentLoaded', function () {
    var submitBtn = document.getElementById('SubmitBtn');
   // var deleteBtn = document.getElementById('DeleteOrderBtn');
    submitBtn.addEventListener('click', function () {
        applyFilter()
    });
    $(".DeleteOrderBtn").click(function () {
        debugger
        var orderId = $(this).data('order-id');
        var row = $("#OrderId_" + orderId).closest('tr');
        
        var id = parseInt(orderId) 
        deleteOrder(id, row, "/Order/Delete")
        
    });
});
$(document).ajaxComplete(function () {
    console.log("ajax complete")
    $(".DeleteOrderBtn").click(function () {
        console.log("DeleteOrderBtn click!")
        var orderId = $(this).data('order-id');
        var row = $("#OrderId_" + orderId).closest('tr');

        var id = parseInt(orderId)
        deleteOrder(id, row, "/Order/Delete")

    });
});


function applyFilter() {
    var filterVM = null;
    var order_container = $("#OrdersContainer");
    //debugger
    filterVM = {
        OrderIds: Array.from(document.getElementById('SelectedOrderId').selectedOptions).map(option => parseInt(option.value)).filter(value => !isNaN(value)),
        ItemNames: Array.from(document.getElementById('SelectedItemName').selectedOptions).map(option => option.text).filter(text => !text.startsWith('-- Select ')),
        ItemUnits: Array.from(document.getElementById('SelectedItemUnit').selectedOptions).map(option => option.text).filter(text => !text.startsWith('-- Select ')),
        ProviderIds: Array.from(document.getElementById('SelectedProviderId').selectedOptions).map(option => parseInt(option.value)).filter(value => !isNaN(value)),
        StartDate: document.getElementById('StartDate').value,
        EndDate: document.getElementById('EndDate').value
    }
    
    $.ajax({
        url: 'Home/OrdersByFilters/',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(filterVM),
        success: function (response) {
           
            order_container.html(response)
         
        },
        error: function (response) {
            alert(response.responseText)
        }
        
        
        
    });
}
function deleteOrder(id, row, url) {
    debugger
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(id),
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response.success == true) {
                
                alert("Removal success");
                if (row) {
                    row.remove();
                }
            }
            else {
                alert("Removal failed\n" + response.statuscode + "\n" + response.description)
            }
            
        },
        error: function (response) {
            alert("Removal failed \n" + response.status);
        }
    });
}