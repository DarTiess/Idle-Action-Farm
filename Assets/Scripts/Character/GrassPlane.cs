using EzySlice;
using UnityEngine;

public class GrassPlane : MonoBehaviour
{

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

#if UNITY_EDITOR

    public void OnDrawGizmos()
    {
        EzySlice.Plane cuttingPlane = new EzySlice.Plane();
        cuttingPlane.Compute(transform);
        cuttingPlane.OnDebugDraw();
    }

#endif
}
