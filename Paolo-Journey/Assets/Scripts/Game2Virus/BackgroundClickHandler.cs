using UnityEngine;

public class BackgroundClickHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        // ลดคะแนนเมื่อคลิกไม่โดนไวรัส
        ScoreManager.Instance.AddScore(-10, Vector3.zero); // ลบ 1 คะแนน
        SoundManager.Instance.Play(SoundManager.SoundName.False);

    }
}