using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformShoot
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D mRig;
        private float mGroundMoveSpeed = 5f;
        private float mJumpForce = 12f;

        private bool mJumpInput = false;
        private int mFaceDir = 1;

        private MainPanel mMainPanel;
        private GameObject mGamePass;

        private void Start()
        {
            mRig = GetComponent<Rigidbody2D>();
            mGamePass = GameObject.Find("GamePass");
            mGamePass.SetActive(false);
            mMainPanel = GameObject.Find("MainPanel").GetComponent<MainPanel>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                var obj = Resources.Load<GameObject>("Bullet");
                obj = Instantiate(obj, transform.position, Quaternion.identity);
                Bullet bullet = obj.GetComponent<Bullet>();
                bullet.GetGamePass(mGamePass);
                bullet.InitDir(mFaceDir);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                mJumpInput = true;
            }
        }

        private void FixedUpdate()
        {
            if (mJumpInput)
            {
                mJumpInput = false;
                mRig.velocity = new Vector2(mRig.velocity.x, mJumpForce);
            }
            float h = Input.GetAxisRaw("Horizontal");
            if (h != 0 && h != mFaceDir)
            {
                mFaceDir = -mFaceDir;
                transform.Rotate(0, 180, 0);
            }
            mRig.velocity = new Vector2(h * mGroundMoveSpeed, mRig.velocity.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Reward"))
            {
                Destroy(collision.gameObject);
                mMainPanel.UpdateScoreText(1);
            }

            if (collision.CompareTag("Door"))
            {
                SceneManager.LoadScene("GamePassScene");
            }
        }
    }
}
