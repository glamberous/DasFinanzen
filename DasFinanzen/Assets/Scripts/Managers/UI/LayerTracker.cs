using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerTracker {
    private static float ZFloat = 0.00f;

    public static float Increment() {
        return ZFloat -= 100f;
    }

    public static float Decrement() {
        return ZFloat += 100f;
    }
}
