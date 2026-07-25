using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialOffsetScroller : MonoBehaviour
{
    [SerializeField]
    private Vector2 scrollVelocity;
    private MeshRenderer meshRenderer;
    private Material dupedMaterial;
    private int baseMapId = Shader.PropertyToID("_BaseMap");
    private Vector2 tiling;
    
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        dupedMaterial = new Material(meshRenderer.material);
        meshRenderer.material = dupedMaterial;
        tiling = dupedMaterial.GetTextureScale(baseMapId);
    }
    
    void Update()
    {
        dupedMaterial.SetTextureOffset(baseMapId, new Vector2(Mathf.Repeat(scrollVelocity.x * Time.time, tiling.x), Mathf.Repeat(scrollVelocity.y * Time.time , tiling.y)));
    }

    void OnDestroy()
    {
        Destroy(dupedMaterial);
    }
}
