serviceUrl = "http://webservice.hoppe.online/api/Products";

// ================================
// FORMAT SINGLE ROW
// ================================
function formatContent(data) {
    var display = "Product ID: ";
    display += data.ProductID;
    display += " | Name: ";
    display += data.Name;
    display += " | Manufacturer: ";
    display += data.Mfg;
    display += " | Vendor: ";
    display += data.Vendor;
    display += " | Price: $";
    display += data.Price;

    return display;
}

// ================================
// FORMAT SEVERAL ROWS
// ================================
function parseAndShowContent(data) {
    var content = JSON.parse(data);

    var resultList = $('<p/>', {
        text: 'List of Products'
    });

    var list = $('<ul/>');

    for (var i = 0; i < content.length; i++) {
        var item = $('<li/>', {
            text: formatContent(content[i])
        });
        list.append(item);
    }

    resultList.append(list);
    resultList.attr('id', 'ListOfProdResults');

    $('#ListOfProdResults').replaceWith(resultList);
}

// ================================
// GET ALL PRODUCTS
// ================================
function getAllProducts() {
    $.ajax({
        type: 'GET',
        url: serviceUrl
    })
    .done(function (data) {
        parseAndShowContent(data);
    })
    .error(function (jqXHR, textStatus, errorThrown) {
        $('#ListOfProdResults').text(jqXHR.responseText || textStatus);
    });
}

// ================================
// FIND PRODUCT BY ID
// ================================
function findProductById(id) {
    $.ajax({
        type: 'GET',
        url: serviceUrl + "/" + id
    })
    .done(function (data) {
        var result = $('<p/>');
        result.append(formatContent(data));
        result.attr('id', 'ListOfProdResults');

        $('#ListOfProdResults').replaceWith(result);
    })
    .error(function (jqXHR, textStatus, errorThrown) {
        $('#ListOfProdResults').text(jqXHR.responseText || textStatus);
    });
}

// ================================
// UPDATE PRODUCT BY ID
// ================================
function updateProductById(id, newPrice) {
    var productJson = {
        ProductID: id,
        Name: "",
        Mfg: "",
        Vendor: "",
        Price: newPrice
    };

    $.ajax({
        type: 'PUT',
        url: serviceUrl + "/" + id,
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(productJson)
    })
    .done(function (data) {
        var result = $('<p/>');
        result.append("Product #" + id + " was updated.");
        result.attr('id', 'ListOfProdResults');

        $('#ListOfProdResults').replaceWith(result);
    })
    .error(function (jqXHR, textStatus, errorThrown) {
        $('#ListOfProdResults').text(jqXHR.responseText || textStatus);
    });
}

// ================================
// DELETE PRODUCT BY ID
// ================================
function deleteProductById(id) {
    $.ajax({
        type: 'DELETE',
        url: serviceUrl + "/" + id
    })
    .done(function (data) {
        var result = $('<p/>');
        result.append("Product #" + id + " was removed.");
        result.attr('id', 'ListOfProdResults');

        $('#ListOfProdResults').replaceWith(result);
    })
    .error(function (jqXHR, textStatus, errorThrown) {
        $('#ListOfProdResults').text(jqXHR.responseText || textStatus);
    });
}



// ================================
$(function () {
    $('#BtnGetProducts').click(getAllProducts);

    $('#BtnProductSearch').click(function () {
        var idToSearch = $('#TextBoxProductSearch').val();
        findProductById(idToSearch);
    });

    $('#BtnProductUpdate').click(function () {
        var idToUpdate = $('#TextBoxProductUpdate_ID').val();
        var newPrice = $('#TextBoxProductUpdate_Price').val();
        updateProductById(idToUpdate, newPrice);
    });

    $('#BtnProductDelete').click(function () {
        var idToDelete = $('#TextBoxProductDelete').val();
        deleteProductById(idToDelete);
    });


});