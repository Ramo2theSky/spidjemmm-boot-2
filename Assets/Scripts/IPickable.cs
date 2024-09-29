using UnityEngine;

namespace Trash {
    public interface IPickable {
        void OnPick(Transform holdPoint);
        void OnThrow(Vector2 velocity);
    }

}