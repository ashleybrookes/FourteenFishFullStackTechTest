﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQuery(document).ready(function($) {
    $(".clickable-row").click(function() {
        window.location = $(this).data("href");
    });
});

function IsInt(value) {
    if (isNaN(value)) {
        return false;
    }
    return Number.isInteger(value);
} 

function DeleteSpeciality(Id) {
    if (!Id) {
        return false;
    }
    if (!IsInt(Id)) {
        return false;
    }
    if (Id <= 0) {
        return false;
    }

    if (confirm("Are you sure you want to delete this ?")) {
        //call the http delete url which will delete the specialty and personspeciality data
        var xhr = new XMLHttpRequest();
        xhr.open("DELETE", "/Speciality/Delete/" + Id);
        xhr.onload = function () {
            if (xhr.status === 200) {
                // Re-direct to speciality home page
                window.location.replace("/Speciality");
            } else {
                alert("Delete failed");
            }
        };
        xhr.send();
    }
    
};

