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


    //TODO setting constraints library?
    //blockRB.constraints = RigidbodyConstraints2D.None;








    /* IS BROKE
     * tolerance is how near a hue must be to the highest RGB hue to be reduced (from 0 to 1)
     * dullStrength is the percentage by which relevant hues will be reduced
     */
    private static Color DullHue(this SpriteRenderer initial, float tolerance, float dullStrength) {

        float[] hueValues = new float[3];   //colour mod is done in-place in this array
        hueValues[0] = initial.color.r;
        hueValues[1] = initial.color.g;
        hueValues[2] = initial.color.b;

        float maxHue = Mathf.Max(hueValues);

        for (int i = 0; i < 3; i++) {
            float offsetFromMaxHue = Mathf.Abs(maxHue - hueValues[i]);
            if (offsetFromMaxHue <= tolerance) {
                hueValues[i] = (hueValues[i] * (1 - dullStrength));     //if the hue is within a certain range of the highest hue, dull it
            }
        }

        return new Color(hueValues[0], hueValues[1], hueValues[2]);
    }
}