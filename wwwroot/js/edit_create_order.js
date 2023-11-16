
function sendData(url) {
    var allFieldsFilled = true;
    debugger
    try {
        //var localDate = 
        //date = new Date(localDate.getTime() + localDate.getTimezoneOffset())
        var formData = {
            Id: parseInt($("#AntiForgeryTokenIdinput").val()),
            Number: $("#Order_Number").val(),
            Date: new Date($("#Order_Date").val()),
            ProviderId: parseInt($("#SelectedProviderId").val()),
            Items: []
        };
        if (!formData.Number || isNaN(formData.Date.getTime()) || isNaN(formData.ProviderId)) {
            allFieldsFilled = false;
        }
        if (allFieldsFilled) {

            $("#ItemsTable tbody tr").each(function (index, element) {
                
                var itemData = {
                    Id: parseInt($(element).find("[id^='Id']").text()),
                    OrderId: parseInt($(element).find("[id^='Order_Id']").text()),
                    Name: $(element).find("[id^='Order_Name']").val(),
                    Quantity: parseFloat($(element).find("[id^='Order_Quantity']").val()),
                    Unit: $(element).find("[id^='Order_Unit']").val()
                };
                formData.Items.push(itemData);
                if (!itemData.Name || isNaN(itemData.Quantity) || !itemData.Unit) {
                        allFieldsFilled = false;
                        return false;              
                }
            });
        }
        
        if (!allFieldsFilled) {
            alert("All fields must be filled in")
            return false;
        }
        if (formData.Date.getFullYear() < 1900 || formData.Date>new Date()) {
            alert("The date must be no earlier than 1900.01.01 and no later than the current one")
            return false;
        }
        formData.Date = formData.Date.toJSON();
        
        
    } catch (e) {
        alert(e)
        return false;
    }
    
    console.log(formData);
    var token = $('input[name="__RequestVerificationToken"]').val();
    debugger;
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        headers: {
            __RequestVerificationToken: token,
            name: $("#AntiForgeryTokenIdinput").val()
        },
        success: function (response) {

            if (response.statuscode === 201) {
                alert("Success \n" + "Status: " + response.statuscode);
            } else {
                alert("Error \n" + "Status: " + response.statuscode + "\n" + response.description);
            }
        },
        error: function (response) {
            alert("Failed \n" + response.responseText);
        }
    });
}