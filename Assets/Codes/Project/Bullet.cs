using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformShoot
{
    public class Bullet : MonoBehaviour
    {
        private LayerMask mLayerMask;
        private GameObject mGamePass;

        private int bulletDir;

        private void Start()
        {
            Destroy(gameObject, 3f);
            mLayerMask = LayerMask.GetMask("Ground", "Trigger");
        }

        private void Update()
        {
            transform.Translate(bulletDir * 12 * Time.deltaTime, 0, 0);
        }

        private void FixedUpdate()
        {
            var coll = Physics2D.OverlapBox(transform.position, transform.localScale, 0, mLayerMask);
            if (coll)
            {
                if (coll.CompareTag("Trigger"))
                {
                    mGamePass.SetActive(true);
                    Destroy(coll.gameObject);
                }
                Destroy(gameObject);
            }
        }

        public void InitDir(int dir)
        {
            bulletDir = dir;
        }

        public void GetGamePass(GameObject pass)
        {
            mGamePass = pass;
        }
    }
}
