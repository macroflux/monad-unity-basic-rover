using UnityEngine;

public class TerrainWallBuilder : MonoBehaviour
{
    public Terrain terrain; // Assign the terrain in the Inspector
    public float wallHeight = 5.0f; // Height of the walls
    public float wallThickness = 1.0f; // Thickness of the walls

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned! Please assign the terrain in the Inspector.");
            return;
        }

        CreateWallsAroundTerrain();
    }

    void CreateWallsAroundTerrain()
    {
        // Get the terrain size
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        // Create wall prefab
        GameObject wallPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallPrefab.name = "WallSegment";
        wallPrefab.GetComponent<MeshRenderer>().material.color = Color.gray; // Set wall color
        Destroy(wallPrefab.GetComponent<Collider>()); // Remove individual collider (we'll use BoxColliders for the walls)

        // Create walls
        CreateWall(new Vector3(terrainSize.x / 2, wallHeight / 2, -wallThickness / 2), terrainSize.x, wallHeight); // Front wall
        CreateWall(new Vector3(terrainSize.x / 2, wallHeight / 2, terrainSize.z + wallThickness / 2), terrainSize.x, wallHeight); // Back wall
        CreateWall(new Vector3(-wallThickness / 2, wallHeight / 2, terrainSize.z / 2), terrainSize.z, wallHeight, true); // Left wall
        CreateWall(new Vector3(terrainSize.x + wallThickness / 2, wallHeight / 2, terrainSize.z / 2), terrainSize.z, wallHeight, true); // Right wall

        // Destroy wall prefab to clean up
        Destroy(wallPrefab);
    }

    void CreateWall(Vector3 position, float length, float height, bool isVertical = false)
    {
        // Create a wall object
        GameObject wall = new GameObject("Wall");
        wall.transform.position = terrain.transform.position + position;

        // Add and configure the Box Collider
        BoxCollider collider = wall.AddComponent<BoxCollider>();
        if (isVertical)
        {
            collider.size = new Vector3(wallThickness, height, length);
        }
        else
        {
            collider.size = new Vector3(length, height, wallThickness);
        }

        // Add a Mesh Renderer for visualization
        MeshRenderer renderer = wall.AddComponent<MeshRenderer>();
        renderer.material.color = Color.gray;

        // Add a Mesh Filter and set the wall to a simple cube shape
        MeshFilter meshFilter = wall.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateCubeMesh(collider.size);
    }

    Mesh CreateCubeMesh(Vector3 size)
    {
        // Simple cube mesh for walls
        Mesh mesh = new Mesh();

        Vector3[] vertices = {
            new Vector3(-size.x / 2, -size.y / 2, -size.z / 2), // Bottom-back-left
            new Vector3(size.x / 2, -size.y / 2, -size.z / 2),  // Bottom-back-right
            new Vector3(size.x / 2, size.y / 2, -size.z / 2),   // Top-back-right
            new Vector3(-size.x / 2, size.y / 2, -size.z / 2),  // Top-back-left
            new Vector3(-size.x / 2, -size.y / 2, size.z / 2),  // Bottom-front-left
            new Vector3(size.x / 2, -size.y / 2, size.z / 2),   // Bottom-front-right
            new Vector3(size.x / 2, size.y / 2, size.z / 2),    // Top-front-right
            new Vector3(-size.x / 2, size.y / 2, size.z / 2)    // Top-front-left
        };

        int[] triangles = {
            // Back face
            0, 2, 1, 0, 3, 2,
            // Front face
            4, 5, 6, 4, 6, 7,
            // Left face
            0, 7, 3, 0, 4, 7,
            // Right face
            1, 2, 6, 1, 6, 5,
            // Top face
            3, 7, 6, 3, 6, 2,
            // Bottom face
            0, 1, 5, 0, 5, 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
