$(document).ready(function () {
    $("#addButton").click(function () {
        var newCourse = $("#addBar").val();


        $.ajax({
            url: "/Skills/AddingSkills",
            method: "POST",
            data: {
                "skillName": newCourse
            },
            success: function (result) {
                location.reload();
            }
        })
    });
});