# Unity_accelerometer_monobehaviour_module
This is a C# implementation of a simple accelerometer as monobehaviour.

To use it, simply save the .cs code to the asset directory and drag it to the inspector window of an object. Then the accelerometer is attached to the object.

This code is based on codes from this tutorial: https://discussions.unity.com/t/best-script-to-get-acceleration-from-a-gameobject/855558 It has been developed to a imidiately useable monobehaviour.

This accelerometer base purely on the transform of the game object which means it cannot detect gravity while the object is stationary.
