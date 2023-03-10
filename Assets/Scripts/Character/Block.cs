using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Rigidbody Rigidbody
    {
        get { return _rb = GetComponent<Rigidbody>(); }

    }
    private BoxCollider _collider;
    private HingeJoint _joint;
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _joint = GetComponent<HingeJoint>();
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public void InitBlockInScene()
    {
        _collider = GetComponent<BoxCollider>();
        _joint = GetComponent<HingeJoint>();
        _rb = GetComponent<Rigidbody>();
        _collider.isTrigger = false;
        Destroy(_joint);
        Destroy(_rb);
    }

    public void InitFirstBlockInStack(Transform _blockPlace, Rigidbody connected)
    {
        InitBlock(_blockPlace, connected);
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    public void InitBlockInStack(Transform _blockPlace, Rigidbody connected)
    {
        InitBlock(_blockPlace, connected);
        _rb = GetComponent<Rigidbody>();
        _rb.mass = 0.1f;
        _rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void InitBlock(Transform _blockPlace, Rigidbody connected)
    {
        _joint = GetComponent<HingeJoint>();
        _meshRenderer = GetComponent<MeshRenderer>();
        transform.parent = _blockPlace.transform;

        _joint.connectedBody = connected;
        _meshRenderer.enabled = false;
    }
    public void SaleBlockFromStack()
    {
         _meshRenderer.enabled=false;
    }
    public void MoveToTarget(Vector3 startPlace, Transform target, float jumpForce, float jumpDuretion)
    {
        transform.position = startPlace;
        transform.DOJump(target.position, jumpForce, 1, jumpDuretion)
      .OnComplete(() =>
      {
          transform.position = target.position;
          transform.rotation = target.rotation;
          transform.GetComponent<BoxCollider>().isTrigger = true;
      });
    }

}
