## Playing the Game
**Note:** During our implementation of the QR code, it was found that the university WiFi service "eduroam" blocked connections using the SmartController package due to its network security configurations. Instead, it is recommended to use mobile data or other WiFi services on campus to allow for connection establishment between the smartphone and display.

### Play Online
To play the game online simply navigate to <a href=https://smartcontrollerjs.github.io/Coin-Chaser/>https://smartcontrollerjs.github.io/Coin-Chaser/</a> and scan the displayed QR code with your smartphone. This will send you to a controller interface as shown below, where you can start to control your character using the SmartController!

![Alt Text](https://imgur.com/GTqxssg.gif)
### Play Locally
**Note:** The locally ran version may encounter WebRTC errors due to browser extensions. For this reason, please run the local game on Chrome rather than browsers like Firefox.
* Download and install <a href="https://unity.com/">Unity Hub</a>
* Install the WebGL Build Support module on the Unity Hub
* Download this repository and open the Game directory as a Unity project
* Navigate to the WebG "Resolution and Presentation" section of the "Project Settings" menu for the game
* Select the "WebsiteTemplate" option as the new WebGL Template
* Build and run the game as a WebGL build, replacing the Build directory with the new build
* Scan the QR code with your smartphone and get ready to play!
