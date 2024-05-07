using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace BusJam
{
    public class VisualPassenger : MonoBehaviour
    {
        public float           unitMoveTime = 0.2f;
        public List<Transform> targetTransforms;
        public bool            isClicked;
        
        private PColor     color;
        private (int, int) position;
        private Transform  myTransform;

        public void Initialize((int, int) pos, PColor c)
        {
            color       = c;
            position    = pos;
            isClicked   = false;
            myTransform = transform;
        }

        public void TestSelect()
        {
            if (isClicked) return;
            
            isClicked =  true;
            var positions = targetTransforms.Select(t => t.position).ToList();
            Travel(positions, transform);
        }

        /// <summary>
        /// Move the passenger input a 
        /// </summary>
        /// <param name="targetDestination"></param>
        /// <param name="t"></param>
        public void Travel(List<Vector3> targetDestination, Transform t)
        {
            if (targetDestination is not { Count: > 0 })
            {
                Debug.Log($"RedFlag invalid move pos for visual passengers");
                return;
            }
            
            var sequence = DOTween.Sequence();
            for (int i = 0; i < targetDestination.Count; i++)
            {
                sequence.Append(t.DOMove(targetDestination[i], unitMoveTime));
            }

            sequence.Play();
        }
    }
}
