// stuff i need to be on page asap
window.onload = function () {

    //setting year
    var x = document.getElementById("year");
    x.innerText = new Date().getFullYear();

    //setting months
    var monthName = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];
    var months = document.getElementById("months");
    for (var i = 0; i < 12; i++) {
        var x = document.createElement("div");
        x.id = "month" + i + "Button";
        x.innerText = monthName[i];
        x.classList += "btn btn-warning month";
        months.appendChild(x);
    }
    //setting days for each month
    var days = document.getElementById("days");
    for (var v = 1; v <= 12; v++) {
        var m = document.createElement("div");
        m.id = "month" + v + "days";
        days.appendChild(m);
        for (var i = 1; i <= daysInMonth(new Date().getFullYear(), v); i++) {
            var x = document.createElement("div");
            x.id = "month" + v + "day" + i;
            x.innerText = i;
            x.classList += "btn btn-info day";
            m.appendChild(x);
        }
    }
}

function daysInMonth(year, month) {
    return new Date(year, month, 0).getDate();
}
