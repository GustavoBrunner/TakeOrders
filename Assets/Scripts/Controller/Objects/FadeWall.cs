using UnityEngine;


namespace Controller.Object
{
    public class FadeWall : MonoBehaviour
    {
        
        private Vector3 newScale;
        private Vector3 originalScale;

        private void Awake()
        {
            newScale = new Vector3(transform.localScale.x, 0.2f, transform.localScale.z);
            originalScale = this.transform.localScale;
        }
        public void FadeOut()
        {
            transform.localScale = newScale;
        }
        public void FadeIn()
        {
            transform.localScale = originalScale;
        }

    }
}