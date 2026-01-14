using System;
using System.Collections;
using UnityEngine;

namespace SoulverTools
{
    [Serializable]
    public class IntValueAnimator
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float duration;
        private MonoBehaviour _runner;

        private Coroutine _coroutine;
        private int _currentValue;

        public int CurrentValue => _currentValue;

        public void Initialize(MonoBehaviour runner, int initialValue = 0)
        {
            _runner = runner ? runner
                : throw new ArgumentNullException(nameof(runner));

            this.curve = curve ?? AnimationCurve.Linear(0, 1, 1, 1);
            this._currentValue = initialValue;
        }

        
        public void Animate(int targetValue, Action<int> onValueChanged, float newDuration, int newCurrentValue)
        {
            // если анимация уже шла — остановить
            if (_coroutine != null)
            {
                _runner.StopCoroutine(_coroutine);
                _coroutine = null;
            }

            // текущее значение уже финальное для предыдущей анимации
            _coroutine = _runner.StartCoroutine(
                AnimateRoutine(
                    from: _currentValue,
                    to: targetValue,
                    newDuration: newDuration,
                    onValueChanged: onValueChanged));
        }

        public void Animate(int targetValue, Action<int> onValueChanged) => 
            Animate(targetValue, onValueChanged, this.duration , CurrentValue);

        public void Stop()
        {
            if (_coroutine != null)
            {
                _runner.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator AnimateRoutine(int from, int to, float newDuration, Action<int> onValueChanged)
        {
            int steps = Mathf.Abs(to - from);

            if (steps == 0)
            {
                _currentValue = to;
                onValueChanged?.Invoke(_currentValue);
                yield break;
            }

            int direction = to > from ? 1 : -1;
            float[] stepTimes = CalculateStepTimes(steps, newDuration);

            _currentValue = from;
            onValueChanged?.Invoke(_currentValue);

            for (int i = 0; i < steps; i++)
            {
                yield return new WaitForSeconds(stepTimes[i]);

                _currentValue += direction;
                onValueChanged?.Invoke(_currentValue);
            }

            _coroutine = null;
        }

        private float[] CalculateStepTimes(int steps, float newDuration)
        {
            float[] times = new float[steps];
            float totalWeight = 0f;

            for (int i = 0; i < steps; i++)
            {
                float t = steps == 1 ? 1f : (float)i / (steps - 1);
                float weight = Mathf.Max(0.0001f, curve.Evaluate(t));
                times[i] = weight;
                totalWeight += weight;
            }

            float multiplier = newDuration / totalWeight;

            for (int i = 0; i < steps; i++)
                times[i] *= multiplier;

            return times;
        }
    }
}
