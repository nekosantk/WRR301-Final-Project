using Assets.Scripts.Managers;
using Assets.Scripts.Placholders;
using Lidgren.Network;
using UnityEngine;

namespace Assets.Scripts
{
    class MessageHandler : MonoBehaviour
    {
        private ClientManager clientManager;
        void Start()
        {
            clientManager = (ClientManager)GetComponentInParent(typeof(ClientManager));
        }
        public void ProcessMessage(string incMessage, NetIncomingMessage netIncMessage)
        {
            try
            {
                string[] splitMessage = incMessage.Split('∰');
                switch (splitMessage[0])
                {
                    case "serverrequest":
                        {
                            //ServerIP:Port
                            Debug.Log("Aval server recieved");
                            ServerRequest incCommand = new ServerRequest(splitMessage[1]);
                            clientManager.ServerRequest(incCommand);
                        }
                        break;
                    case "loginstatus":
                        {
                            //State - ErrorMessage
                            Debug.Log("Login status recieved");
                            LoginStatus incCommand = new LoginStatus(splitMessage[1], splitMessage[2]);
                            clientManager.LoginStatus(incCommand);
                        }
                        break;
                    case "savedata":
                        {
                            //Map - Hero - Score
                            Debug.Log("Save game recieved");
                            SaveData incCommand = new SaveData(splitMessage[1], splitMessage[2]);
                            clientManager.SaveData(incCommand);
                        }
                        break;
                    case "achievementdata":
                        {
                            //Name - Description
                            Debug.Log("Achievement data recieved");
                            AchievementData incCommand = new AchievementData(splitMessage);
                            clientManager.AchievementData(incCommand);
                        }
                        break;
                    case "statdata":
                        {
                            Debug.Log("Statistic data recieved");
                            StatData incCommand = new StatData(splitMessage);
                            clientManager.StatData(incCommand);
                        }
                        break;
                    case "datarequest":
                        {
                            Debug.Log("Requested data recieved");
                            DataRequest incCommand = new DataRequest(splitMessage[1], splitMessage[2], splitMessage[3]);
                            clientManager.DataRequest(incCommand);
                        }
                        break;
                    case "killrequest":
                        {
                            Debug.Log("Kill count recieved");
                            KillRequest incCommand = new KillRequest(splitMessage[1]);
                            clientManager.KillRequest(incCommand);
                        }
                        break;
                    default:
                        {
                            Debug.Log("Unknown message recieved: " + splitMessage[0]);
                        }
                        break;
                }
            }
            catch
            {
                Debug.Log("ERROR: BAD MESSAGE FORMAT: " + incMessage);
            }
        }
    }
}
