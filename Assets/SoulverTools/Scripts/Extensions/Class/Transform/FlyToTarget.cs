using System;
using System.Collections;
using UnityEngine;

namespace SoulverTools
{
    [Serializable]
    public class FlyToTarget
    {
        [SerializeField] private Transform transform;
        [SerializeField] private Transform target;
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private float delay = 0.1f;
        private MonoBehaviour _runner;

        private Coroutine _coroutine;
        private Vector3 _startPosition;

        public void Initialize(MonoBehaviour runner)
        {
            _runner = runner ? runner : throw new ArgumentNullException(nameof(runner));

            _startPosition = transform.position;
            transform.gameObject.SetActive(false);
        }

        /// <summary>
        /// Всегда перезапускает анимацию.
        /// Текущая анимация прерывается без завершения.
        /// </summary>
        public void Play(Action onArrived)
        {
            StopInternal();

            transform.gameObject.SetActive(true);

            _coroutine = _runner.StartCoroutine(
                FlyRoutine(transform.position, target.position, onArrived));
        }

        private IEnumerator FlyRoutine(Vector3 from, Vector3 to, Action onArrived)
        {
            if (delay > 0f)
                yield return new WaitForSeconds(delay);
            
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);

                transform.position = Vector3.Lerp(from, to, t);
                yield return null;
            }

            transform.position = to;
            Debug.Log("End");
            onArrived?.Invoke();
            Reset();
        }

        private void StopInternal()
        {
            if (_coroutine != null)
            {
                _runner.StopCoroutine(_coroutine);
                _coroutine = null;
            }

            Reset();
        }

        private void Reset()
        {
            transform.gameObject.SetActive(false);
            transform.position = _startPosition;
        }
    }
}
