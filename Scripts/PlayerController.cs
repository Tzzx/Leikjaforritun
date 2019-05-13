using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Sindri Snær Hjaltalin 16/02/2019
    // geri hluti public eða private eftir hovrt ég vil geta breytt þeim here eða inní unity.
    public float speed;
    public Text winText;
    public Text countText;
    private Rigidbody rb;
    private int count;
    

    void Start ()
    {
        // læti hluti hafa guild þannig það koma ekki error þegar ég kalla á þau seinna.
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

  void FixedUpdate ()
    {
        // hreyfa sig fram og aftur.
        float moveHorizontal = Input.GetAxis("Horizontal");
        // hrayfa sig til hægri og vinstri.
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // kalla speed veriablue sem er pubilc að því ég vil geta brytt þív í unity.
        rb.AddForce (movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        // tellur peninga.
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            // count er = 0 læt count hækka um ein fyrir hver pening sem er tekin upp.
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        // ef count er meira eða = 10 byrtu "You Win!!! á skjáinn.
        if (count >= 10)
        {
            winText.text = "You Win!!!";
        }
    }

}
