/* Tal Rastopchin
 * July 18, 2019
 * 
 * A collection of utility helper functions that I found I used all over the
 * place but are not specific to the problems I used them to solve
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script with helper functions. */
public static class Utils
{
    public static Vector4 Vec3ToPos(Vector3 vec3)
    {
        return new Vector4(vec3.x, vec3.y, vec3.z, 1);
    }

    public static Vector3 Vec4To3(Vector4 vec4)
    {
        return new Vector3(vec4.x, vec4.y, vec4.z);
    }


    /* Smooth smoothes our our input
     * 
     * Applies a sign preserving square function to joystick input. Assumes
     * input is within the domain of [-1, 1].
     */
    public static float SmoothAxisInput(float input)
    {
        float sign = (input > 0) ? 1 : -1;
        return sign * Mathf.Pow(.25f * input, 2);
    }

    public static void OrientCube(GameObject cube, Vector3 point1, Vector3 point2, Vector3 up, float radius)
    {
        cube.transform.position = Vector3.Lerp(point1, point2, .5f);
        cube.transform.rotation = Quaternion.LookRotation(up, (point2 - point1).normalized);
        cube.transform.localScale = new Vector3(radius, (point2 - point1).magnitude, radius);
    }

    /* OrientCylinder
     * 
     * Given a cylindrial GameObject, two points, and a radius, orients the
     * GameObject as if it were a 'rod' connecting the two points with the
     * desired radius.
     */
    public static void OrientCylinder(GameObject cylinder, Vector3 point1, Vector3 point2, float radius)
    {
        Vector3 midpoint = Vector3.Lerp(point1, point2, .5f);
        cylinder.transform.position = midpoint;
        cylinder.transform.up = (point2 - point1).normalized;
        cylinder.transform.localScale = new Vector3(radius, (point2 - point1).magnitude / 2, radius);

    }

    public static void OrientSphere(GameObject sphere, Vector3 position, float radius)
    {
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector3(radius*2, radius*2, radius*2);
    }

    public static void InstantiateCylinder(Vector3 point1, Vector3 point2, float radius)
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        OrientCylinder(cylinder, point1, point2, radius);
    }
}
