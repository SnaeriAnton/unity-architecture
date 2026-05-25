using UnityEngine;

namespace Game
{
    public class PlayerFactory
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public PlayerFactory(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public Entity CreatePlayer(Transform playerTransform, Border border, float speed, float iFramesDuration)
        {
            Entity player = _commandBuffer.CreateEntity();

            _world.Add(player, new PlayerTag());

            _world.Add(player, new Position
            {
                Value = playerTransform.position
            });

            _world.Add(player, new MoveInput
            {
                Value = Vector2.zero
            });

            _world.Add(player, new PlayerShield
            {
                IsActive = false,
                Current = 0,
                Cooldown = 0
            });
            
            _world.Add(player, new MoveSpeed
            {
                Value = speed
            });

            _world.Add(player, new MovementBounds
            {
                Size = border.Size,
                PlayerScale = playerTransform.localScale
            });

            _world.Add(player, new PlayerViewRef
            {
                Transform = playerTransform
            });
            
            _world.Add(player, new Health
            {
                Current = 0,
                Max = 0
            });

            _world.Add(player, new Invulnerability
            {
                TimeLeft = 0f,
                Duration = iFramesDuration
            });

            return player;
        }
    }
}