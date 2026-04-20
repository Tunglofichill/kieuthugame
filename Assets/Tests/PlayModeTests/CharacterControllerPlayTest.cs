using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterControllerPlayTest
{
    [UnityTest]
    public IEnumerator MoveCharacter_UpdatesPositionCorrectly_InScene()
    {
        // tạo nhân vật trên scene luôn 
        GameObject player = new GameObject("PlayerTest");
        var controller = player.AddComponent<CharacterControllerExam>();
        player.transform.position = Vector3.zero; // set về O cho dễ nhìn

        // lấy toạ độ X gốc 
        float oldX = player.transform.position.x;

        // di chuyển player tới 
        Vector3 movement = controller.MoveCharacter(5f, new Vector3(1, 0, 0));
        player.transform.Translate(movement * Time.deltaTime); // deltaTime cho y như game thực tế

        // bắt buộc phải chờ 1 frame để unity update position
        yield return null; 

        // check xem x mới có lớn hơn x cũ ko, tức là nhân vật có thụt lên phía trước ko
        float newX = player.transform.position.x;
        Assert.Greater(newX, oldX);

        // OK gòi xoá
        GameObject.Destroy(player);
    }
}
