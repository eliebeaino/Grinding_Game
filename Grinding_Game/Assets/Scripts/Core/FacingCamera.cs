using UnityEngine;

namespace GrindingGame.Core
{
    public class FacingCamera : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
