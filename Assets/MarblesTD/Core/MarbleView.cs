using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.Core
{
    public class MarbleView : MonoBehaviour, IMarbleView
    {
        public Marble Marble { get; set; }
        
        public void DestroySelf()
        {
            Bootstrap.Instance.Marbles.Remove(Marble);
            Destroy(gameObject);
        }
        
        public void UpdatePosition(Vector2 newPosition)
        {
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.y);
        }

        private void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.TryGetComponent(out MarbleView view)) return;
            
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
        
    }
}