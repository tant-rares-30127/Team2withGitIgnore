$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

    connection.start().then(function () {
        console.log("signalr connected");
    });

    connection.on("InternAdded", function(id, name, birthDate, emailAddress){
        createNewIntern(id, name, birthDate, emailAddress);
        console.log(`intern added:"${name}"`);
    })

    connection.on("InternDeleted", function(id){
        deleteIntern(id);
        console.log(`intern deleted`);
    })

    connection.on("InternUpdated", function(id, name, birthDate, emailAddress){
        updateIntern(id, name, birthDate, emailAddress);
        console.log(`intern updated`);
    })
});

function createNewIntern(id, name, birthDate, emailAddress) {
    $("#internTableBodyId").append(`<tr intern-id="${id}">
                <td intern-name="${id}">
                    ${name}
                </td>
                <td intern-birthdate="${id}">
                    ${birthDate}
                </td>
                <td intern-emailaddress="${id}">
                    ${emailAddress}
                </td>
                <td>
                    <a href="Interns/Edit/${id}">Edit</a> |
                    <a href="Interns/Details/${id}">Details</a> |
                    <a href="Interns/Delete/${id}">Delete</a>
                </td>
            </tr>`);
}

function deleteIntern(id) {
    $(`tr[intern-id=${id}]`).remove();
}

function updateIntern(id, name, birthDate, emailAddress) {
    $(`tr[intern-id=${id}]`).find(".intern-name").text(name);
    $(`tr[intern-id=${id}]`).find(".intern-birthdate").text(birthDate);
    $(`tr[intern-id=${id}]`).find(".intern-emailaddress").text(emailAddress);
}
