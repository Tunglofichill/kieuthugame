using UnityEngine;

public class CharacterControllerExam : MonoBehaviour
{
    // hàm theo yêu cầu đề
    public Vector3 MoveCharacter(float speed, Vector3 direction)
    {
        // chuẩn hoá để đi khỏi bị xéo lẹ hơn
        Vector3 normalizedDirection = direction.normalized;
        Debug.Log("dir lúc sau: " + normalizedDirection);

        // nhân với tốc độ ra quảng đường 
        Vector3 result = normalizedDirection * speed;
        Debug.Log("kêt quả tính ra: " + result);

        return result;
    }
}
