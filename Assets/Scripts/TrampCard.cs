using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TrampCard : MonoBehaviour
{
    public int cardNumber; // カードの番号（1～13）
    public string cardSuit; // カードのスート（h, d, c, s）
    CardManager cardManager;
    void Start()
    {
        cardManager = GameObject.Find("GameManager").GetComponent<CardManager>();
    }

    public void OnClick()
    {
        if (cardManager.SelectedCardNumbers.Count < cardManager.selectCardMax)
        {
            // カードが選択されたときの処理をここに追加
            cardManager.SelectedCardNumbers.Add(cardNumber);
            cardManager.SelectedCards.Add(this.gameObject); // 選択されたカードのGameObjectをリストに追加
            GetComponent<Image>().color = Color.gray; // 選択されたカードの色を変更する例
            Debug.Log("Card selected: " + cardSuit + cardNumber);
            if(cardManager.SelectedCardNumbers.Count == cardManager.selectCardMax)
            {
                // 選択されたカードの枚数が最大に達したときの処理をここに追加
                Debug.Log("Maximum cards selected.");
                cardManager.MatchCheck(); // 選択されたカードの合致チェックを呼び出す
            }
        }
    }
}
