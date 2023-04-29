using UnityEditor;
using UnityEngine;

namespace UnityCraft.UI.Game.Health
{
    public class BaseBar : MonoBehaviour
    {
        [SerializeField]
        protected BaseContainer ContainerPrefab;
        [Header("Container sprites")]
        public Sprite Container;
        public Sprite ContainerDamage;
        public Sprite FillingFull;
        public Sprite FillingFullDamage;
        public Sprite FillingHalf;
        public Sprite FillingHalfDamage;
        public byte Max = 20;
        public BaseContainer[] Containers;
        public byte lastValue { get; private set; } = 20;
        protected BaseHealthBarHurtAnimation BarHurtAnimation = null;

        public void Subtract(byte value)
        {
            int newValue = Mathf.Clamp(lastValue - value, 0, Max);
            UpdateBar((byte)(newValue));
        }
         
        public void Add(byte value)
        {
            int newValue = Mathf.Clamp(lastValue + value, 0, Max);
            UpdateBar((byte)(newValue));
        }

        public void Set(byte value)
        {
            DestroyBar();
            CreateBar(Max);

            int newValue = Mathf.Clamp(value, 0, Max);
            lastValue = value;
            UpdateBar((byte)newValue, animate: false);
        }

        /// <summary>
        /// Creates a bar of Max/2 sprites.
        /// </summary>
        public virtual void CreateBar(byte value)
        {
            lastValue = value;
            Containers = new BaseContainer[Max/2];
            for (int i = 0; i < Max / 2; i++)
            {
                Containers[i] = Instantiate(ContainerPrefab, transform);
            }
        }

        /// <summary>
        /// Updates the bar sprites according to given value.
        /// </summary>
        /// <param name="value">New value for the bar.</param>
        public virtual void UpdateBar(byte value, bool animate = true)
        {
            if (lastValue > value && animate)
            {
                AnimateDepleting(lastValue, value);
            }
            lastValue = value;

            for (int i = 0; i < Max; i += 2)
            {
                if (TryGetFilling(i, value, in FillingFull, in FillingHalf, out var filling))
                {
                    Containers[i / 2].Show(filling);
                }
                else
                {
                    Containers[i / 2].HideFilling();
                }
            }
        }

        /// <summary>
        /// Updates the flashing sprites.
        /// </summary>
        /// <param name="value">Previous value of current bar.</param>
        public virtual void UpdateDamagedBar(byte value)
        {
            for (int i = 0; i < Max; i += 2)
            {
                if (TryGetFilling(i, value, in FillingFullDamage, in FillingHalfDamage, out var filling))
                {
                    Containers[i / 2].ShowDamaged(filling);
                }
                else
                {
                    Containers[i / 2].HideDamaged();
                }
            }
        }

        /// <summary>
        /// Calls an animation of the bar depleting.
        /// </summary>
        /// <param name="lastValue">Previous value.</param>
        /// <param name="value">Current value.</param>
        public virtual void AnimateDepleting(byte lastValue, byte value)
        {
            if (BarHurtAnimation is not null)
            {
                StartCoroutine(BarHurtAnimation.Animate(this, lastValue, value));
            }
        }

        /// <summary>
        /// Removes all containers from this bar.
        /// </summary>
        public virtual void DestroyBar()
        {
            if (Containers is not null && Containers.Length != 0)
            {
                for (int i = 0; i < Max / 2; i++)
                {
                    if (Containers[i].gameObject is not null)
                    {
                        Destroy(Containers[i].gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the if the filling should be in it's half state, full state or not render at all.
        /// </summary>
        /// <param name="index">Current iteration index</param>
        /// <param name="value">Current value of given stat</param>
        /// <param name="filling">A sprite to render, null if the function returns false.</param>
        /// <returns>A bool determining if the container filling should be rendered.</returns>
        protected bool TryGetFilling(int index, int value, in Sprite full, in Sprite half, out Sprite filling)
        {
            filling = null;

            if ((index >= value + 1 && value > 0) || value == 0 || index == value)
            {
                return false;
            }

            filling = value % 2 == 1 && index >= value - 1 ? half : full;
            return true;
        }
    }
}