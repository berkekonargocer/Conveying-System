using System.Collections.Generic;
using UnityEngine;

namespace NOJUMPO.Utils
{
    public static class NJWait
    {
        // -------------------------------- FIELDS ---------------------------------
        static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public static WaitForSeconds ForSeconds(float time) {
            if (time < 1f / Application.targetFrameRate)
                return null;

            if (WaitDictionary.TryGetValue(time, out var wait))
                return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }
    }
}