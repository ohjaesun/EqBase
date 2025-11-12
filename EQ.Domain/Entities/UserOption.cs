using EQ.Common.Logs;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Entities
{
    #region 설정 값들
    /// <summary>
    /// 사용자 옵션 정의
    /// </summary>
    public class UserOption
    {

        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Magazine 갯수")]
        public int Chip_MagazineCount { get; set; } = 1;
        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Magazine의 Tray 갯수")]
        public int Chip_TrayCount { get; set; } = 10;

        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Tray X ")]
        public int Chip_Tray_X { get; set; } = 20;
        [CategoryAttribute("ChipData")]
        [DescriptionAttribute("Tray Y ")]
        public int Chip_Tray_Y { get; set; } = 50;

    }

    public class UserOption2  // 네트워크 관련
    {
        public struct NetworkInfo
        {
            [ReadOnly(true)]
            public string Name { get; set; } // 네트워크 이름
            public string IP { get; set; }
            public int Port { get; set; }

            public NetworkInfo(string name, string ip, int port)
            {
                Name = name;
                IP = ip;
                Port = port;
            }
        }

        [CategoryAttribute("Vision")]
        [DescriptionAttribute("Global Vision")]
        public NetworkInfo[] GVision { get; set; } =
        {
            new NetworkInfo(GVisionType.GVisionTOP.ToString(), "192.168.0.1", 8080),
            new NetworkInfo(GVisionType.GVisionBOTTOM.ToString(), "192.168.0.2", 8081),
            new NetworkInfo(GVisionType.GVisionSIDE.ToString(), "192.168.0.3", 8082),
        };

        [CategoryAttribute("Vision")]
        [DescriptionAttribute("5초마다 자동 재연결 시도")]
        public bool AutoReconnect { get; set; } = true;
        [CategoryAttribute("Vision")]
        [DescriptionAttribute("Data에 STX ETX 붙임")]
        public bool ETX_Used { get; set; } = true;


    }
    public class UserOption3
    {
        public enum LanguageType
        {
            English,
            Korean,
        }

        [CategoryAttribute("언어")]
        [DescriptionAttribute("언어설정")]
        public LanguageType Language { get; set; } = LanguageType.English;
    }

    public class UserOption4 // 디버깅 옵션
    {
        [CategoryAttribute("SW Limit")]
        [DescriptionAttribute("모터 상대값 이동 Limit 해제")]
        public bool Motor_InterLock_Skip { get; set; } = false;


        [CategoryAttribute("TimeCheck(리소스 사용량 증가 됨)")]
        [DescriptionAttribute("모터 동작 예상 시간 로그 출력")]
        public bool MotionExpectationTimeOut { get; set; } = false;


        [CategoryAttribute("TimeCheck(리소스 사용량 증가 됨)")]
        [DescriptionAttribute("액션 실행 시간 저장")]
        public bool ActTimeCheck { get; set; } = false;

        [CategoryAttribute("Error")]
        [DescriptionAttribute("Sound 무음")]
        public bool ErrorSoundSkip { get; set; } = false;

        [CategoryAttribute("DoorOpen")]
        [DescriptionAttribute("Open시에도 동작 함")]
        public bool doorOpenSkip { get; set; } = false;

        [CategoryAttribute("Sequence")]
        [DescriptionAttribute("Sequence TimeOut")]
        public int MaxSequenceTime { get; set; } = 1000 * 60;

        [CategoryAttribute("Vision")]
        [DescriptionAttribute("Vision Skip")]
        public bool VisionSkip { get; set; } = false;


        [CategoryAttribute("SQLite")]
        [DescriptionAttribute("MBTI DB 저장")]
        public bool SqlMbtiSave { get; set; } = true;
        [CategoryAttribute("SQLite")]
        [DescriptionAttribute("Alarm 발생이력 DB 저장")]
        public bool SqlAlarmSave { get; set; } = true;

        [CategoryAttribute("SecsGem")]
        [DescriptionAttribute("SecsGem 사용여부")]
        public bool SecsGemSkip { get; set; } = false;

        [CategoryAttribute("Exception")]
        [DescriptionAttribute("에러 발생시 dump 파일 생성")]
        public bool ExceptionAbort { get; set; } = false;

    }

    public class UserOptionUI
    {
        [CategoryAttribute("Type")]
        [DescriptionAttribute("Type")]
        public Type uiType { get; set; } // 버튼인지 텍스트박스인지 등
        public string value { get; set; } // 값

        public string name { get; set; } // 이름

        public T GetValue<T>()
        {
            if (value == null)
                return default(T); 

            try
            {             
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
              Log.Instance.Error($"UserOptionUI GetValue Error : {ex.Message}");
                return default(T);
            }
        }
    }
    #endregion
}
