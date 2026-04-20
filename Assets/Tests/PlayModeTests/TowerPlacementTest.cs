using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.SceneManagement;

public class TowerPlacementTest
{
    [UnityTest]
    public IEnumerator Can_Place_Bunny_Successfully()
    {
        Debug.Log("========== [TEST BẮT ĐẦU] TowerPlacementTest ==========");
        yield return SceneManager.LoadSceneAsync("game");
        Debug.Log("[TEST] (1) Đã load scene 'game' thành công.");
        yield return new WaitForSeconds(0.6f);

        var gameManager = Object.FindFirstObjectByType<GameManager>();
        var dragDrop = Object.FindFirstObjectByType<DragDropBunny>();

        Assert.IsNotNull(gameManager, "Không tìm thấy GameManager");
        Assert.IsNotNull(dragDrop, "Không tìm thấy DragDropBunny");

        int initialMoney = gameManager.MoneyAvailable;
        Debug.Log($"[TEST] (2) Tiền lúc vừa vào Game: {initialMoney}");
        
        while (gameManager.MoneyAvailable < 50)
        {
            gameManager.AlterMoneyAvailable(100);
            Debug.Log($"[TEST] (2b) Bơm thêm 100 tiền. Hiện tại: {gameManager.MoneyAvailable}");
        }

        initialMoney = gameManager.MoneyAvailable;
        Debug.Log($"[TEST] (3) Số tiền trước khi gọi lệnh đặt tháp: {initialMoney}");

        // Thay tọa độ này nếu test fail (dùng vị trí Background thật)
        dragDrop.SimulateValidTowerPlacement(new Vector2(0f, 0f));

        yield return new WaitForSeconds(0.3f);

        Debug.Log($"[TEST] (4) Kiểm tra kết quả: Tiền dư = {gameManager.MoneyAvailable} (Kỳ vọng: {initialMoney - 50})");
        Assert.AreEqual(initialMoney - 50, gameManager.MoneyAvailable);
        Debug.Log("========== [TEST HOÀN THÀNH] TowerPlacementTest PASSED ==========");
    }
}