using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(float speed)
    {
        _animator.SetFloat("IsMove", speed);
    }

    public void CuttingAnimation()
    {
        _animator.SetBool("Cutting", true);
    }

    public void StopCuttingAnimation()
    {
        _animator.SetBool("Cutting", false);
    }
}
