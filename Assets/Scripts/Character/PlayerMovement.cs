using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerCutting))]
[RequireComponent(typeof(PlayerBlockStack))]
[RequireComponent(typeof(PlayerMoneyStack))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Settings")] 
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _rotationSpeed;
    

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";   
    private NavMeshAgent _nav;
    private PlayerAnimator _animator;
  //  private Animator _anim;   
    private Vector3 _temp;
    private bool _isDead;

    private void Start()
    {       
      _animator = GetComponent<PlayerAnimator>();
        _nav = GetComponent<NavMeshAgent>();  
    }

    private void FixedUpdate()
    {
        if (_isDead) return;        
        Move();    
    }

    private void Move()
    {
        float inputHorizontal = SimpleInput.GetAxis(HorizontalAxis);
        float inputVertical = SimpleInput.GetAxis(VerticalAxis);

        _temp.x = inputHorizontal;
        _temp.z = inputVertical;

    _animator.MoveAnimation(_temp.magnitude);

        _nav.Move(_temp * _playerSpeed * Time.fixedDeltaTime);

        Vector3 tempDirect = transform.position + Vector3.Normalize(_temp);
        Vector3 lookDirection = tempDirect - transform.position;
        if (lookDirection != Vector3.zero)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
        }
    }

   

}
