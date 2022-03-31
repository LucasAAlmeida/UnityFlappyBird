using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int wallScoreValue = 5;
    private readonly float positionToDestroyWallZ = -2;
    readonly static Color[] wallBodyColors = { Color.red, Color.blue, Color.green, Color.yellow };

    public static IEnumerator CreateRoutine()
    {
        yield return new WaitForSeconds(1);
        while (!GameHandler.Instance.IsGameOver) {
            Create();
            yield return new WaitForSeconds(5);
        }
    }

    public static void Create()
    {
        var obstacle = Instantiate(AssetHandler.Instance.obstaclePrefab);
        var obstacleTransform = obstacle.transform;

        var leftWall = obstacleTransform.GetChild(0);
        var rightWall = obstacleTransform.GetChild(1);

        SetWallBodyColor(leftWall, rightWall);
        MakeGap(leftWall, rightWall);
        SetGapLayout(obstacleTransform);
        SetGapPosition(obstacleTransform);
    }

    private static void SetWallBodyColor(Transform leftWall, Transform rightWall)
    {
        List<Transform> wallBodies = new List<Transform> {
            leftWall.GetChild(0),
            rightWall.GetChild(0)
        };

        var randomIndex = Random.Range(0, wallBodyColors.Length);
        foreach (var wallBody in wallBodies) {
            Renderer r = wallBody.GetComponent<Renderer>();
            r.material.color = wallBodyColors[randomIndex];
        }
    }

    private static void MakeGap(Transform leftWall, Transform rightWall)
    {
        var gapSize = Random.Range(5, 15);
        leftWall.Translate(Vector3.left * gapSize * 0.5f);
        rightWall.Translate(Vector3.right * gapSize * 0.5f);
    }

    private static void SetGapLayout(Transform obstacleTransform)
    {
        var isGapHorizontal = Random.value > 0.5f;
        if (!isGapHorizontal) {
            obstacleTransform.Rotate(Vector3.forward * 90);
        }
    }

    private static void SetGapPosition(Transform obstacleTransform)
    {
        var gapPosition = Random.Range(-10, 10);
        obstacleTransform.Translate(Vector3.right * gapPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);

        if (transform.position.z < positionToDestroyWallZ) {
            GameHandler.Instance.AddToScore(wallScoreValue);
            Destroy(gameObject);
        }
    }
}
