using NUnit.Framework;
using Assets.Scripts;

public class ConstantsTest
{
    [Test]
    public void Constants_HaveCorrectValues()
    {
        UnityEngine.Debug.Log("========== [TEST BẮT ĐẦU] ConstantsTest (EditMode) ==========");

        UnityEngine.Debug.Log("[TEST] (1) Kiểm tra giá trị mua Thỏ (BunnyCost)...");
        Assert.AreEqual(50, Constants.BunnyCost, "BunnyCost không đúng");
        UnityEngine.Debug.Log($"-> OK: Giá mua Thỏ bằng {Constants.BunnyCost}");

        UnityEngine.Debug.Log("[TEST] (2) Kiểm tra giá trị cà rốt thưởng (CarrotAward)...");
        Assert.AreEqual(10, Constants.CarrotAward, "CarrotAward không đúng");
        UnityEngine.Debug.Log($"-> OK: Thưởng cà rốt bằng {Constants.CarrotAward}");

        UnityEngine.Debug.Log("[TEST] (3) Kiểm tra máu khởi điểm của quái (InitialEnemyHealth)...");
        Assert.AreEqual(50, Constants.InitialEnemyHealth, "InitialEnemyHealth không đúng");
        UnityEngine.Debug.Log($"-> OK: Máu khởi điểm quái bằng {Constants.InitialEnemyHealth}");

        UnityEngine.Debug.Log("[TEST] (4) Kiểm tra sát thương mũi tên (ArrowDamage)...");
        Assert.AreEqual(20, Constants.ArrowDamage, "ArrowDamage không đúng");
        UnityEngine.Debug.Log($"-> OK: Sát thương cung tên bằng {Constants.ArrowDamage}");

        UnityEngine.Debug.Log("[TEST] (5) Kiểm tra tầm bắn tối thiểu (MinDistanceForBunnyToShoot)...");
        Assert.AreEqual(3f, Constants.MinDistanceForBunnyToShoot, "MinDistanceForBunnyToShoot không đúng");
        UnityEngine.Debug.Log($"-> OK: Tầm bắn thỏ bằng {Constants.MinDistanceForBunnyToShoot}");

        UnityEngine.Debug.Log("========== [TEST HOÀN THÀNH] ConstantsTest PASSED ==========");
    }
}
