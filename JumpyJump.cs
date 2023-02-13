using UnityEngine;
using DG.Tweening;

public class JumpyJump : MonoBehaviour
{
    [SerializeField] float _jumpyRange;
    [SerializeField] float _jumpyTime;

    void Start()
    {
        transform.DOLocalMoveY(transform.position.y + _jumpyRange, _jumpyTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
