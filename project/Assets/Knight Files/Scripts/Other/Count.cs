using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Count {
    private static float deltatime = 20;
    // Use this for initialization

    public static void setTime(float i) {
        deltatime = i;
    }

    public static float getTime() {
        return deltatime;
     } 
}
