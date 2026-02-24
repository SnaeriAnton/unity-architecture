using UnityEngine;

namespace Runtime
{
    public class Border : MonoBehaviour
    {
        private const int NUMBER_OF_ATTEMPTS = 20;
        
        public Vector2 Size => transform.localScale;
        
        public Vector3 PickPoint(Vector3 playerPosition, float radiusPlayer)
        {
            Vector2 player2 = new Vector2(playerPosition.x, playerPosition.y);
            float halfW = transform.localScale.x / 2f;
            float halfH = transform.localScale.y / 2f;

            float minX = -halfW;
            float maxX = halfW;
            float minY = -halfH;
            float maxY = halfH;

            float r2 = radiusPlayer * radiusPlayer;

            for (int i = 0; i < NUMBER_OF_ATTEMPTS; i++)
            {
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                Vector2 p = new Vector2(x, y);

                if ((p - player2).sqrMagnitude >= r2) return p;
            }

            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector2 randomDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            return player2 + randomDir * radiusPlayer;
        }
    }
}
