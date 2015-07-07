using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDefinition
{
    public static int GameScreenWidth = -1;
    public static int GameScreenHeight = -1;

    //2015快速跳關使用 紀錄跳關編號
    public static int QuickJumpStoryIndex = 0;

    //人物選擇交換時間
    public const float CardChangeTime = 0.5f;

    //最小遊戲人數
    public const int MinPlayerCount = 2;

    //紀錄人物名字，與其對應的角色 <參數預設名字，測試使用>
    public static Dictionary<SystemPlayerName, string> PlayerNameData = new Dictionary<SystemPlayerName, string>() {
        {SystemPlayerName.翠絲,"翠絲"} , {SystemPlayerName.巴洛,"巴洛"} , {SystemPlayerName.卡勒b,"卡勒b"},
        {SystemPlayerName.里昂,"里昂"} , {SystemPlayerName.莉莉卡,"莉莉卡"} , {SystemPlayerName.葛蘭蒂,"葛蘭蒂"}};

    //紀錄目前被選擇的腳色<參數預設，測試使用>
    public static SystemPlayerName CurrentChoosePlayerName = SystemPlayerName.莉莉卡;

    //區域地圖遊戲進行時間 <參數，測試使用>
    public const int AreaMaxGameTime = 900;
    public static int CurrentGameTime = 900;

    //紀錄目前選擇的任務
    public static Mission CurrentChooseMission = Mission.None;
    //紀錄目前選擇的遊戲類型
    public static GameType CurrentChooseGameType = GameType.None;

    //紀錄目前進行到的島嶼
    public static Island CurrentIsland = Island.莎吉斯島;

    //紀錄神秘島目前正在進行的寶物控制器腳本 
    public static TreasureController CurrentTreasureController_Script;

    //任務被觸發的狀況 false = 未進行該任務 <目前為預設值>
    public static Dictionary<Mission, bool> MissionActiveStateMapping = new Dictionary<Mission, bool>() { 
        //(智慧)莎吉斯島
        {Mission.卡片掉了,false} ,{Mission.黃綠紅,false},{Mission.知識通,false},{Mission.推理要在晚餐後,false},{Mission.消失的羅盤,false},
        //(勇氣)布列德島
        {Mission.奶油水果派,false} ,{Mission.給我食譜,false},{Mission.我的船壞了,false},{Mission.在我的歌聲裡,false},{Mission.你怎麼連話都說不清楚,false},
        //(自信)康費爾森島
        {Mission.我要成為畢卡索,false} ,{Mission.筆墨登場,false},{Mission.你是我的眼,false},{Mission.未填詞,false},{Mission.混亂的程序,false}};

    public static List<QuickAnsGameQuestionData> QuickAnsGameQuestionDataList_isUsed = new List<QuickAnsGameQuestionData>();
    public static List<QuickAnsGameQuestionData> QuickAnsGameQuestionDataList = new List<QuickAnsGameQuestionData>()
    {
        new QuickAnsGameQuestionData("台灣每年出產量達1800萬隻，世界第一的水族王國，主要哪種生物？","蝦","魚","龜",1 ),
        new QuickAnsGameQuestionData("2015年5月台灣指揮家莊東杰獲得馬爾科國際青年指揮大賽，拿下世界冠軍，請問在哪舉辦？","紐西蘭","澳洲","丹麥",3 ),
        new QuickAnsGameQuestionData("2014WCE世界盃烘豆大賽，來自台灣的賴昱權榮登冠軍寶座，請問他烘的主要是？","紅豆","咖啡豆","花豆",2 ),
        new QuickAnsGameQuestionData("2015世界威士忌競賽，冠軍來自台灣麥芽威士忌原酒，是屬於哪個地方？","屏東","花蓮","宜蘭",3 ),
        new QuickAnsGameQuestionData("2015年國際發明展，台灣共拿回26金16銀11銅，以及7面特別獎，獲獎率世界排名第一，請問是在哪裡舉辦的？","瑞士日內瓦","德國","英國",1 ),
        new QuickAnsGameQuestionData("2014年亞運女子舉重63公斤 我國林子琦奪金，請問<舉重>屬於哪種類型運動？","打擊","角力","有氧",2 ),
        new QuickAnsGameQuestionData("美國白宮指定台灣製造，請問是哪項產品","電視","檯燈","窗簾布",3 ),
        new QuickAnsGameQuestionData("2015年「第23屆台灣精品金銀質獎」台灣某自行車品牌榮獲「REACTO（銳克多）世界冠軍選手車」，是哪個品牌呢？","美利達","捷安特","鳳凰",1 ),
        new QuickAnsGameQuestionData("世界大學運動會何時將於台灣舉辦？","2016","2017","2018",2 ),
        new QuickAnsGameQuestionData("台灣最長的跨海大橋？","澎湖跨海大橋","鵬灣跨海大橋","杭州灣跨海大橋",1 ),
        new QuickAnsGameQuestionData("2014正式列為台灣第九座的國家公園是位於何處？","蘭嶼","澎湖","東沙島",2 ),
        new QuickAnsGameQuestionData("台南的億載金城是由誰所建築的？","沈葆楨","劉銘傳","鄭成功",1 ),
        new QuickAnsGameQuestionData("下列誰沒有得到諾貝爾物理獎？","楊振寧","李遠哲","李政道",2 ),
        new QuickAnsGameQuestionData("台灣面積最大的是下列哪個縣市","台東縣","屏東縣","花蓮縣",3 ),
        new QuickAnsGameQuestionData("台灣唯一的海底隧道哪裡","澎湖","基隆","旗津",3 ),
        new QuickAnsGameQuestionData("台灣哪一族原住民.在豐收祭時會有婦女跳表演長髮舞","達悟族","布農族","阿美族",1 ),
        new QuickAnsGameQuestionData("1996年亞特蘭大奧運是用布農族的哪一首樂曲作為大會開幕曲呢？","飲酒歌","八部合音","捕魚歌",1 ),
        new QuickAnsGameQuestionData("曾登上《紐約時報》的世界十大餐廳之一的台灣餐廳是？","赤鬼","鼎泰豐","鼎王",2 ),
        new QuickAnsGameQuestionData("2013年台灣哪一個縣市製作了世界最長的立體書呢？","台北市","台中市","台南市",2 ),
        new QuickAnsGameQuestionData("我們愛吃的毛豆長大後會變成什麼？","青豆","蠶豆","黃豆",3 ),
        new QuickAnsGameQuestionData("2015年台灣女將莊佳佳於俄羅斯拿下何項世界性錦標賽的金牌？也篤定得到明年里約奧運會的門票。","射箭","自行車","跆拳道",3 ),
        new QuickAnsGameQuestionData("響尾蛇的響尾(又稱角質環)材質跟人的哪個部位材質一樣？","頭髮","眼睛","指甲",3 ),
        new QuickAnsGameQuestionData("身體哪個部位的紋路和指紋一樣，每個人都不同？","舌頭","耳朵","牙齒",1 ),
        new QuickAnsGameQuestionData("世界五大洲哪一個洲種不出玉米？","北極洲","南極洲","非洲",2 ),
        new QuickAnsGameQuestionData("2015年第28屆世大運在哪個國家舉辦？","台灣","日本","韓國",3 ),
        new QuickAnsGameQuestionData("在網球比賽中，零是用哪個單字來表示？","Love","Like","lose",1 ),
        new QuickAnsGameQuestionData("日本職棒選手楊岱鋼隸屬於日本火腿鬥士隊，請問火腿隊的主場是在日本的哪個城市？","大阪","東京","北海道",3 ),
        new QuickAnsGameQuestionData("2018年世界杯足球賽是由哪個國家主辦？","英國","義大利","俄羅斯",3 ),
        new QuickAnsGameQuestionData("NBA職業選手林書豪今天效力哪個球隊？","湖人隊","火箭隊","尼克隊",1 ),
        new QuickAnsGameQuestionData("常見的鮪魚種類，何者單價最高？","大目鮪","真鮪","長鰭鮪",2 ),
        new QuickAnsGameQuestionData("流行樂團「飛兒樂團」的英文簡稱？","FBI","FTA","FIR",3 ),
        new QuickAnsGameQuestionData("在比薩斜塔上做自由落體實驗的是哪一位科學家？","伽利略","牛頓","愛因斯坦",1 ),
        new QuickAnsGameQuestionData("「既生瑜，何生亮」中的亮，指的是？","卜學亮","豬哥亮","諸葛亮",3 ),
        new QuickAnsGameQuestionData("下列哪裡未將葡萄牙語語列為官方語言？","葡萄牙","墨西哥","巴西",2 ),
        new QuickAnsGameQuestionData("下列何人不是日治時期的台灣總督？","伊澤多喜男","田健治郎","木村拓哉",3 ),
        new QuickAnsGameQuestionData("「不為五斗米折腰」的古人是？","陶淵明","劉伯溫","岳飛",1 ),
        new QuickAnsGameQuestionData("環境保護中所謂的「三生」不包括下列何者？","生態","生物","生活",2 ),
        new QuickAnsGameQuestionData("iPhone5的充電接頭規格稱為？","USB3.0","Lightning","Mirco USB",2 ),
        new QuickAnsGameQuestionData("下列哪個是台灣「高雄」的名產？","旗魚捲","鐵蛋、阿給","太陽餅",1 ),
        new QuickAnsGameQuestionData("從1999年起連載15年，於2014年底以總共700話完結的知名漫畫是？","哆啦A夢","獵人","火影忍者",3 ),
        new QuickAnsGameQuestionData("長期注視3C產品容易因接收過量藍光而傷眼，請問若配戴抗藍光眼鏡看見的畫面會偏向何種顏色？","藍色","綠色","黃色",3 ),
        new QuickAnsGameQuestionData("台灣最北邊的島嶼為？","東引","北竿","西莒",1 ),
    };

    public static List<string> HandSomethingGameQuestionList_isUsed = new List<string>();
    public static List<string> HandSomethingGameQuestionList = new List<string>() 
    {
       "葉問","如虎添翼","冰雪奇緣","哥吉拉","駭客任務","派大星","宮保雞丁","和樂新聞","旋轉木馬","變形金剛","阿基師","熱氣球","洗衣機","樂高","按摩椅",
       "神魔之塔","雲霄飛車","隱形眼鏡","來自星星的你","馬拉松","唱題","鳳雛傳II","救護車","珍珠奶茶","颱風","少林足球","愛心媽媽"
    };

    //按鈕事件
    public enum ButtonEvent
    {
        Nothing = 0,
        //離開遊戲
        ExitGame = 1, OpenStartGame = 2, BackGame = 3,
        //角色選擇場景
        SureButton_RoleSelect = 1000, LeftArrow_RoleSelect = 1001, RightArrow_RoleSelect = 1002, StartGame_RoleSelect = 1003,

        //區域地圖場景
        MissionSure_Area = 2000, MissionCancel_Area = 2001, NextAreaMap = 2002, SureNextArea = 2003, CancelNextArea = 2004,
        NextGameStep = 3000,
        GameEnd = 4000, GameEnd_卡片掉了 = 4001,
        HandSomethingGame_Correct = 5000, HandSomethingGame_Giveup = 5001, ColorGame_ShowAnswer = 5002, ColorGame_Correct = 5003, ColorGame_Error = 5004, ReasoningGameNextHint = 5005, ReasoningGameShowAnswer = 5006, IdiomsGameShowAnswer = 5007, ReasoningGameSkip = 5008,
        TreasureGame_Finish = 6000
    }

    public enum SystemPlayerName
    {
        翠絲, 巴洛, 卡勒b, 里昂, 莉莉卡, 葛蘭蒂
    }

    public enum DialogName
    {
        None = 0, 被選角色名 = 1,

        //主角群----
        翠絲 = 11, 巴洛 = 12, 卡勒b = 13, 里昂 = 14, 莉莉卡 = 15, 葛蘭蒂 = 16,

        //NPC----
        卡片蒐集者 = 21, 知識通 = 22, 警長 = 23, 小偷 = 24, //第一座島嶼
        貪吃鬼 = 31, 甜點師 = 32, 老船長 = 33, 美人魚 = 34, 醫生 = 35, 病人 = 36, //第二座島嶼
        小畫家 = 41, 著名歌手 = 42, 音樂家 = 43, 村長 = 44, //第三座島嶼

        //2015腳色
        鳳雛仙人 = 100, 阿當 = 101, 小美 = 102, 阿明 = 103,
        阿明媽 = 104, 小玉 = 105, 阿偉哥 = 106, 阿哲 = 107, 主持人 = 108, 老師 = 109, 校長 = 110, 同學A = 111, 同學B = 112, 創作家 = 113
    }

    /// <summary>
    /// 任務種類
    /// </summary>
    public enum Mission
    {
        None = 0,
        //(智慧)莎吉斯島
        卡片掉了 = 11, 黃綠紅 = 12, 知識通 = 13, 推理要在晚餐後 = 14, 消失的羅盤 = 15,    //關鍵任務：推理要在晚餐後

        //(勇氣)布列德島
        奶油水果派 = 21, 給我食譜 = 22, 我的船壞了 = 23, 在我的歌聲裡 = 24, 你怎麼連話都說不清楚 = 25,    //關鍵任務：你怎麼連話都說不清楚

        //(自信)康費爾森島
        我要成為畢卡索 = 31, 筆墨登場 = 32, 你是我的眼 = 33, 未填詞 = 34, 混亂的程序 = 35   //關鍵任務：混亂的程序
    }

    /// <summary>
    /// 遊戲種類
    /// </summary>
    public enum GameType
    {
        None = 0,
        記憶對對碰 = 1, 顏不及意 = 2, 快問快答 = 3, 推理要在晚餐後_1 = 4, 支援前線_1 = 5,
        大家來找碴 = 6, 歌喉戰_1 = 7, 比手畫腳 = 8, 人人都是畢卡索_1 = 9, 排列組合 = 10,
        支援前線_2 = 11, 歌喉戰_2 = 12, 人人都是畢卡索_2 = 13, 看圖猜成語 = 14, 肢體對對碰_1 = 15, 肢體對對碰_2 = 16, 文字聯想王 = 17, 推理要在晚餐後_2 = 18, 繪畫聯想王 = 19,
    }

    public enum Island
    {
        莎吉斯島 = 1, 布列德島 = 2, 康費爾森島 = 3, 神秘島 = 4
    }

    public enum TreasureType
    {
        劍 = 1, 戰甲 = 2, 眼鏡 = 3
    }

    public class QuickAnsGameQuestionData
    {
        public string QuestionText;

        public string OptionText1;
        public string OptionText2;
        public string OptionText3;

        public int TrueAnswer;
        public QuickAnsGameQuestionData(string question, string option1, string option2, string option3, int trueAnswer)
        {
            this.QuestionText = question;
            this.OptionText1 = option1;
            this.OptionText2 = option2;
            this.OptionText3 = option3;
            this.TrueAnswer = trueAnswer;
        }
    }

    [System.Serializable]
    public class RoleData
    {
        public SystemPlayerName SystemName;  //方便辨識，系統名稱
        public GameObject RoleObject;   //腳色動畫物件
    }
}