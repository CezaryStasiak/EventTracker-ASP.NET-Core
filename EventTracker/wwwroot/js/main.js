// stuff i need to be on page asap
window.onload = function () {
    
    setMonthsListener();
    setDaysListener();
}

function setMonthsListener() {
    for (let i = 1; i <= 12; i++) {
        let month = document.getElementById("month" + i + "button");
        month.addEventListener("click", function () { hideDays(i) });
    }
    for (var i = 1; i <= 12; i++) {
        let d = document.getElementById("month" + i + "days");
        d.style.display = "none";
    }
}

function setDaysListener() {
    for (let v = 1; v <= 12; v++) {
        for (let i = 1; i <= daysInMonth(new Date().getFullYear(), v); i++) {
            let x = document.getElementById("month" + v + "day" + i + "button")
            x.addEventListener("click", function () { daysLink(document.getElementById("year").innerText, v, i) });
        }
    }
}

function daysInMonth(year, month) {
    return new Date(year, month, 0).getDate();
}

var daysLink = function (year, month, day) {
    window.location.href = "/Events/Index/" + year + "-" + month + "-" + day;
}

// hide days based on month clicked
var hideDays = function(id) {
        for (var i = 1; i <= 12; i++) {
                let d = document.getElementById("month" + i + "days");
                d.style.display = "none";
        }
        let q = document.getElementById("month" + id + "days");
        q.style.display = "block";
}




