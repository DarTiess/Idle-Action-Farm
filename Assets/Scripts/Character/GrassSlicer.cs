using EzySlice;
using UnityEngine;
using Zenject;

public class GrassSlicer : MonoBehaviour
{
   	
	[SerializeField]private Material _crossMat;
	 [SerializeField]private GrassPlane _plane;

    private CapsuleCollider _collider;
   private GameObject _source;
 private GrassGrounding _grassGrounding;
  
    private void Start()
    {
        _collider= GetComponent<CapsuleCollider>();
        _grassGrounding= GetComponent<GrassGrounding>();
        PrepareSlicer();
    }
    public  void PrepareSlicer() {
		_source = gameObject;

		if (!_source.activeInHierarchy) {
			Debug.Log("Object is Hidden. Cannot Slice.");

			return;
		}

		if (_source.GetComponent<MeshFilter>() == null) {
			Debug.Log("GameObject must have a MeshFilter.");

			return;
		}
	}

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {           
             _collider.enabled=false;
             MakeSlice();
        }
    }
    private void MakeSlice()
    {
        SlicedHull hull =_plane.SliceObject(_source, _crossMat);

        if (hull != null)
        {
          GameObject lowerObj=  hull.CreateLowerHull(_source, _crossMat);
          GameObject upperObj=  hull.CreateUpperHull(_source, _crossMat);

            lowerObj.AddComponent<GrassPart>().Dissapearing(2f, _grassGrounding);
                  
            upperObj.AddComponent<GrassPart>().Dissapearing(2f, _grassGrounding);
            upperObj.AddComponent<MeshCollider>().convex=true;    
          Rigidbody rbUpper=  upperObj.AddComponent<Rigidbody>();
            rbUpper.constraints=RigidbodyConstraints.FreezePositionZ;

            _source.SetActive(false);
        }
    }

  
}
