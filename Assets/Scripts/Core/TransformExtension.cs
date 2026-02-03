using UnityEngine;

namespace Core
{
    public static class TransformExtension
    {
        
        public static bool IsInsideArc(this Vector2 center, Vector2 targetPos, Vector2 direction, float distance,float angle)
        {
            Vector2 dir = (targetPos - (Vector2)center);
            if (dir.magnitude > distance) return false;
            float a = Vector2.Angle(direction, dir);
            return a <= angle * 0.5f;
        }
        
        
        
        
    }
}