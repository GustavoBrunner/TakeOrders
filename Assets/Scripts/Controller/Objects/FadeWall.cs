using UnityEngine;


namespace Controller.Object
{
    public class FadeWall : MonoBehaviour
    {
        private Color initialColor;
        private Color transparentColor;
        [SerializeField]
        private Renderer render;
        [SerializeField]
        private Material material;
        private Vector3 newScale;
        private Vector3 originalScale;
        private MeshRenderer mRender;
        public float ColorAlpha;
        private void Awake()
        {
            newScale = new Vector3(transform.localScale.x, 0.2f, transform.localScale.z);
            originalScale = this.transform.localScale;
            mRender = GetComponent<MeshRenderer>();
            material = mRender.material;
            initialColor = this.mRender.material.color;
            transparentColor = this.initialColor;
            

            TransparencyMaterials(this.material.name);
        }
        public void FadeOut()
        {
            //transform.localScale = newScale;
            this.material.color = transparentColor;
            //Debug.Log("Desligando parede");
        }
        public void FadeIn()
        {
            //transform.localScale = originalScale;
            this.material.color = initialColor;
            //Debug.Log("ligando parede");
        }
        private void TransparencyMaterials(string matName)
        {
            switch (matName)
            {
                case "Paredes (Instance)":
                    this.transparentColor.a = 0.4f;
                    this.ColorAlpha = this.transparentColor.a;
                    break;
                case "Textura Parede Cozinha (Instance)":
                    this.transparentColor.a = 0.1f;
                    this.ColorAlpha = this.transparentColor.a;
                    break;
                default:
                    break;
            }
        }
    }
}