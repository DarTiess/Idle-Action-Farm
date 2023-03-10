using DG.Tweening;
using UnityEngine;

public class GrassGrounding : MonoBehaviour
{
    [SerializeField] private float _maxSize;
    [SerializeField] private float _growDuration;
    [SerializeField] private GrassPlane _plane;

    private float _startSize;
    private GrassSlicer _slicer;
    private CapsuleCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _startSize = gameObject.transform.localScale.y;
        _collider = GetComponent<CapsuleCollider>();
        _slicer = GetComponent<GrassSlicer>();
        GroundingGrass();
    }
    private void GroundingGrass()
    {
        _collider.enabled = false;
        _plane.gameObject.SetActive(false);
        _slicer.enabled = false;

        transform.DOScaleY(_maxSize, _growDuration)
            .OnComplete(() =>
            {
                _collider.enabled = true;

                _slicer.enabled = true;
                _plane.gameObject.SetActive(true);
                transform.DOKill();
            });
    }

    public void StartGroundAgain()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, _startSize, gameObject.transform.localScale.z);

        GroundingGrass();
    }
}
