using System.Collections;
using UnityEngine;

namespace UnityCraft.UI.Game.Health
{
    public abstract class BaseHealthBarHurtAnimation
    {
        /// <summary>
        /// A coroutine that animates the bar if it gets damaged.
        /// </summary>
        /// <param name="bar">Reference to the animated bar.</param>
        public abstract IEnumerator Animate(BaseBar bar, byte lastValue, byte value);
    }
}