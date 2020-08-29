using Lidgren.Network;
using runefighterz_loginserver.Commands;
using runefighterz_loginserver.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Handlers
{
    class MessageHandler
    {
        private LoginManager loginManager;
        public MessageHandler(NetServer netServer)
        {
            loginManager = new LoginManager(new MessageSender(netServer));
        }
        public void ProcessMessage(string incMessage, NetIncomingMessage netIncMessage)
        {
            try
            {
                string[] splitMessage = incMessage.Split('∰');
                switch (splitMessage[0])
                {
                    case "loginattempt":
                        {
                            Console.WriteLine("Login attempt recieved from " + netIncMessage.SenderEndPoint);
                            LoginAttempt incCommand = new LoginAttempt(netIncMessage.SenderEndPoint, splitMessage[1], splitMessage[2]);
                            loginManager.LoginAttempt(incCommand);
                        }
                        break;
                    case "savedata":
                        {
                            Console.WriteLine("Savegame request recieved from " + netIncMessage.SenderEndPoint);
                            SaveData incCommand = new SaveData(netIncMessage.SenderEndPoint, splitMessage[1]);
                            loginManager.SaveData(incCommand);

                        }
                        break;
                    case "achievementdata":
                        {
                            Console.WriteLine("Achievement data request recieved from " + netIncMessage.SenderEndPoint);
                            AchievementData incCommand = new AchievementData(netIncMessage.SenderEndPoint, splitMessage[1]);
                            loginManager.AchievementData(incCommand);

                        }

                        break;
                    case "statdata":
                        {
                            Console.WriteLine("Statistic data request recieved from " + netIncMessage.SenderEndPoint);
                            StatData incCommand = new StatData(netIncMessage.SenderEndPoint, splitMessage[1]);
                            loginManager.StatData(incCommand);

                        }
                        break;
                    case "statupdate":
                        {
                            Console.WriteLine("Update data request recieved from " + netIncMessage.SenderEndPoint);
                            StatUpdate incCommand = new StatUpdate(netIncMessage.SenderEndPoint, splitMessage[1], splitMessage[2], splitMessage[3]);
                            loginManager.UpdateStat(incCommand);
                        }
                        break;
                    case "register":
                        {
                            Console.WriteLine("New registration request from " + netIncMessage.SenderEndPoint);
                            Registration incCommand = new Registration(netIncMessage.SenderEndPoint, splitMessage[1], splitMessage[2], splitMessage[3]);
                            loginManager.Registration(incCommand);
                        }
                        break;
                    case "forgotpass":
                        {
                            Console.WriteLine("New forgotten password request from " + netIncMessage.SenderEndPoint);
                            Forgotpass incCommand = new Forgotpass(netIncMessage.SenderEndPoint, splitMessage[1]);
                            loginManager.Forgotpass(incCommand);
                        }
                        break;
                    case "datarequest":
                        {
                            Console.WriteLine("New data request from " + netIncMessage.SenderEndPoint);
                            DataRequest incCommand = new DataRequest(netIncMessage.SenderEndPoint, splitMessage[1]);
                            loginManager.DataRequest(incCommand);
                        }
                        break;
                    case "dataupdate":
                        {
                            Console.WriteLine("New data update from " + netIncMessage.SenderEndPoint);
                            DataUpdate incCommand = new DataUpdate(netIncMessage.SenderEndPoint, splitMessage[1], splitMessage[2], splitMessage[3], splitMessage[4]);
                            loginManager.DataUpdate(incCommand);
                        }
                        break;
                    case "killrequest":
                        {
                            Console.WriteLine("New data update from " + netIncMessage.SenderEndPoint);
                            KillRequest incCommand = new KillRequest(netIncMessage.SenderEndPoint, splitMessage[1]);
                            loginManager.KillRequest(incCommand);
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Unknown message recieved: " + splitMessage[0]);
                        }
                        break;
                }
            }
            catch
            {
                Console.WriteLine("ERROR: Message handling failed");
            }
        }
    }
}
