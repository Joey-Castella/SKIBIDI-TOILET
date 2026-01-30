using UnityEngine;

public static class GlobalHelper
{
    public static string GenerateUniqueID(GameObject obj)
    {
        // Creates a unique name based on the scene and position (e.g., "Level1_5.5_2.0")
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}"; 
    }
}