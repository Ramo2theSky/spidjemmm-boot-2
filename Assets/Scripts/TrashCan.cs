using System;
using UnityEngine;

namespace Trash {
    public class TrashCan : MonoBehaviour {

        [SerializeField] private int maxCapacity = 10;
        [SerializeField] private int currentFilled;

        public event Action<int, int> OnFillCountChanged = delegate { };

        public void OnInsert() {
            currentFilled++;
            OnFillCountChanged(currentFilled, maxCapacity);
        }

    }

}