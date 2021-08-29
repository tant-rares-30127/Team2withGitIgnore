$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

    connection.start().then(function () {
        console.log("signalr connected");
    });

    connection.on("LibraryResourceAdded", function (id, name, recommandation, url){
        createNewLibrary(id, name, recommandation, url);
        console.log(`library added:"${name}"`);
    })

    connection.on("LibraryResourceDeleted", function(id){
        deleteLibrary(id);
        console.log(`library deleted`);
    })

    connection.on("LibraryResourceUpdated", function (id, name, recommandation, url){
        updateLibrary(id, name, recommandation, url);
        console.log(`library updated`);
    })
});

function createNewLibrary(id, name, description, url) {
    $("#libraryTableBodyId").append(`<tr library-id="${id}">
                <td library-name="${id}">
                    ${name}
                </td>
                <td library-recommandation="${id}">
                    ${recommandation}
                </td>
                <td library-url="${id}">
                    <a href="${url}">${url}</a>
                </td>
                <td>
                    <a href="LibraryResources/Edit/${id}">Edit</a> |
                    <a href="LibraryResources/Details/${id}">Details</a> |
                    <a href="LibraryResources/Delete/${id}">Delete</a>
                </td>
            </tr>`);
}

function deleteLibrary(id) {
    $(`tr[library-id=${id}]`).remove();
}
function updateLibrary(id, name, recommandation, skillMatrixUrl) {
    $(`tr[library-id=${id}]`).find(".library-name").text(name);
    $(`tr[library-id=${id}]`).find(".library-recommandation").text(recommandation);
    $(`tr[library-id=${id}]`).find(".library-url").text(url);
}
