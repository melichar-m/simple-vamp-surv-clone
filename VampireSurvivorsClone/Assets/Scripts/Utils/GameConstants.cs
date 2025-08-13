namespace VampireSurvivors.Utils
{
    public static class GameConstants
    {
        // Screen Settings
        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;
        public const int TARGET_FPS = 60;

        // Player Settings
        public const float PLAYER_BASE_SPEED = 5f;
        public const float PLAYER_BASE_HEALTH = 100f;
        public const float PLAYER_PICKUP_RADIUS = 2f;

        // Enemy Settings
        public const float ENEMY_BASE_SPEED = 2f;
        public const float ENEMY_BASE_HEALTH = 10f;
        public const float ENEMY_BASE_DAMAGE = 10f;

        // Spawning Settings
        public const float INITIAL_SPAWN_RATE = 2f; // enemies per second
        public const float SPAWN_DISTANCE = 10f; // spawn distance from player
        public const int MAX_ENEMIES = 200;

        // Experience Settings
        public const float BASE_EXP_VALUE = 1f;
        public const float EXP_PICKUP_SPEED = 10f;
        public const float LEVEL_UP_BASE = 10f;
        public const float LEVEL_UP_MULTIPLIER = 1.5f;

        // Weapon Settings
        public const float WEAPON_BASE_DAMAGE = 10f;
        public const float WEAPON_BASE_SPEED = 1f; // attacks per second
        public const int WEAPON_BASE_PROJECTILES = 1;

        // Game Rules
        public const float GAME_DURATION = 1800f; // 30 minutes in seconds
        public const float BOSS_SPAWN_INTERVAL = 300f; // Every 5 minutes

        // Visual Settings
        public const float PLAYER_VISUAL_SCALE = 0.3f;
        public const float ENEMY_VISUAL_SCALE = 0.4f;
        public const float PROJECTILE_VISUAL_SCALE = 0.1f;
        public const float PICKUP_VISUAL_SCALE = 0.15f;

        // Layer Names
        public const string LAYER_PLAYER = "Player";
        public const string LAYER_ENEMY = "Enemy";
        public const string LAYER_PROJECTILE = "Projectile";
        public const string LAYER_PICKUP = "Pickup";

        // Tags
        public const string TAG_PLAYER = "Player";
        public const string TAG_ENEMY = "Enemy";
        public const string TAG_PROJECTILE = "Projectile";
        public const string TAG_EXPERIENCE = "Experience";
        public const string TAG_HEALTH = "Health";
    }
}
