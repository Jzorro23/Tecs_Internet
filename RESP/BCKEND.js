var t1 = document.getElementById("T1OR");
var t2 = document.getElementById("T2OR");
var sub = document.getElementById("SOR");
var desc = document.getElementById("DESCOR");
var pr = document.getElementById("PRECIOOR");
const img = document.getElementById("imageS");

function update(t1n, t2n, subn, descn, prn){
    t1.innerText = t1n;
    t2.innerText = t2n;
    sub.innerText = subn;
    desc.innerText = descn;
    pr.innerText = prn;

    clear();
}

function clear(){
    document.getElementById("T1").value   = "";
    document.getElementById("T2").value   = "";
    document.getElementById("S1").value   = "";
    document.getElementById("DESC").value = "";
    document.getElementById("PRECIO").value = "";
}

function image(n){
    switch(n){
        case 1:
            img.src = 'images/controller.png'
            break
        case 2:
            img.src = 'images/hotdog.png'
            break
        case 3:
            img.src = 'images/storm.png'
            break
        case 4:
            img.src = 'images/led.png'
            break
        case 5:
            img.src = 'images/egg.png'
            break
    }
}

var num11, num12, num12;
var num21, num22, num23; 

function changeColor(){
    num11 = Math.random()*255;
    num12 = Math.random()*255;
    num13 = Math.random()*255;
    num21 = Math.random()*255;
    num22 = Math.random()*255;
    num23 = Math.random()*255;

    document.getElementById("colorBox").style.background = "linear-gradient(rgb(" + num11 + ", " + num12 + ", " + num13 + "), rgb(" + num21 + ", " + num22 + ", " + num23 + "))";
}