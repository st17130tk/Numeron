﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI処理のクラスを使用する宣言
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class NumChoice : MonoBehaviour
{
    // Image コンポーネントを格納する変数
    private Image m_Image;
    // スプライトオブジェクトを格納する配列
    public Sprite[] m_Sprite;
    // 指定のimageのスプライトオブジェクトを変更するためのフラグ
    public static int Change;
    // プレイヤーを切り替えたときのフラグ
    public static bool flag_p;
    // 最初の入力が互いに終わったときのフラグ
    public static bool flag_Ans;

    //予想&最初の正解入力
    public static int[,] array = new int[2,3]{{-1,-1,-1},{-1,-1,-1}};

    //正解
    public static int[,] array_Ans = new int[2,3]{{-1,-1,-1},{-1,-1,-1}};

    public Image waku1;
    public Image waku2;
    public Image waku3;
    public Image waku;
    public Image player2;
    public Image player1;
    //gamestartPanel
    [SerializeField] GameObject gamePanel;
    public Text EBtext;
    // ゲーム開始時に実行する処理
    void Start()
    {
        // Image コンポーネントを取得して変数 m_Image に格納

        m_Image = GetComponent<Image>();
        Change = 0;
        flag_p = true;
        flag_Ans = true;
    }

    // ゲーム実行中に毎フレーム実行する処理
    public void OnClick_waku(int number)
    {
        Change =  number;
    }

    public void OnClick_Change10(int number)
    {
        if (Change == 10)OnClick_Change(number, 0);
    }
    public void OnClick_Change11(int number)
    {
        if (Change == 11)OnClick_Change(number, 1); 
    }
    public void OnClick_Change12(int number)
    {
        if (Change == 12)OnClick_Change(number, 2);
    }
    
    public void OnClick_Change(int number, int n)
    {
        m_Image.sprite = m_Sprite[number];
        if(flag_p) array[0,n] = number;
        else array[1,n] = number;
    }
    private IEnumerator Delay(){
        yield return new WaitForSeconds(1.5f);
        gamePanel.SetActive(false);
    }

    public void OnClick_Change_P()
    {

        int n;
        bool flag = true;

        n = flag_p ? 0 : 1;
        //ダブりはダメ
        int n_n=0;
        for(int i=0;i<3;i++){
            for(int h=i+1;h<3;h++){
                if(array[n,i]==array[n,h]){
                    n_n=1;
                    break;
                }
            }
            if(n_n==1){
                break;
            }
        }
        if(n_n==1){
            n_n=0;
            return;
        }
        ///////////////////

        for(int i=0; i<3; i++){
            if(array[n,i]<0) flag = false;
        }
        
        if(flag){
            if(!flag_Ans){
            int[] jj=numeron(array,array_Ans,n);/////////////////////////
            if(jj[0]==3){
                if(n==0){
                SceneManager.LoadScene("Result");
                }
            }
            EBtext.text=jj[0].ToString()+"Eat"+jj[1].ToString()+"Byte";
            }
            flag_p = flag_p ? false : true;
            for(int i=0; i<3; i++){
                if(flag_Ans) array_Ans[n,i] = array[n,i];
                array[n,i] = -1;
            }
            m_Image.sprite = m_Sprite[1-n];
            GameObject image_object = GameObject.Find("Image");
            if(n==0){
            m_Image.sprite = player2.sprite;
            }else{
            m_Image.sprite = player1.sprite;
            }
            waku1.GetComponent<Image>().sprite=waku.sprite;
            waku2.GetComponent<Image>().sprite=waku.sprite;
            waku3.GetComponent<Image>().sprite=waku.sprite;
            if(n==1 && flag_Ans) {
                flag_Ans = false;
                gamePanel.SetActive(true);
                StartCoroutine("Delay");
            }
        }
    }
  ///////////イートか、バイトか
  private int[]  numeron(int[,] array,int[,] ans_array,int n){
      int[] EB=new int[2]{0,0};
      int j=0;
      if(n==0){
          j=1;
      }
      for(int i=0;i<3;i++){
          for(int h=0;h<3;h++){
              if(i==h && ans_array[j,i]==array[n,h]){
                  EB[0]++;
                  break;
              }
              else if(ans_array[j,i]==array[n,h]){
                  EB[1]++;
                  break;
              }
          }
      }
   //   Debug.Log(EB[0]);
  //    Debug.Log(EB[1]);
    return EB;
  }
  /////////////////////////////////
}