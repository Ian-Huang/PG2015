using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDefinition
{
    public static int GameScreenWidth = -1;
    public static int GameScreenHeight = -1;

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
        new QuickAnsGameQuestionData("2014年世界盃足球賽在下列哪個國家舉辦？","西班牙","阿根廷","巴西",3),
        new QuickAnsGameQuestionData("戰國末年，楚國詩人屈原因為感到救國無望，在絕望和悲憤之下懷大石跳進哪條河川？","黃河","汨羅江","淮河",2),
        new QuickAnsGameQuestionData("是誰提出行星運動三大定律？","哥白尼","克卜勒","伽利略",2),
        new QuickAnsGameQuestionData("誰領導了曼哈頓計畫並被稱為「原子彈之父」","理查德‧費曼","阿爾柏特‧愛因斯坦","羅伯特·奧本海默",3),
        new QuickAnsGameQuestionData("是誰設計現代交流電力系統？","安德烈‧馬里‧安培","尼古拉·特斯拉","詹姆斯‧克拉克‧馬克斯威爾",2),
        new QuickAnsGameQuestionData("歷史上最重要的數學家之一，並有「數學王子」之稱的是誰？","高斯","李昂哈德‧尤拉","畢達哥拉斯",1),
        new QuickAnsGameQuestionData("以下何人著有《戰爭與和平》、《安娜·卡列尼娜》和《復活》這幾部被視作經典的長篇小說","屠格涅夫","歌德","列夫·托爾斯泰",3),
        new QuickAnsGameQuestionData("小說「新人間革命」中，山本伸一是誰的化名？","牧口常三郎","戶田城聖","池田大作",3),
        new QuickAnsGameQuestionData("以下何者不是鉛筆的成份？","石墨","鉛","黏土",2),
        new QuickAnsGameQuestionData("以下哪種電器不需要變壓器？","電腦","電風扇","手機充電器",2),
        new QuickAnsGameQuestionData("中文名稱為「國際商業機器公司」，有「藍色巨人」之稱的企業是？","IBM","Intel","Apple",1),
        new QuickAnsGameQuestionData("除了埃及外以下哪個古文明有金字塔建築？","中國","印加","印度",2),
        new QuickAnsGameQuestionData("撲克牌中每個花色的K都代表著歷史上偉大的君王，黑桃是大衛王，梅花是亞歷山大大帝，紅桃是查理大帝，方塊是？","亞瑟王","凱撒大帝","秦始皇",2),
        new QuickAnsGameQuestionData("歷史上有名的諾曼地戰役發生在哪個國家？","德國","法國","英國",2),
        new QuickAnsGameQuestionData("三國演義中「臥龍鳳雛，得一可安天下」，其中「鳳雛」指的是誰？","諸葛亮","周瑜","龐統",3),
        new QuickAnsGameQuestionData("下列何者不屬於「歲寒三友」之列？","竹","梅","蓮",3),
        new QuickAnsGameQuestionData("地球公轉一圈大約需要多久？","一年","一個月","24小時",1),
        new QuickAnsGameQuestionData("以下哪個不是電腦容量單位？","KB","GB","FB",3),
        new QuickAnsGameQuestionData("以下何者不是正義聯盟的成員？","布魯思·偉恩","克拉克·肯特","史蒂芬·羅傑斯",3),
        new QuickAnsGameQuestionData("以下何者不是復仇者聯盟的成員？","東尼·史塔克","克拉克·肯特","雷神·索爾",2),
        new QuickAnsGameQuestionData("哆啦A夢是出自哪位漫畫家之手？","藤子·F·不二雄","臼井儀人","尾田榮一郎",1),
        new QuickAnsGameQuestionData("以下何者不是台灣歷史上曾有的別名？","大員","高砂","安平",3),
        new QuickAnsGameQuestionData("華爾街(Wall street)的wall(牆)最初的目的為何？","抵禦敵人","公佈消息","管控黑奴",3),
        new QuickAnsGameQuestionData("至今英國唯一一位女首相，有「鐵娘子」之稱的人是？","柴契爾夫人","伊麗莎白二世","瑪格麗特二世",1),
        new QuickAnsGameQuestionData("「小時了了、大未必佳」是指誰？","孔子","孔融","孔明",2),
        new QuickAnsGameQuestionData("希臘神話中，宙斯掌管宇宙，那掌管冥界的是誰？","黑帝斯","宙斯","普羅米修斯",1),
        new QuickAnsGameQuestionData("哪一種動物不會進行無性生殖？","蝸牛","蜜蜂","草履蟲",1),
        new QuickAnsGameQuestionData("創價學會創立之日是哪一天？","3.16","11.18","5.3",2),
        new QuickAnsGameQuestionData("台鐵普悠瑪號的名字是源自哪個原住民族的族語？","卑南族","布農族","阿美族",1),
        new QuickAnsGameQuestionData("以下何者「不是」第一次世界大戰期間的發明？","面紙","電燈泡","拉鍊",2),
        new QuickAnsGameQuestionData("以下哪個不是溫度的單位？","攝氏","華氏","芮氏",3),
        new QuickAnsGameQuestionData("為什麼輕觸含羞草葉子會合起來？","膨壓作用","反射作用","光合作用",1),
        new QuickAnsGameQuestionData("請問平時用來唱題的念珠有幾顆珠子？","168","108","88",2),
        new QuickAnsGameQuestionData("在本屆世足賽有十個隊伍穿著臺灣製的環保球衣，請問是回收下列何種物品製成？","塑膠袋","寶特瓶","鋁箔包",2),
        new QuickAnsGameQuestionData("請問下列選項不是貝多芬的作品？","費加洛的婚禮","月光奏鳴曲","命運交響曲",1),
        new QuickAnsGameQuestionData("衛生紙與面紙的主要差別在於纖維的：","密度","長度","硬度",2),
        new QuickAnsGameQuestionData("以下何種材質的衣服最不容易皺？","亞麻布","棉布","聚酯纖維",3),
        new QuickAnsGameQuestionData("應該維護會館環境整潔的人是？","華城會","金城會","每一個人",3),
        new QuickAnsGameQuestionData("下列海邊常見的東西中，何者與其他組成成份最不相同？","沙子","貝殼","珊瑚礁",1),
        new QuickAnsGameQuestionData("螞蟻主要依靠哪種感官來辨別同伴？","視覺","嗅覺","觸覺",2),
        new QuickAnsGameQuestionData("“法律面前人人平等”是什麼人最先提出來的?","中國","法國","英國",2),
        new QuickAnsGameQuestionData("古人所謂“黃道”是哪種天體運行周年的軌道？","月亮","太陽","金星",2),
        new QuickAnsGameQuestionData("如果把一個成人的全部血管連接起來，其長度接近多少公里？","10公里","1萬公里","10萬公里",3),
        new QuickAnsGameQuestionData("青蛙除了用肺外還用什麼器官呼吸？","皮膚","鰓","脾臟",1),
        new QuickAnsGameQuestionData("下列哪個動物不屬於十二生肖?","汗血寶馬","花栗鼠","哆啦A夢",3),
        new QuickAnsGameQuestionData("下列哪一下非現行社交通訊軟體？","LINE","WeChat","MSN",3),
        new QuickAnsGameQuestionData("請問被稱為「台灣電腦之父」是誰？","郭台銘","施振榮","李國鼎",2),
        new QuickAnsGameQuestionData("下列何者是繪圖專用軟體","繪聲繪影","Excel","PhotoShop",3),
        new QuickAnsGameQuestionData("下列哪一項非作業系統？","Mac OS","Linux","MMS",3),
        new QuickAnsGameQuestionData("身分證字號最後一碼是用來做為下列那種檢驗？","檢查號碼的正確性","一致性","範圍",1),
        new QuickAnsGameQuestionData("最後一位蘇聯領導人，推動政策改革，結束了全世界的冷戰對峙局面，此人是？","戈巴契夫","史達林","列寧",1),
        new QuickAnsGameQuestionData("有日本「經營之神」稱號，並且與池田先生對談，內容編撰成《人生問答》的是誰？","安藤忠雄","松下幸之助","安倍晉三",2),
        new QuickAnsGameQuestionData("曾與池田先生對話，因為拒絕在巴士上讓座給白人，點燃了美國拒乘巴士的聯合抵制運動，此位黑人女性是？","J.K.羅琳","羅莎‧帕克斯","碧昂絲",2),
        new QuickAnsGameQuestionData("曾與池田先生對話，南非首位黑人總統，以非暴力行動促進種族和解，此人是？","甘地","錢德拉","曼德拉",3),
        new QuickAnsGameQuestionData("因為生活中開心的事情而獲得短暫的快樂，是「十界」中的哪一界？","畜生界","天界","人界",2),
        new QuickAnsGameQuestionData("以自我為中心，好勝心強烈，是「十界」中的哪一界？","修羅界","聲聞界","畜生界",1),
        new QuickAnsGameQuestionData("形容接受此一信仰容易，但要能堅持信心卻是難上加難的是下列哪一個佛法用語？","受易持難","佛法西還","依正不二",1),
        new QuickAnsGameQuestionData("對所有人都極為尊敬，不輕視任何人，此一做人的良好態度，就如同？","諸天善神","觀世音菩薩","常不輕菩薩",3),
        new QuickAnsGameQuestionData("下列何者是象徵學問、思想的菩薩？","妙音菩薩","藥王菩薩","普賢菩薩",3),
        new QuickAnsGameQuestionData("貫徹信心的人， 誰必定會以各種人事物的姿態出現在其生活周遭，守護法華經的行者？","地涌菩薩","諸天善神","三類強敵",2),
        new QuickAnsGameQuestionData("曾與池田先生共同出版對談集，著名的武俠小說大師，「香港四大才子」之一，此人是？","古龍","黃易","金庸",3),
        new QuickAnsGameQuestionData("非洲第一位諾貝爾和平獎女得主，創立綠帶運動，在肯亞種植四千五百萬棵樹，此人是？","萬家麗‧瑪阿塔伊","加倍加‧瑪阿塔伊","寶加利‧瑪阿塔伊",1),
        new QuickAnsGameQuestionData("池田先生與湯因比博士的對談集，至今已被翻譯成28國語言出版，此書是？","二十一世紀的警鐘","展望二十一世紀","探求一個燦爛的世紀",2),
        new QuickAnsGameQuestionData("曾與池田先生共同出版對談集，先後獲得了諾貝爾化學獎與和平獎，二次大戰號召當時著名的科學家聯署反對發展核武，此人是？","萊納斯‧鮑林","馬克斯‧普朗克","約里奧‧居里",1),
        new QuickAnsGameQuestionData("曾與池田先生進行對談，擔任中國總理期間發生文化大革命，他卻設法予以保護，挽救了大批珍貴文物，此人是？","胡錦濤","鄧小平","周恩來",3),
        new QuickAnsGameQuestionData("此刻的我們都為了「遊戲過關」此相同目標一起努力，這就是所謂的？","同體異心","異地共戰","異體同心",3),
        new QuickAnsGameQuestionData("請問下列哪一項不包含「三障四魔」中的「四魔」？","天子魔","佛地魔","死魔",2),
        new QuickAnsGameQuestionData("請問下列哪一項不是三類強敵？","道門增上慢","念書真散漫","俗眾增上慢",2),
        new QuickAnsGameQuestionData("一個人驕傲自滿，認為自己最有道理，固執不聽他人意見，下列哪個是對應此情況？","願兼於業","我慢偏執","一念三千",2),
        new QuickAnsGameQuestionData("當我們遇到各種生活挑戰時，哪句話告訴我們要以喜悅的心去面對？","十界互具","我慢偏執","煩惱即菩堤",3),
        new QuickAnsGameQuestionData("下列何者意指：一切的困難與挑戰都是為了證明信心的功德而自願承受的？","廣宣流布","願兼於業","發迹顯本",2),
        new QuickAnsGameQuestionData("貪得無厭，被慾望所控制而毫不滿足，是「十界」中的哪一界？","人界","地獄界","餓鬼界",3),
        new QuickAnsGameQuestionData("最低的人生境界，處於毫無歡喜、希望，掉入絕望悲慘的境涯，是「十界」中的哪一界？","菩薩界","地獄界","餓鬼界",2),
        new QuickAnsGameQuestionData("認真信心活動之人，即使生活上遇到任何苦難與困難，也必定能「」","轉重輕受","諸天善神","佛法西還",1),
        new QuickAnsGameQuestionData("佛法最初由西方印度傳入中國進而傳到日本，而日蓮大聖人的佛法將會從日本傳回印度，最終傳遍全世界，此一道理稱為。","佛法東還","佛法西下","佛法西還",3),
        new QuickAnsGameQuestionData("良師益友，能夠引導眾生離惡修善，入於佛法的人或團體，都可稱為？","善知識","惡知識","善常識",1),
        new QuickAnsGameQuestionData("第一位在大聯盟中打出全壘打的台灣人是下列哪一位？","曹錦輝","陳金鋒","郭泓志",3),
        new QuickAnsGameQuestionData("卡通「我們這一家」主題曲的副歌旋律是緣自於下列哪一首古典樂曲？","埃爾家的威風堂堂進行曲","德布西的新世紀交響曲","韋瓦第的四季",1),
        new QuickAnsGameQuestionData("下列哪一部影片沒有得過金馬獎最佳劇情片獎？","桃姐","賽德克巴萊","功夫",1),
        new QuickAnsGameQuestionData("下列哪一個地點不是網球四大滿貫賽事舉辦的地方？","巴黎","墨爾本","邁阿密",3),
        new QuickAnsGameQuestionData("馬拉松賽所規定的長度為下列何者？","35公里又300米","42公里又195米","52公里",2),
        new QuickAnsGameQuestionData("有「世界屋脊」之稱的是？","塔里木盆地","青藏高原","柴達木盆地",2),
        new QuickAnsGameQuestionData("小明出國去玩，當地晚上九點，他發現天還是亮的，請問他最有可能在哪個國家？","英國","新加坡","墨西哥",1),
        new QuickAnsGameQuestionData("《三國志》的作者是誰？","羅貫中","曹雪芹","陳壽",3),
        new QuickAnsGameQuestionData("哈利波特小說中提到一個特別的月台，叫做什麼名字？","八又三分之一月台","七又七分之二月台","九又四分之三月台",3),
        new QuickAnsGameQuestionData("反詐騙電話是幾號 ？","166","165","113",2),
        new QuickAnsGameQuestionData("賣火柴的小女孩、醜小鴨、國王的新衣等都是哪一個童話作家的作品？","格林","迪士尼","安徒生",3),
        new QuickAnsGameQuestionData("下列哪一項不屬於銅管樂器？","長笛","長號","法國號",1),
        new QuickAnsGameQuestionData("請問和樂新聞中池田先生的指導文章通常放在第幾搬到第幾版？","4-6版","5-7版","6-8版",2)
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
        //離開遊戲
        ExitGame = 1,
        //角色選擇場景
        SureButton_RoleSelect = 1000, LeftArrow_RoleSelect = 1001, RightArrow_RoleSelect = 1002, StartGame_RoleSelect = 1003,

        //區域地圖場景
        MissionSure_Area = 2000, MissionCancel_Area = 2001, NextAreaMap = 2002, SureNextArea = 2003, CancelNextArea = 2004,
        NextGameStep = 3000,
        GameEnd = 4000, GameEnd_卡片掉了 = 4001,
        HandSomethingGame_Correct = 5000, HandSomethingGame_Giveup = 5001, ColorGame_ShowAnswer = 5002, ColorGame_Correct = 5003, ColorGame_Error = 5004, ReasoningGameNextHint = 5005, ReasoningGameShowAnswer = 5006,
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
        小畫家 = 41, 著名歌手 = 42, 音樂家 = 43, 村長 = 44 //第三座島嶼
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
        記憶對對碰 = 1, 顏不及意 = 2, 快問快答 = 3, 推理要在晚餐後 = 4, 支援前線_1 = 5,
        大家來找碴 = 6, 歌喉戰_1 = 7, 比手畫腳 = 8, 人人都是畢卡索_1 = 9, 排列組合 = 10,
        支援前線_2 = 11, 歌喉戰_2 = 12, 人人都是畢卡索_2 = 13
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