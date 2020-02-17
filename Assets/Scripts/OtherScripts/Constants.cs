public static class Constants
{
    // Player
    public const int lives = 4;
    public const int superPower = 3;
    public const int multiplierInit = 1;
    public const float playerRespawnTime = 0.8f;

    // Multiplier
    public const int pointsForMultiplier2 = 1000;
    public const int pointsForMultiplier3 = 15000;
    public const int pointsForMultiplier4 = 25000;
    public const int pointsForMultiplier5 = 70000;

    // Enemies
    public enum enemies { GRUNT, WEAVER, BH }

    // Grunt
    public const float gruntSpeed = 3.1f;
    public const int gruntScore = 50;
    public const float gruntRespawnTime = 0.7f;
    public const float gruntSpawnChance = 50f;

    // Weaver
    public const float weaverSpeed = 2.4f;
    public const int weaverScore = 150;
    public const float weaverRespawnTime = 0.8f;
    public const float weaverSpawnChance = 20f;

    // Black Hole
    public const int BHlives = 4;
    public const int BHScore = 400;
    public const float BHRespawnTime = 1f;
    public const float BHSpawnChance = 800;
}
