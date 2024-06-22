using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassLogic {

    public static float AdjustNewReading(float oldReading, float newReading) {
        while (Math.Abs(newReading - oldReading) > 180) {
            if (newReading > oldReading) {
                newReading -= 360;
            } else {
                newReading += 360;
            }
        }
        return newReading;
    }
    
}
