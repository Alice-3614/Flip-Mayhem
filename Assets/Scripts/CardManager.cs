using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class CardManager : MonoBehaviour
{
    public List<string> Cards = new List<string>();//カードの並び順としても使用
    [SerializeField] private GameObject cardPrefab;//カードのプレハブ
    [SerializeField] private GameObject BattleField;
    void Start()
    {
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
    }
    public void Shuffle()
    {
        // リストをランダムに並べ替える
        Cards = Cards.OrderBy(a => Guid.NewGuid()).ToList();
    }
    void GenerateCards()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
            card.name = Cards[i];
            card.transform.SetParent(BattleField.transform);
            // カードの見た目を設定するコードをここに追加
        }
        Cards.Clear();
    }
}