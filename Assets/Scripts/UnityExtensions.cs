using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions {

    //Extension method to check if a layer is in a layermask
    //layerMask.Contains(layer)
    public static bool Contains(this LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
    }

    //Extension method to turn a Color32 into a Color
    public static Color ToColor(this Color32 col32) {
        return new Color(col32.r, col32.g, col32.b);    //rgb components are 0 to 1    
    }
}