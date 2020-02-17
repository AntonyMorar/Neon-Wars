public static class Constants
{
    // Player
    public const int lives = 4;
    public const int superPower = 3;
    public const int multiplierInit = 1;
    public const float playerRespawnTime = 0.8f;

    // Multiplier
    public const int pointsForMultiplier2 = 1000;
    public const int pointsForMultiplier3 = 10000;
    public const int pointsForMultiplier4 = 20000;
    public const int pointsForMultiplier5 = 50000;

    // Enemies
    public enum enemies { GRUNT, WEAVER }

    // Grunt
    public const float gruntSpeed = 3f;
    public const int gruntScore = 50;
    public const float gruntRespawnTime = 0.7f;
    public const float gruntSpawnChance = 50f;

    // Weaver
    public const float weaverSpeed = 2f;
    public const int weaverScore = 150;
    public const float weaverRespawnTime = 0.8f;
    public const float weaverSpawnChance = 300f;
    public const float weaverlookRadius = 1.3f;

    // Spinner
}
