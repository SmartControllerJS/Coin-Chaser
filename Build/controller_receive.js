const simplePeer = new smartcontroller.NesSmartController("123456789");
simplePeer.createQrCode(
  "https://smartcontrollerjs.github.io/Controllers/nesController.html",
  "qrcode",
  150,
  150,
  "1"
);

//listen for new connections and log them in the console
simplePeer.on("connection", function (data) {
    console.log(data);
});

//a function that checks if player 1 is connected
//if yes then check the arrow keys to highlight the buttons
function processData() {
    if (simplePeer.controllerList[1]) {
        //store the controller to access its fields
        //the dictionary key is 1 because a player ID has been specified, otherwise the peer ID from smartphone will be used
        var controller = simplePeer.controllerList[1];
        var button_id = ["up", "down", "left", "right"];
        //check if up button is pressed, if yes change the background colour from gray to yellow
        //then do the same for the remaining arrows
        for (var id of button_id) {
        if (controller.buttons[id]) {
            document.getElementById(id).style.backgroundColor = "#e9ec06";
        } else {
            document.getElementById(id).style.backgroundColor = "#dddcdc";
        }
        }
    }
    requestAnimationFrame(processData);
}
  
processData();
  
  
