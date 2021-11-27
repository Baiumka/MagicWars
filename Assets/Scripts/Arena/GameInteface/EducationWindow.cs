﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationWindow : MonoBehaviour
{
    [SerializeField]
    GameObject dialogPrefab;

    EducationDialog eduDialog;



    float[,] arrowPosition = new float[,] { { 0, 0, 0},//0
    { 0, 0, 0},//1
    { -398, 253, 90 },//2
    { -450, 157, 180 },//3
    { -450, 157, 180 },//4
    { -409, 12, 180 },//5
    { -605, 157, 90 },//6
    { 0, -112, 270 },//7
    { -600, -112, 270 },//8
    { 0, 0, 0},//9
    { 0, 0, 0},//10
    { 0, 0, 0},//11
    { 0, 0, 0},//12
    { 414, -52, 270},//13
    { -512, -119, 270},//14
        { 0, 0, 0},//15
        { -372, -83, 270},//16
         { 0, 0, 0},//17
         { -532, -254, 270},//18
         { 62, -254, 270},//19
         { -238, -254, 270},//20
         { 62, -254, 270},//21
         { 0, 0, 0},//22
         { 0, 0, 0},//23
         { 0, 0, 0},//24
         { 0, 0, 0},//25
         { 0, 0, 0},//26
         { 0, 0, 0},//27
         { 0, 0, 0},//28
         { 0, 0, 0},//29
         { 0, 0, 0},//30

    };

    string[] notes = new string[] { "Привет, рад вас приветствовать в своей игре. Сейчас я проведу краткое обучение.",//0
        "Для начала расскажу про интерфейс...",//1
        "Это ваш показатель здоровья, как только оно закончиться вы умрёте.",//2
        "Смерть - еще не поражение, если вы играете в командном режиме, есть шанс что ваша команда добъёт противника без вас. Кстати вот список вашей команды.",//3
        "Если вы нажмете на члена своей или вражеской команды, то можете узнать более подробную информацию о выбраном игроке.",//4
        "Вы можете скрыть список команд нажав на стрелочку.",//5
        "Прямо под показателем здоровья, будут отображены наложенные на вас эффекты, например: 'Ожог', 'Двойной урон', 'Ослепление' и т.д. Внимательно читайте каждый из них, это важно.",//6
        "Что бы нанести противнику урон или наложить на него эффект вам нужно создать закленание из элементов, для этого выберите один из элементов на нижней панэли.",//7
        "Для первого раза, используйте <color=#ff0000>Огонь</color>.",//8
        "none",//9
        "none",//10
        "Теперь мы ожидаем хода противника...",//11
        "none",//12
        "Противник походил, его счетчик элемнтов в закленании увеличился, следите за ним что бы знать когда ожидать нападения.",//13
        "Теперь давайте объеденим Огонь с другим элементом что бы создать элемент следуйщего уровня, используйте <color=#00BFFF>Воду</color>.",//14
        "none",//15
        "Отлично вы объеденили два элемента и создали новый - Пар. Теперь давайте используем ваше закленание...",//16
        "У вас есть два варианта использования закления: на себя или на противника. В зависимости от выбранной цели каждый элемент может меня свой эффект.",//17
        "С помощью этой кнопки, вы можете использовать закленание на себя...",//18
        "...а с помощью этой на противника.",//19
        "Вы так же можете пропустить свой ход что бы выждать лучшего момента для своего закленания.",//20
        "Что ж, давайте используем закленание на противника!",//21
        "none",//22
        "Вы моежете добавить в закленание до 4 базовых элементов. Комбинируя их, вы можете создать еще два элемента второго уровня. Элементы второго уровня так же могут объедениться и создать элемент третьего(высшего) уровня.",//23
        "Таким образом в вашем закленании может оказаться до 7 элементов. Что бы посмотреть возможноные комбинации с элементом, нажмите на него правой кнопкой мыши.",//24
        "Попробуйте одалеть своего первого противника!",//25
        "none",//26
        "Похоже ваш противник часто использует элемент: Огонь. Защититесь от него, используя комбинацию: Огонь+Защита.",//27
        "none",//28
        "Создайте элемент высшего уровня используя комбинацию: Земля+Природа+Вода+Природа",//29
        "none" };//30 - Last


int noteIteretor = 0;

    void Start()
    {
        Debug.Log("Обучающие окно на связи");
        GameObject dialog = GameObject.Instantiate(dialogPrefab, this.transform);
        eduDialog = dialog.GetComponent<EducationDialog>();
        eduDialog.SetText(notes[noteIteretor]);
        noteIteretor++;
    }

    public void OnClickNext()
    {
        if (noteIteretor > notes.Length-1) return;

        if (notes[noteIteretor] == "none") return;
        if (notes[noteIteretor+1] == "none")
        {
            eduDialog.ShowButton(false);
        }
        else
        {
            eduDialog.ShowButton(true);
        }

        eduDialog.SetText(notes[noteIteretor]);
        eduDialog.SetArrow(arrowPosition[noteIteretor, 0], arrowPosition[noteIteretor, 1], arrowPosition[noteIteretor, 2]);
        noteIteretor++;

        if (noteIteretor == 9)
        {
           // Client.game.UnpauseEducation();
            return;
        }
    }

    public void CurrentEducationStep(int step)
    {
        if (step == 2)
        {
            noteIteretor = 8;
        }

        if (step == 3)
        {
            noteIteretor = 11;
        }
        if (step == 4)
        {
            noteIteretor = 13;
        }
        if (step == 5)
        {
            noteIteretor = 16;
        }
        if (step == 6 && noteIteretor < 25)
        {
            noteIteretor = 23;
        }
        if (step == 7)
        {
            noteIteretor = 27;
        }
        if (step == 8)
        {
            noteIteretor = 29;
        }

        try
        {
            OnClickNext();
        }
        catch
        {

        }
    }

}