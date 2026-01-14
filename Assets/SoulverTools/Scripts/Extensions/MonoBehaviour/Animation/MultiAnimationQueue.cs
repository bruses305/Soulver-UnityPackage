using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulverTools.WorkData;

namespace SoulverTools
{
    public class MultiAnimationQueue : MonoBehaviour
    {
        private class AnimationTask
        {
            public readonly Animator animator;   // указываем аниматор объекта
            public readonly string animationName; // имя анимации
            public readonly float duration;

            public AnimationTask(Animator animator, string animationName, float duration)
            {
                this.animator =  animator;
                this.animationName = animationName;
                this.duration = duration;
            }
        }
        
        private readonly Queue<AnimationTask> _animationQueue = new Queue<AnimationTask>();
        
        public void AddAnimation(Animator animator, string animationName, float duration = 0f)
        {
            _animationQueue.Enqueue(new(animator,animationName,duration));
        }

        public void Play(float secondStart = 0f)
        {
            StartCoroutine(PlayAnimations(secondStart));
        }

        private IEnumerator PlayAnimations(float secondStart)
        {
            yield return new WaitForSeconds(secondStart);
            while (_animationQueue.Count > 0)
            {
                AnimationTask task = _animationQueue.Dequeue();
                yield return new WaitForSeconds(task.duration);

                // проигрываем анимацию у объекта
                task.animator.Play(task.animationName);

                // ждём пока закончится
                yield return new WaitForSeconds(UnityData.GetAnimationLength(task.animator, task.animationName));
            }
            Destroy(gameObject);
        }

        
    }
}