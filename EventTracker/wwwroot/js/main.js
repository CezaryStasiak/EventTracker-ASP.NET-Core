// stuff i need to be on page asap
window.onload = function () {

    //setting year
    getYear();
    //setting days for each month
    setDays();
    //setting months
    setMonths();
    
}

function getYear() {
    let x = document.getElementById("year");
    x.innerText = new Date().getFullYear();
}
function setDays() {
    var days = document.getElementById("days");
    for (var v = 1; v <= 12; v++) {
        let m = document.createElement("div");
        m.id = "month" + v + "days";
        days.appendChild(m);
        for (var i = 1; i <= daysInMonth(new Date().getFullYear(), v); i++) {
            let x = document.createElement("div");
            x.id = "month" + v + "day" + i + "button";
            x.innerText = i;
            x.classList += "btn btn-info day";
            m.appendChild(x);
        }
    }
}

function setMonths() {
    var monthName = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];
    var months = document.getElementById("months");
    for (let i = 1; i <= 12; i++) {
        let x = document.createElement("div");
        x.id = "month" + i + "button";
        x.innerText = monthName[i-1];
        x.classList += "btn btn-warning month";
        x.addEventListener("click", function () { hideDays(i) } );
        months.appendChild(x);
    }

    for (var i = 1; i <= 12; i++) {
        let d = document.getElementById("month" + i + "days");
        d.style.display = "none";
    }
}

function daysInMonth(year, month) {
    return new Date(year, month, 0).getDate();
}

var hideDays = function(id) {
        for (var i = 1; i <= 12; i++) {
                let d = document.getElementById("month" + i + "days");
                d.style.display = "none";
        }
        let q = document.getElementById("month" + id + "days");
        q.style.display = "block";
}




