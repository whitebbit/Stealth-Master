using _3._Scripts.Noises.Interfaces;
using UnityEngine;

namespace _3._Scripts.Noises
{
    public static class NoiseEmitter
    {
        private static readonly Collider[] OverlapResults = new Collider[32];
        private static int _overlapResultsCount;

        public static void MakeNoise(Vector3 position, int layerIndex, float radius = 5)
        {
            var layerMask = 1 << layerIndex;
            _overlapResultsCount = Physics.OverlapSphereNonAlloc(position, radius, OverlapResults, layerMask);

            for (var i = 0; i < _overlapResultsCount; i++)
            {
                if (OverlapResults[i].TryGetComponent(out INoiseListener listener) == false) continue;
                listener.HearingNoise(position);
            }
        }
    }
}