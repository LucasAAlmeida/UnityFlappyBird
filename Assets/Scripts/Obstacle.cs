using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int wallScoreValue = 5;
    private readonly float positionToDestroyWallZ = -2;
    readonly static Color[] wallBodyColors = { Color.red, Color.blue, Color.green, Color.yellow };

    /// <summary>
    /// Perpetually creates obstacles until the game is over
    /// </summary>
    /// <returns></returns>
    public static IEnumerator CreateRoutine()
    {
        // yield allows the coroutine to pause its operation
        yield return new WaitForSeconds(1);
        while (!GameHandler.Instance.IsGameOver) {
            Create();
            yield return new WaitForSeconds(5);
        }
    }

    /// <summary>
    /// Creates the obstacle with the given prefab, set its positions,
    /// colors it and makes the gap for the bird to fly through,
    /// each one with its own private method to better separate code
    /// </summary>
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

    /// <summary>
    /// Colors each of the obstacle's walls with random colors
    /// </summary>
    /// <param name="leftWall"></param>
    /// <param name="rightWall"></param>
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

    /// <summary>
    /// Moves the obstacle's walls in order to make a gap so that the bird can fly through
    /// </summary>
    /// <param name="leftWall"></param>
    /// <param name="rightWall"></param>
    private static void MakeGap(Transform leftWall, Transform rightWall)
    {
        var gapSize = Random.Range(8, 15);
        leftWall.Translate(Vector3.left * gapSize * 0.5f);
        rightWall.Translate(Vector3.right * gapSize * 0.5f);
    }

    /// <summary>
    /// Randomly rotates the gap vertically or horizontally
    /// </summary>
    /// <param name="obstacleTransform"></param>
    private static void SetGapLayout(Transform obstacleTransform)
    {
        var isGapHorizontal = Random.value > 0.5f;
        if (!isGapHorizontal) {
            obstacleTransform.Rotate(Vector3.forward * 90);
        }
    }

    /// <summary>
    /// Positions the gap in different places of the screen to provide challenge
    /// </summary>
    /// <param name="obstacleTransform"></param>
    private static void SetGapPosition(Transform obstacleTransform)
    {
        var gapPosition = Random.Range(-10, 10);
        obstacleTransform.Translate(Vector3.right * gapPosition);
    }

    /// <summary>
    /// Update is called once per frame
    /// The walls that move, not the bird.
    /// if the bird passes the wall (if the obstacle is behind the bird), it gets destroyed.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);

        if (transform.position.z < positionToDestroyWallZ) {
            GameHandler.Instance.AddToScore(wallScoreValue);
            Destroy(gameObject);
        }
    }
}
