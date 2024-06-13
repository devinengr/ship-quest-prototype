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

    // public static float OptimizeValueForAveraging(float big, float small) {
    //     // Averaging compass values can be finicky. If the user moves their phone such that
    //     // their phone points north briefly, compass values can alternate between 0 and 360.
    //     // The average of this is 180, but we want it to be 0.
    //     float bigAdjusted = big - 360;
    //     // Calculate the averages
    //     float avg = (big + small) / 2;
    //     float avgAdjusted = (bigAdjusted + small) / 2 + 360;
    //     // The average closer to one of the values is more sensible.
    //     // Get the distances between the averages and the values and compare which
    //     // distance is smallest.
    //     float distAvg1 = Mathf.Abs(avg - small);
    //     float distAvg2 = Mathf.Abs(avg - big);
    //     float distAvgAdjusted1 = Mathf.Abs(avgAdjusted - small);
    //     float distAvgAdjusted2 = Mathf.Abs(avgAdjusted - big);

    //     float minDistAvg = Mathf.Min(distAvg1, distAvg2);
    //     float minDistAvgAdjusted = Mathf.Min(distAvgAdjusted1, distAvgAdjusted2);

    //     if (minDistAvg < minDistAvgAdjusted) {
    //         // The values likely did not wrap past 360, so do not do anything.
    //         return small;
    //     } else {
    //         // The values likely wrapped past 360, so add 360 to the smaller value.
    //         return small + 360;
    //     }
    // }

    // public static float AdjustedAverage(float readingFirst, float readingSecond) {
    //     // Averaging compass values can be finicky. If the user moves their phone such that
    //     // their phone points north briefly, compass values can alternate between 0 and 360.
    //     // The average of this is 180, but we want it to be 0.
    //     float small = Mathf.Min(readingFirst, readingSecond);
    //     float big = Mathf.Max(readingFirst, readingSecond);
    //     float bigAdjusted = big - 360;
    //     // Calculate the averages
    //     float avg = (big + small) / 2;
    //     float avgAdjusted = (bigAdjusted + small) / 2 + 360;
    //     // The average closer to one of the values is more sensible.
    //     // Get the distances between the averages and the values and compare which
    //     // distance is smallest.
    //     float distAvg1 = Mathf.Abs(avg - small);
    //     float distAvg2 = Mathf.Abs(avg - big);
    //     float distAvgAdjusted1 = Mathf.Abs(avgAdjusted - small);
    //     float distAvgAdjusted2 = Mathf.Abs(avgAdjusted - big);

    //     float minDistAvg = Mathf.Min(distAvg1, distAvg2);
    //     float minDistAvgAdjusted = Mathf.Min(distAvgAdjusted1, distAvgAdjusted2);

    //     if (minDistAvg < minDistAvgAdjusted) {
    //         return avg;
    //     } else {
    //         return avgAdjusted;
    //     }
    // }
    
}
