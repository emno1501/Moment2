"use strict";

// Fälla upp och ned valmöjlighet av restaurang
let settingIcon = document.getElementById("citySetting");
let cityForm = document.getElementById("cityForm");
let cityDisplay = document.getElementById("chooseRestaurant");
settingIcon.addEventListener("click", changeCity, false);

function changeCity() {
    if (cityForm.className == "hiddenSetting") {
        cityForm.className = "showSetting";
        cityDisplay.className = "cityDown";
    } else {
        cityForm.className = "hiddenSetting";
        cityDisplay.className = "cityUp";
    }
}

//Fälla upp och ned mobilmeny
let navigation = document.getElementById("navigation");
let hamburgermenu = document.getElementById("hamburger");
hamburgermenu.addEventListener("click", showMobileMenu, false);

function showMobileMenu() {
    if (navigation.className == "defaultmenu") {
        navigation.className = "showmenu";
    } else {
        navigation.className = "defaultmenu";
    }
}