using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerTest
{
    [UnityTest]
    public IEnumerator AlterMoneyAvailable_CorrectlyUpdatesMoney()
    {
        Debug.Log("========== [TEST BẮT ĐẦU] GameManagerTest ==========");
        
        // Cố gắng load scene "game" nếu scene này có chứa GameManager
        yield return SceneManager.LoadSceneAsync("game");
        Debug.Log("[TEST] (1) Đã load scene 'game' thành công.");
        yield return new WaitForSeconds(0.6f);

        var gameManager = Object.FindFirstObjectByType<GameManager>();
        Assert.IsNotNull(gameManager, "Không tìm thấy GameManager trong scene");

        int initialMoney = gameManager.MoneyAvailable;
        Debug.Log($"[TEST] (2) Tiền ban đầu: {initialMoney}");

        // Kiểm tra hàm cộng thêm tiền
        gameManager.AlterMoneyAvailable(50);
        
        Debug.Log($"[TEST] (3) Sau khi cộng 50, kiểm tra tiền hiện tại = {gameManager.MoneyAvailable} (Kỳ vọng: {initialMoney + 50})");
        Assert.AreEqual(initialMoney + 50, gameManager.MoneyAvailable);

        // Kiểm tra hàm trừ tiền
        gameManager.AlterMoneyAvailable(-100);
        
        Debug.Log($"[TEST] (4) Sau khi trừ 100, kiểm tra tiền hiện tại = {gameManager.MoneyAvailable} (Kỳ vọng: {initialMoney - 50})");
        Assert.AreEqual(initialMoney - 50, gameManager.MoneyAvailable);

        Debug.Log("========== [TEST HOÀN THÀNH] GameManagerTest PASSED ==========");
    }
}
