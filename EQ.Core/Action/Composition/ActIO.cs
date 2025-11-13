using EQ.Core.Actions;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Action
{
    public class ActIO : ActComponent
    {
        private IIoController _ioHardware;

        public ActIO(ACT act) : base(act) { }

        private Dictionary<string, int> OutputNameToIndex = new Dictionary<string, int>();
        private Dictionary<string, int> InputNameToIndex = new Dictionary<string, int>();
        /// <summary>
        /// 실제 HW 연결
        /// </summary>
        /// <param name="controller"></param>
        public void SetHardwareController(IIoController controller)
        {
            this._ioHardware = controller;

            _ioHardware.Init("");

            foreach (IO_OUT output in Enum.GetValues(typeof(IO_OUT)))
            {
                OutputNameToIndex.Add(output.ToString(), (int)output);
            }
            foreach (IO_IN input in Enum.GetValues(typeof(IO_IN)))
            {
                InputNameToIndex.Add(input.ToString(), (int)input);
            }
        }

        /// <summary>
        /// IO 상태 읽기
        /// </summary>
        /// <returns></returns>
        public (byte[] _input , byte[] _output) GetIoStatus()
        {
            if (this._ioHardware == null)
                return (null , null);

            return _ioHardware.GetCachedData();
        }

        /// <summary>
        /// Input 읽기
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ReadInput(IO_IN name)
        {
            return _ioHardware.ReadInput((int)name);
        }

        /// <summary>
        /// Output 읽기
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ReadOutput(IO_OUT name)
        {
            return _ioHardware.ReadOutput((int)name);
        }

        /// <summary>
        /// Input 쓰기 , 시뮬레이션 용
        /// </summary>
        /// <param name="name"></param>
        /// <param name="On"></param>
        public void WriteInput(IO_IN name, bool On)
        {
            _ioHardware.WriteInput((int)name, On ? (byte)1 : (byte)0);
        }
        /// <summary>
        /// output 쓰기
        /// </summary>
        /// <param name="name"></param>
        /// <param name="On"></param>
        public void WriteOutput(IO_OUT name, bool On)
        {
            _ioHardware.WriteOutput((int)name, On ? (byte)1 : (byte)0);

            //시뮬레이션용
            if (InputNameToIndex.TryGetValue(name.ToString(), out int index1))
            {
                _ioHardware.WriteInput(index1, On ? (byte)1 : (byte)0);
            }          
        }        
       

        /// <summary>
        /// 복동형의 신호 제어
        /// </summary>
        /// <param name="name">On 하려는 IO</param>
        /// <param name="checkInputIO">출력 신호를 볼 경우</param>
        /// <returns></returns>
        public async Task<ActionStatus> doubleTypeOn(IO_OUT name , bool checkInputIO = true)
        {
            var stepString = new List<string> { "Start", "FindSuffix", "IOChange", "WaitResult", "End" };

            IO_OUT onIO  = name;
            IO_OUT? offIO = null;

            return await _act.ExecuteAction(
                title: $"doubleTypeOn:[{name}]",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            await Task.Delay(1000);
                            nextStep++;
                            break;

                        case "FindSuffix":
                            {
                                string input = name.ToString();
                                int lastIndex = input.LastIndexOf('_');
                                if (lastIndex == -1 || lastIndex == input.Length - 1)
                                {
                                    nextStep++;
                                }

                                string prefix = input.Substring(0, lastIndex);                               
                                string suffix = input.Substring(lastIndex);
                               
                                string newSuffix = suffix switch
                                {
                                    "_ON" => "_OFF",
                                    "_OFF" => "_ON",
                                    "_PUSH" => "_BACK",
                                    "_BACK" => "_PUSH",
                                    _ => "None" 
                                };

                                if( newSuffix == "None")
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    string offIoName = prefix + newSuffix;
                                    if (OutputNameToIndex.TryGetValue(offIoName, out int index))                                                                        
                                        offIO = (IO_OUT)index;                                    
                                                                       
                                    nextStep++;
                                }
                            }
                            break;

                        case "IOChange":
                            {
                                WriteOutput(onIO, true);
                                if(offIO != null)
                                    WriteOutput((IO_OUT)offIO, false);

                                nextStep++;
                            }
                            break;

                            case "WaitResult":
                            {
                                if (checkInputIO)
                                {
                                    bool isOn = true;

                                    IO_IN? checkInputOn = null;
                                    IO_IN? checkInputOff = null;

                                    if (InputNameToIndex.TryGetValue(onIO.ToString(), out int index1))
                                    {
                                        checkInputOn = (IO_IN)index1;
                                        isOn = ReadInput((IO_IN)checkInputOn);
                                    }                                        

                                    if (InputNameToIndex.TryGetValue(offIO.ToString(), out int index2))
                                    {
                                        checkInputOff = (IO_IN)index2;
                                        isOn = !ReadInput((IO_IN)checkInputOff);
                                    }                                     

                                    if (isOn)                                    
                                        nextStep++;                                    
                                    else                                                                          
                                        await Task.Delay(10);                                    
                                }
                                else
                                {
                                    nextStep++;
                                }
                            }
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;

                        default: //정의 되지 않은 step
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );
        }
    }
}
