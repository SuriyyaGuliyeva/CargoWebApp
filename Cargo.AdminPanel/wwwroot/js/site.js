// click on hamburger menu
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".sidebarBtn");

sidebarBtn.onclick = function () {

    sidebar.classList.toggle("active");

    if (sidebar.classList.contains("active"))
        sidebarBtn.classList.replace("bx-menu", "bx-menu-alt-right");
    else
        sidebarBtn.classList.replace("bx-menu-alt-right", "bx-menu");
}

//add/remove active class in side bar list
const currentLocation = location.href;

var btnContainer = document.getElementById("sideBarList");
var btns = btnContainer.getElementsByClassName("list-item");
var current = document.getElementsByClassName("active");

for (var i = 0; i <= btns.length; i++) {    
    if (currentLocation.startsWith(btns[i].href) || btns[i].href === currentLocation) {
        current[0].className = current[0].className.replace("active", "");

        btns[i].className = "active";
    }
}
