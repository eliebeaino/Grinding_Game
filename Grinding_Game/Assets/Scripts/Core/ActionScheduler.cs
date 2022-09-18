using UnityEngine;

namespace GrindingGame.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction = null;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                //Debug.Log(currentAction.ToString() + " was cancelled!");
                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
