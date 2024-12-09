using UnityEngine;

public class RandomTerrainGenerator : MonoBehaviour
{
    public Terrain terrain; // Assign your terrain in the Inspector
    public float terrainDepth = 20.0f; // Vertical scale of the terrain
    public float scale = 50.0f; // Noise scale for gentle slopes
    public int dropOffCount = 10; // Number of steep drop-offs
    public float dropOffSize = 5.0f; // Radius of each drop-off area

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned! Please assign the terrain in the Inspector.");
            return;
        }

        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;

        // Get terrain dimensions
        int terrainWidth = terrainData.heightmapResolution;
        int terrainHeight = terrainData.heightmapResolution;

        // Update terrain size
        terrainData.size = new Vector3(terrainData.size.x, terrainDepth, terrainData.size.z);

        // Generate random heights
        float[,] heights = GenerateHeights(terrainWidth, terrainHeight);

        // Add steep drop-offs
        AddDropOffs(heights, terrainWidth, terrainHeight);

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
                // Generate height using Perlin noise
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;
                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord) * 0.1f; // Reduced amplitude
            }
        }

        return heights;
    }

    void AddDropOffs(float[,] heights, int width, int height)
    {
        for (int i = 0; i < dropOffCount; i++)
        {
            // Choose a random center point for the drop-off
            int centerX = Random.Range(0, width);
            int centerY = Random.Range(0, height);

            // Create a circular drop-off area
            for (int x = -Mathf.FloorToInt(dropOffSize); x <= Mathf.FloorToInt(dropOffSize); x++)
            {
                for (int y = -Mathf.FloorToInt(dropOffSize); y <= Mathf.FloorToInt(dropOffSize); y++)
                {
                    int posX = centerX + x;
                    int posY = centerY + y;

                    // Ensure we're within bounds
                    if (posX >= 0 && posX < width && posY >= 0 && posY < height)
                    {
                        float distance = Mathf.Sqrt(x * x + y * y);
                        if (distance <= dropOffSize)
                        {
                            // Gradually lower height based on distance from center
                            heights[posX, posY] *= Mathf.Clamp01(1.0f - (distance / dropOffSize));
                        }
                    }
                }
            }
        }
    }
}
