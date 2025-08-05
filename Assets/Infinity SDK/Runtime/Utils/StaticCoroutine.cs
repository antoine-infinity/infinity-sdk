using System;
using System.Collections;
using UnityEngine;

namespace Infinity.Runtime.Utils
{
    public static class StaticCoroutine {
        private static StaticCoroutineRunner runner;

        public static Coroutine Start(IEnumerator coroutine) {
            EnsureRunner();
            return runner.StartCoroutine(coroutine);
        }

        public static Coroutine WaitForFrame(Action callback)
        {
            EnsureRunner();
            return runner.StartCoroutine(WaitForFrameRoutine());

            IEnumerator WaitForFrameRoutine()
            {
                yield return new WaitForEndOfFrame();
                callback?.Invoke();
            }
        }
        
        public static Coroutine WaitForSeconds(float seconds, Action callback)
        {
            EnsureRunner();
            return runner.StartCoroutine(WaitForFrameRoutine());

            IEnumerator WaitForFrameRoutine()
            {
                yield return new WaitForSeconds(seconds);
                callback?.Invoke();
            }
        }
        
        public static void Stop(Coroutine coroutine)
        {
            EnsureRunner();
            runner.StopCoroutine(coroutine);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static void EnsureRunner()
        {
            if (runner) return;
            
            runner = new GameObject("[Infinity] Static Coroutine Runner")
                .AddComponent<StaticCoroutineRunner>();
            runner.gameObject.AddComponent<DontDestroyOnLoad>();
        }

        private class StaticCoroutineRunner : MonoBehaviour {}
    }
}