using System;
using System.Collections;
using UnityEngine;
using SoulverTools.WorkData;

namespace SoulverTools
{
    public class AnimationEndEvent : MonoBehaviour
    {
        private Animator _animator; // указываем аниматор объекта
        private string _animationName; // имя анимации
        private Action _callback; // обратный вызов


        public static void CreateAnimationPoint(Animator animator, string animationName, Action callback,
            bool play = true, float delay = 0f)
        {
            AnimationEndEvent animationEndEvent = new GameObject().AddComponent<AnimationEndEvent>();
            animationEndEvent._animator = animator;
            animationEndEvent._animationName = animationName;
            animationEndEvent._callback = callback;
            animationEndEvent.StartCoroutine(animationEndEvent.PlayAnimation(play, delay));
        }

        public static void CreateAnimationPoint(Animator animator, AnimationClip animationName, Action callback,
            bool play = true, float delay = 0f)
        {
            CreateAnimationPoint(animator, animationName ? animationName.name : "", callback, play, delay);
        }

        private IEnumerator PlayAnimation(bool play = true, float delay = 0)
        {
            // проигрываем анимацию у объекта
            if (play && _animator)
                _animator.Play(_animationName);

            // ждём пока закончится
            if (delay <= 0)
                yield return new WaitForSeconds(UnityData.GetAnimationLength(_animator, _animationName));
            else
                yield return new WaitForSeconds(delay);
            _callback?.Invoke();

            Destroy(gameObject);
        }
    }
}