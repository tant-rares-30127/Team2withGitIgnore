$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

    connection.start().then(function () {
        console.log("signalr connected");
    });

    connection.on("SkillAdded", function (id, name, description, skillMatrixUrl){
        createNewSkill(id, name, description, skillMatrixUrl);
        console.log(`skill added:"${name}"`);
    })

    connection.on("SkillDeleted", function(id){
        deleteSkill(id);
        console.log(`skill deleted`);
    })

    connection.on("SkillUpdated", function (id, name, description, skillMatrixUrl){
        updateSkill(id, name, description, skillMatrixUrl);
        console.log(`skill updated`);
    })
});

function createNewSkill(id, name, description, skillMatrixUrl) {
    $("#skillTableBodyId").append(`<tr skill-id="${id}">
                <td skill-name="${id}">
                    ${name}
                </td>
                <td skill-description="${id}">
                    ${description}
                </td>
                <td skill-skillMatrixUrl="${id}">
                    <a href="${skillMatrixUrl}">${skillMatrixUrl}</a>
                </td>
                <td>
                    <a href="Skills/Edit/${id}">Edit</a> |
                    <a href="Skills/Details/${id}">Details</a> |
                    <a href="Skills/Delete/${id}">Delete</a>
                </td>
            </tr>`);
}

function deleteSkill(id) {
    $(`tr[skill-id=${id}]`).remove();
}
function updateSkill(id, name, description, skillMatrixUrl) {
    $(`tr[skill-id=${id}]`).find(".skill-name").text(name);
    $(`tr[skill-id=${id}]`).find(".skill-description").text(description);
    $(`tr[skill-id=${id}]`).find(".skill-skillMatrixUrl").text(skillMatrixUrl);
}
