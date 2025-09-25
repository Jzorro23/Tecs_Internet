import { Application } from './node_modules/@splinetool/runtime/build/runtime.js';
 
window.onload = function() {
    loadSpline();
  };

var res = 0;
var val1 = 0;
var val2 = 0;

function loadSpline(){
    const canvas = document.getElementById('spline');
    const spline = new Application(canvas);
    spline
        .load('https://prod.spline.design/PEchU5BMwjn8PYTf/scene.splinecode')
        .then(() => {
           
 
            console.log('This is a test integration between Spline and JS');
           
 
            spline.addEventListener('mouseDown', (e) => {
                [val1, val2] = [parseFloat(document.getElementById('valo1').value), parseFloat(document.getElementById('valo2').value)];
                if (e.target.name === 'SUM') {
                    //console.log('I have been clicked!  Sum');
                    //console.log(val2);
                    res = val1 + val2;
                    //console.log(res)
                }
                else if (e.target.name == 'RES') {
                    //console.log('I have been clicked!  Res');
                    res = val1 - val2
                }
                else if (e.target.name == 'MUL') {
                    res = val1 * val2
                }
                else if (e.target.name == "DIV") {
                    res = val1 / val2
                }
                var myModal = new bootstrap.Modal(document.getElementById("myModal"), {});
                document.getElementById('res').textContent = res
                myModal.show();
            });
        });
}
 
 