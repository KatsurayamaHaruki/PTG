using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private PhotonView photonView;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();

        // このオブジェクトが自分のものであれば、Cinemachineカメラの追従対象に設定する
        if (photonView.IsMine)
        {
            // まずはシーン内からカメラを探す
            CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            // もしカメラが見つかったら、追従対象に設定する
            if (virtualCamera != null)
            {
                virtualCamera.Follow = transform;
            }
            // もしカメラが見つからなかったら、エラーメッセージを出す
            else
            {
                Debug.LogError("シーン内にCinemachine Virtual Cameraが見つかりません。MainGameScreenにカメラを配置しているか確認してください。");
            }
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveX, moveY).normalized * moveSpeed;
    }
}