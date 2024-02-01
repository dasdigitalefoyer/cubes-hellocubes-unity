# HelloCubes - DemoApp for the cubes project

A simple tutorial app named HelloCubes is available with some examples of event handling (internal from cube backend if running or mocked by keyboard)

## Description
This app handles the orientation Event from CubeControl events sent by the backend. Additionally it receives MQTT-messages on *puzzlecubes/app/helloCubes* and responds to it on an own topic  *puzzlecubes/<CUBE_ID>/app/helloCubes*.

Move the cube (or press the right keys) to deactivate the spashscreen.

**HelloCubesEventDispatcher:** This script is derived from the base *EventDispatcher* class and subscribes to the mqtt event sent by the gamemaster app which is internally dispatched to a *HelloCubes*\-Unity-Event. A convenience function *DispatchHelloCubes* is added to be used by components to send out the individual *HelloCubes*\-Message to MQTT.

**HelloCubesAppState:** Inherits from AppState and adds orientation field.

**HelloCubesAppController:** This script inherits from base *AppController* handles the HelloCubes-Message (this method is connected through the editor to the *EventDispatcher*) by responding with its own Message calling *DispatchHelloCubes.*

It also listens to *CubeControl*\-Events with its *HandleCubeControl*\-method (also comnnected to EventDispatcher in Unity Editor). The orientation from *CubeControl* is extracted and sent to an *orientationEvent*. This can be used by other scripts (e.g. *WorldOrientationHandler*). When the *isMoving*\-flag is set in *CubeControl* the spashscreen is removed.

When the orientation differs more than 10 degrees from orientation stored in *appState* the state is set dirty so it gets sent out by MQTT. The *appState* is initialized in the initialize-function.

**HelloCubesKeyboardController:** The keyboard control script inherits from base *KeyboardController* and just introduces sending HelloCubes messages by Space-Bar.

## Getting started

we used Unity 2021.3 for devloping, neweer versions could require some modifications.

### Corresponding gamemaster app
[https://github.com/dasdigitalefoyer/cubes-hellocubes-web]

Follow the steps to getting the gamemaster app started together with a MQTT broker

### Setup
* open SampleScene
* Change the MQTT settings according to your infrastructure. The settings can be found in the scene under Controller->Communication.MQTTcommunication

### Run
* press space for sending out HelloCubes
* press E/Q/Y for mock MOVING event to get rid of the splash screen
* use LEFT/RIGHT arrows for rotating the world (simulates cube  orientation)
* press SPACE for sending out a Hello message to MQTT


### Use Backend
If the sensor hardware is accessible you can run the cube backend (https://github.com/dasdigitalefoyer/cubes-cube-backend) on your system to interact with the sensors and generate the events.