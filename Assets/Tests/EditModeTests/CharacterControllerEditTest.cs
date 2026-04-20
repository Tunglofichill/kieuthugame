using NUnit.Framework;
using UnityEngine;

public class CharacterControllerEditTest
{
    [Test]
    public void MoveCharacter_Speed5_DirectionX_Returns500()
    {
        // tạo object ảo để gắn script
        GameObject go = new GameObject();
        var controller = go.AddComponent<CharacterControllerExam>();
        
        // đề iêu cầu test số 5 với 1,0,0
        Vector3 result = controller.MoveCharacter(5f, new Vector3(1, 0, 0));
        
        // so sánh kết quả mông đợi với KQ thật
        Assert.AreEqual(new Vector3(5, 0, 0), result);
        
        // pass xong xoá lẹ cho đỡ lag
        GameObject.DestroyImmediate(go);
    }

    [Test]
    public void MoveCharacter_Speed3_DirectionY_Returns030()
    {
        // setup giong cau tren
        GameObject go = new GameObject();
        var controller = go.AddComponent<CharacterControllerExam>();
        
        // gọi hàm
        Vector3 result = controller.MoveCharacter(3f, new Vector3(0, 1, 0));
        
        // check đáp án đề là 0 3 0
        Assert.AreEqual(new Vector3(0, 3, 0), result);
        
        // xoá rác 
        GameObject.DestroyImmediate(go);
    }
}
