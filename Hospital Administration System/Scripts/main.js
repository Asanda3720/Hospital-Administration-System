let bodyCover = document.getElementById("body__cover");
let sideBar = document.getElementById("sidebar");
let closeSidebarBtn = document.getElementById("sidebar__close__btn");
let openSidebarBtn = document.getElementById("openSidebar");


if (openSidebarBtn) {
    openSidebarBtn.addEventListener("click", () => {
        sideBar.classList.add("showSideBar");
        bodyCover.classList.add("display");
    })
}
if (closeSidebarBtn) {
    closeSidebarBtn.addEventListener('click', () => {
        removeSidebar();
    })
}
if (bodyCover) {
    bodyCover.addEventListener("click", () => {
        removeSidebar();
    })
}

function removeSidebar(){
    sideBar.classList.remove("showSideBar");
    bodyCover.classList.remove("display");
}