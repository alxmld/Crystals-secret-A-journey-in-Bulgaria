using System.Collections.Generic;
using UnityEngine;

public class TextCurve : MonoBehaviour
{
    public static GameObject DrawTextAlongPath(string text, List<VPoint> path, float position = 0.5f, float preferredSize = 0.15f, float letterPaddingFactor = 1f)
    {
        GameObject textObject = new GameObject(text);

        float letterPadding = preferredSize * letterPaddingFactor;
        float totalTextWidth = text.Length * letterPadding;
        float curPointDistance = 0f;
        int curPointIndex = 0;
        float pointDistance = Vector2.Distance(VPointToVector2(path[curPointIndex]), VPointToVector2(path[curPointIndex + 1]));

        float totalPathDistance = GetPathDistance(path, 0f);
        float desiredStartDistance = (totalPathDistance * 0.5f) - (totalTextWidth * 0.5f);

        while (desiredStartDistance < 0)
        {
            preferredSize -= 0.01f;
            letterPadding = preferredSize * letterPaddingFactor;
            totalTextWidth = text.Length * letterPadding;
            desiredStartDistance = (totalPathDistance * 0.5f) - (totalTextWidth * 0.5f);
        }

        float curDistance = desiredStartDistance;
        while (desiredStartDistance > pointDistance)
        {
            desiredStartDistance -= pointDistance;
            curPointDistance += pointDistance;
            curPointIndex++;
            pointDistance = Vector2.Distance(VPointToVector2(path[curPointIndex]), VPointToVector2(path[curPointIndex + 1]));
        }

        foreach (char c in text)
        {
            GameObject letterObject = new GameObject(c.ToString());
            letterObject.transform.SetParent(textObject.transform);
            TextMesh textMesh = letterObject.AddComponent<TextMesh>();
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.text = c.ToString();

            textMesh.characterSize = preferredSize;

            float angle = Vector2.SignedAngle(VPointToVector2(path[curPointIndex + 1]) - VPointToVector2(path[curPointIndex]), Vector2.up);
            Vector2 position2D = Vector2.Lerp(VPointToVector2(path[curPointIndex]), VPointToVector2(path[curPointIndex + 1]), (curDistance - curPointDistance) / pointDistance);
            letterObject.transform.position = new Vector3(position2D.x, 0f, position2D.y);
            letterObject.transform.rotation = Quaternion.Euler(90f, angle - 90f, 0f);

            curDistance += letterPadding;
            float tmpWidth = (curDistance - curPointDistance);
            while (tmpWidth > pointDistance)
            {
                tmpWidth -= pointDistance;
                curPointDistance += pointDistance;
                curPointIndex++;
                pointDistance = Vector2.Distance(VPointToVector2(path[curPointIndex]), VPointToVector2(path[curPointIndex + 1]));
            }
        }

        return textObject;
    }

    private static Vector2 VPointToVector2(VPoint vPoint)
    {
        return new Vector2(vPoint.x, vPoint.y);
    }

    private static float GetPathDistance(List<VPoint> path, float startDistance)
    {
        float totalDistance = 0f;
        for (int i = 0; i < path.Count - 1; i++)
        {
            totalDistance += Vector2.Distance(VPointToVector2(path[i]), VPointToVector2(path[i + 1]));
        }
        return totalDistance;
    }

    public struct VPoint
    {
        public float x, y;

        public VPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
