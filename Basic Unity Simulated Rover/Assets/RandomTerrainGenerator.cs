using UnityEngine;

public class RandomTerrainGenerator : MonoBehaviour
{
    public Terrain terrain; // Assign your terrain in the Inspector
    public float terrainDepth = 20.0f; // Vertical scale of the terrain
    public float scale = 50.0f; // Noise scale for gentle slopes

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned! Please drag your Terrain object to the script in the Inspector.");
            return;
        }

        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;

        // Get terrain dimensions from TerrainData
        int terrainWidth = terrainData.heightmapResolution;
        int terrainHeight = terrainData.heightmapResolution;

        // Set the terrain size (optional: adjust vertical scale)
        terrainData.size = new Vector3(terrainData.size.x, terrainDepth, terrainData.size.z);

        // Generate and smooth random heights
        float[,] heights = GenerateHeights(terrainWidth, terrainHeight);
        SmoothHeights(heights, terrainWidth, terrainHeight);

        // Apply heights to the terrain
        terrainData.SetHeights(0, 0, heights);
    }

    float[,] GenerateHeights(int width, int height)
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Generate height using scaled Perlin noise
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;
                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord) * 0.1f; // Reduced amplitude
            }
        }

        return heights;
    }

    void SmoothHeights(float[,] heights, int width, int height)
    {
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                // Average the height with its neighbors
                heights[x, y] = (heights[x, y] +
                                 heights[x - 1, y] +
                                 heights[x + 1, y] +
                                 heights[x, y - 1] +
                                 heights[x, y + 1]) / 5.0f;
            }
        }
    }
}
