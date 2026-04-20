using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyPathTest
{
    [UnityTest]
    public IEnumerator Enemy_Reaches_Base_And_Lives_Reduced()
    {
        Debug.Log("========== [TEST BẮT ĐẦU] EnemyPathTest ==========");
        yield return SceneManager.LoadSceneAsync("game");
        Debug.Log("[TEST] (1) Đã load scene 'game' thành công.");
        yield return new WaitForSeconds(0.6f);

        var gameManager = Object.FindFirstObjectByType<GameManager>();
        Assert.IsNotNull(gameManager, "Không tìm thấy GameManager");

        int initialLives = gameManager.Lives;
        Debug.Log($"[TEST] (2) Mạng ban đầu của người chơi: {initialLives}");

        var enemyGO = Object.Instantiate(gameManager.EnemyPrefab, 
            gameManager.Waypoints[0].position, Quaternion.identity);
        var enemy = enemyGO.GetComponent<Enemy>();
        Debug.Log("[TEST] (3) Đã triệu hồi Quái Vật tại Waypoint khởi điểm.");

        enemy.SimulateReachLastWaypoint();

        yield return new WaitForSeconds(0.3f);

        Debug.Log($"[TEST] (4) Kiểm tra kết quả: Mạng còn lại = {gameManager.Lives} (Kỳ vọng: {initialLives - 1})");
        Assert.AreEqual(initialLives - 1, gameManager.Lives);
        Debug.Log("========== [TEST HOÀN THÀNH] EnemyPathTest PASSED ==========");
    }
}