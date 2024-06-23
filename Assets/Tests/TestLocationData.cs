using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestLocationData {

    [Test]
    public void testMatches() {
        Location loc1 = new Location("loc1", 40.05f, 70.02f, 0f);
        Location loc2 = new Location("loc2", 40.05f, 70.02f, 0f);
        Location loc3 = new Location("loc3", 40.06f, 70.02f, 0f);
        Location loc4 = new Location("loc4", 40.05f, 70.03f, 0f);
        Location loc5 = new Location("loc5", 40.05f, 70.03f, 0.001f);
        Location loc6 = new Location("loc6", 40.0000005f, 70.02f, 0f);
        Location loc7 = new Location("loc7", 40.0000001f, 70.02f, 0f);
        // test different cases
        Assert.AreEqual(true, LocationLogic.CompareLatLon(loc1, loc2));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc1, loc3));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc1, loc4));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc3, loc4));
        Assert.AreEqual(true, LocationLogic.CompareLatLon(loc4, loc5));
        // test delta
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc1, loc6));
        Assert.AreEqual(true, LocationLogic.CompareLatLon(loc6, loc7));
        // test parameters swapped
        Assert.AreEqual(true, LocationLogic.CompareLatLon(loc2, loc1));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc3, loc1));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc4, loc1));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc4, loc3));
        Assert.AreEqual(true, LocationLogic.CompareLatLon(loc5, loc4));
        Assert.AreEqual(false, LocationLogic.CompareLatLon(loc6, loc1));
        Assert.AreEqual(true, LocationLogic.CompareLatLon(loc7, loc6));
    }

}
