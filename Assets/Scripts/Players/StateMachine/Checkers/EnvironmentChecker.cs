using UnityEngine;

namespace Players.StateMachine.Checkers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class EnvironmentChecker : MonoBehaviour
    {
        private const string _groundLayer = "Ground";

        [SerializeField] protected PlayerInfo PlayerInfo;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(_groundLayer))
                SetStatus(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(_groundLayer))
                SetStatus(false);
        }

        protected abstract void SetStatus(bool value);
    }
}