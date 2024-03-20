using System;
using System.Collections;
using _3._Scripts.Detectors;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Money
{
    public class MoneyMagnet : MonoBehaviour
    {
        [SerializeField] private float attractDuration = 1f;
        [SerializeField] private float followOffsetDistance = 5f;
        [SerializeField] private float speed = 5;
        [SerializeField] private float scaleReduceDuration = 0.5f;
        [SerializeField] private float scaleReduceMoveSpeed = 5;
        [SerializeField] private float takeShakeDuration = 0.2f;

        private float FollowRange => followOffsetDistance * followOffsetDistance;

        private void Awake()
        {
            var detector = GetComponent<BaseDetector<Money>>();
            detector.OnFound += Attract;
        }

        public void AttractLinear(Money money)
        {
            money.transform.DOComplete(true);

            money.transform.DOLocalRotate(Vector3.zero, 0.05f);
            money.transform.DOLocalMove(transform.position, 0.05f).OnComplete(() =>
            {
                money.Take();
                Destroy(money.gameObject);
            });
        }

        public void Attract(Money money)
        {
            money.DisableGravity();
            money.DisableCollision();
            StartCoroutine(Animate(money));
        }

        private IEnumerator Animate(Money money)
        {
            var m = money.transform;
            m.DOComplete(true);
            m.DOShakeScale(takeShakeDuration, 2f);

            yield return new WaitForSeconds(takeShakeDuration);

            m.DOLocalRotate(Vector3.zero, attractDuration);

            while (Vector3.SqrMagnitude(transform.position - m.position) > FollowRange)
            {
                var clampedSpeed = Mathf.Clamp(speed * Time.deltaTime, 0, 1);
                m.position =
                    Vector3.Lerp(m.position, transform.position, clampedSpeed);

                yield return null;
            }

            m.DOScale(0, scaleReduceDuration).OnComplete(() =>
            {
                money.Take();
                m.DOComplete(true);
                Destroy(money.gameObject);
            });
            
            while (money != null && money.gameObject != null)
            {
                m.position = Vector3.MoveTowards(m.position, transform.position,
                    scaleReduceMoveSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}