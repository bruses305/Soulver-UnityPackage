#if UNITY_EDITOR
using UnityEngine;

namespace SoulverTools
{
    public abstract class GizmosPrefab
    {
        public static void GizmosSquare(Vector2 pos, Matrix4x4 matrix, Vector2 size, Color color)
        {

            // set matrix
            Matrix4x4 defaultMatrix = Gizmos.matrix;
            Gizmos.matrix = matrix;

            // set the color
            Color defaultColor = Gizmos.color;
            Gizmos.color = color;

            Vector2 point1 = new(pos.x + size.x, pos.y + size.y);
            Vector2 point2 = new(pos.x + size.x, pos.y - size.y);
            Vector2 point3 = new(pos.x - size.x, pos.y - size.y);
            Vector2 point4 = new(pos.x - size.x, pos.y + size.y);

            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(point2, point3);
            Gizmos.DrawLine(point3, point4);
            Gizmos.DrawLine(point4, point1);

            // restore default colors
            Gizmos.color = defaultColor;

            // restore the default matrix
            Gizmos.matrix = defaultMatrix;
        }

        public static void GizmosSquare(Vector2 pos, Matrix4x4 matrix, Vector2 size) =>
            GizmosSquare(pos, matrix, size, Color.red);


        public static void GizmosCircle(Vector2 pos, Matrix4x4 matrix, float radius, float m_Theta = 0.1f) =>
            GizmosCircle(pos, matrix, radius, Color.red, m_Theta: m_Theta);

        public static void GizmosCircle(Vector3 pos, Matrix4x4 matrix, float radius, Color color, float m_Theta = 0.1f)
        {

            // set matrix
            Matrix4x4 defaultMatrix = Gizmos.matrix;
            Gizmos.matrix = matrix;

            // set the color
            Color defaultColor = Gizmos.color;
            Gizmos.color = color;

            // Draw circle
            Vector3 beginPoint = pos;
            Vector3 firstPoint = pos;

            int i = 0;
            for (var theta = 0f; theta < 2 * Mathf.PI; theta += m_Theta, i++)
            {
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                Vector3 endPoint = new Vector3(x, y, 0) + pos;
                if (theta == 0)
                {
                    firstPoint = endPoint;
                }
                else
                {
                    if (i % 3 == 1) Gizmos.DrawLine(beginPoint, endPoint);
                }

                beginPoint = endPoint;
            }

            // Draw the last segment
            Gizmos.DrawLine(firstPoint, beginPoint);

            // restore default colors
            Gizmos.color = defaultColor;

            // restore the default matrix
            Gizmos.matrix = defaultMatrix;
        }
    }
}
#endif