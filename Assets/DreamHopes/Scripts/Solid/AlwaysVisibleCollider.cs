using UnityEngine;

[ExecuteInEditMode]
public class AlwaysVisibleCollider : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        // Obtém o BoxCollider anexado a este GameObject
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;

            // Desenha o cubo representando o BoxCollider
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }
    }
}
