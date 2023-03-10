using UnityEngine;
using Zenject;

public class Melee : MonoBehaviour
{
    private PlayerBlockStack _playerStack;

    [Inject]
    private void Construct(PlayerBlockStack playerStack)
    {
        _playerStack = playerStack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grass"))
        {
            _playerStack.CreateBlockOfGrass(other.gameObject.transform);
        }
    }
}
