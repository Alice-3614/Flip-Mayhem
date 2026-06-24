using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class CardManager : MonoBehaviour
{
    public List<string> Cards = new List<string>();//カードの並び順としても使用
    [SerializeField] private GameObject cardPrefab;//カードのプレハブ
    [SerializeField] private GameObject BattleField;
    public int Player1Score = 0;//スキル発動で消費する
    public int Player2Score = 0;//スキル発動で消費する
    public int selectCardMax;//選択できるカードの枚数(基本は2)s
    public List<int> SelectedCardNumbers = new List<int>(); // 選択されたカードの番号を格納するリスト
    public List<GameObject> SelectedCards = new List<GameObject>(); // 選択されたカードのGameObjectを格納するリスト
    GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup = GameObject.Find("BattleField").GetComponent<GridLayoutGroup>();
        Cards.Clear();
        for (int i = 1; i < 14; i++)
        {
            Cards.Add("h" + i);//ハート
            Cards.Add("d" + i);//ダイヤ
            Cards.Add("c" + i);//クローバー
            Cards.Add("s" + i);//スペード
        }
        Shuffle();
        GenerateCards();
        selectCardMax = 2; // 選択できるカードの最大枚数を設定
    }
    public void Shuffle()
    {
        // リストをランダムに並べ替える
        Cards = Cards.OrderBy(a => Guid.NewGuid()).ToList();
    }
    void GenerateCards()
    {
        gridLayoutGroup.enabled = true;
        for (int i = 0; i < Cards.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
            card.name = Cards[i];
            card.GetComponent<TrampCard>().cardNumber = int.Parse(Cards[i].Substring(1)); // カード番号を設定
            card.GetComponent<TrampCard>().cardSuit = Cards[i].Substring(0, 1); // カードのスートを設定
            card.transform.SetParent(BattleField.transform);
            // カードの見た目を設定するコードをここに追加
        }
        Cards.Clear();
    }
    public void MatchCheck()//選択されたカードの合致チェック
    {
        gridLayoutGroup.enabled = false;
        for (int a = 0; a < SelectedCardNumbers.Count; a++)
        {
            for (int b = a + 1; b < SelectedCardNumbers.Count; b++)
            {
                if (SelectedCardNumbers[a] == SelectedCardNumbers[b])
                {
                    Debug.Log("Match found: " + SelectedCardNumbers[a]);

                    // マッチした場合の処理をここに追加
                    Destroy(SelectedCards[a]);
                    Destroy(SelectedCards[b]);
                    SelectedCards.RemoveAt(a); // マッチしたカードをリストから削除
                    SelectedCards.RemoveAt(b - 1); // bのインデックスはaを削除した後に1つ減るため、b-1を使用
                    if (SelectedCards.Count >= 2)
                    {
                        if (SelectedCards[0].GetComponent<TrampCard>().cardNumber == SelectedCards[1].GetComponent<TrampCard>().cardNumber)
                        {
                            Destroy(SelectedCards[0]);
                            Destroy(SelectedCards[1]);
                            SelectedCards.RemoveAt(0); // マッチしたカードをリストから削除
                            SelectedCards.RemoveAt(0); // マッチしたカードをリストから削除
                        }
                        else
                        {
                            foreach (GameObject card in SelectedCards)
                            {
                                card.GetComponent<Image>().color = Color.white; // 選択されたカードの色を元に戻す
                            }
                        }
                    }
                    else if (SelectedCards.Count >= 1)
                    {
                        SelectedCards[0].GetComponent<Image>().color = Color.white; // 選択されたカードの色を元に戻す
                    }
                    SelectedCardNumbers.Clear(); // チェック後に選択されたカードの番号リストをクリア
                    SelectedCards.Clear(); // チェック後に選択されたカードのGameObjectリストをクリア
                    return;
                }
                else
                {
                    Debug.Log("No match: " + SelectedCardNumbers[a] + " and " + SelectedCardNumbers[b]);
                    // マッチしなかった場合の処理をここに追加
                    foreach (GameObject card in SelectedCards)
                    {
                        card.GetComponent<Image>().color = Color.white; // 選択されたカードの色を元に戻す
                    }
                }
            }
        }
        SelectedCardNumbers.Clear(); // チェック後に選択されたカードの番号リストをクリア
        SelectedCards.Clear(); // チェック後に選択されたカードのGameObjectリストをクリア

    }
}