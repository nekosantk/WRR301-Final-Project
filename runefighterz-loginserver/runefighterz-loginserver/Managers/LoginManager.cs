using runefighterz_loginserver.Commands;
using runefighterz_loginserver.Handlers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace runefighterz_loginserver.Managers
{
    class LoginManager
    {
        private Timer heartbeatCheck;
        private MessageSender messageSender;
        private string loadBalancerIP;
        private string sqlUsername;
        private string sqlPassword;
        private string sqlServer;
        private string sqlDatabase;

        public LoginManager(MessageSender messageSender)
        {
            this.messageSender = messageSender;

            //Read all config parameters from config.ini
            string[] lines = File.ReadAllLines(@"config.ini", Encoding.UTF8);
            List<string> configParams = new List<string>();
            foreach (string x in lines)
            {
                configParams.Add(x.Split('=')[1]);
            }

            //Add each config parameter in order here
            loadBalancerIP = configParams[0];
            sqlUsername = configParams[1];
            sqlPassword = configParams[2];
            sqlServer = configParams[3];
            sqlDatabase = configParams[4];

            //Begin heartbeat checks
            heartbeatCheck = new Timer(HeartbeatCheck, null, 0, 5000);
        }
        private void HeartbeatCheck(object sender)
        {
            Console.WriteLine("Sending heartbeat...");
            messageSender.SendHeartbeat(loadBalancerIP);
        }

        //SERVER REQUESTS-------------
        public void LoginAttempt(LoginAttempt incCommand)
        {
            if (incCommand.getUsername() == "")
            {
                messageSender.SendLoginState("failure", "blankname", incCommand.getIpAddress());
                return;
            }
            if (incCommand.getPassword() == "")
            {
                messageSender.SendLoginState("failure", "blankpass", incCommand.getIpAddress());
                return;
            }
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                       ";password=" + sqlPassword +
                                       ";server=" + sqlServer +
                                       ";Trusted_Connection=yes" +
                                       ";database=" + sqlDatabase +
                                       ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            string dbPassword = "";
            string banned = "";
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from logindeets where CONVERT(VARCHAR,username)='" +
                    incCommand.getUsername() + "'", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    banned = myReader["banned"].ToString();
                    dbPassword = myReader["password"].ToString();
                }
                if (incCommand.getPassword() == dbPassword)
                {
                    if (banned == "0")
                    {
                        messageSender.SendLoginState("success", "none", incCommand.getIpAddress());
                        Console.WriteLine("Login success by:" + incCommand.getUsername());
                    }
                    else
                    {
                        messageSender.SendLoginState("failure", "banned", incCommand.getIpAddress());
                        Console.WriteLine("Login success by :[Banned]" + incCommand.getUsername());
                    }
                }
                else
                {
                    if (dbPassword == "")
                    {
                        messageSender.SendLoginState("failure", "exists", incCommand.getIpAddress());
                        Console.WriteLine("Login failure by :" + incCommand.getUsername());
                    }
                    else
                    {
                        messageSender.SendLoginState("failure", "password", incCommand.getIpAddress());
                        Console.WriteLine("Login failure by :" + incCommand.getUsername());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void SaveData(SaveData incCommand)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                       ";password=" + sqlPassword +
                                       ";server=" + sqlServer +
                                       ";Trusted_Connection=yes" +
                                       ";database=" + sqlDatabase +
                                       ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            string lastMap = "";
            string lastHero = "";
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from savedata where CONVERT(VARCHAR,username)='" + incCommand.getUsername() + "'", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    lastMap = myReader["lastMap"].ToString();
                    lastHero = myReader["lastHero"].ToString();
                }
                if (lastMap != "")
                {
                    //Return save data
                    messageSender.SendSaveData(lastMap, lastHero, incCommand.getIpAddress());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void AchievementData(AchievementData incCommand)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                       ";password=" + sqlPassword +
                                       ";server=" + sqlServer +
                                       ";Trusted_Connection=yes" +
                                       ";database=" + sqlDatabase +
                                       ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select logindeets.username, achievements.name, achievements.description" +
                    " from logindeets, achievementData, achievements" +
                    " where CONVERT(VARCHAR,logindeets.username)='" + incCommand.getUsername() + "'" +
                    " and achievementData.AchievementID=achievements.id" +
                    " and logindeets.id=achievementData.UserID", myConnection);

                myReader = myCommand.ExecuteReader();
                List<string> messageToSend = new List<string>();
                while (myReader.Read())
                {
                    messageToSend.Add(myReader["name"].ToString());
                }
                //Send Data to client
                messageSender.SendAchievementData(messageToSend, incCommand.getIpAddress());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void StatData(StatData incCommand)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                       ";password=" + sqlPassword +
                                       ";server=" + sqlServer +
                                       ";Trusted_Connection=yes" +
                                       ";database=" + sqlDatabase +
                                       ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlDataReader myReader = null;
                Console.WriteLine(incCommand.getUsername());
                SqlCommand myCommand = new SqlCommand("select logindeets.username, statisticBase.statName, statisticData.CurTally" +
                    " from statisticBase, statisticData, logindeets" +
                    " where CONVERT(VARCHAR,logindeets.username)='" + incCommand.getUsername() + "'" +
                    " and statisticData.UserID=logindeets.id" +
                    " and statisticBase.id=statisticData.StatisticID", myConnection);

                myReader = myCommand.ExecuteReader();
                List<string> messageToSend = new List<string>();
                while (myReader.Read())
                {
                    messageToSend.Add(myReader["statName"].ToString());
                    messageToSend.Add(myReader["CurTally"].ToString());
                }
                //Send Data to client
                messageSender.SendStatData(messageToSend, incCommand.getIpAddress());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void UpdateStat(StatUpdate incCommand)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                      ";password=" + sqlPassword +
                                      ";server=" + sqlServer +
                                      ";Trusted_Connection=yes" +
                                      ";database=" + sqlDatabase +
                                      ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("update statisticData" +
                    " set CurTally+='" + incCommand.statChange + "'" +
                    " from logindeets, statisticData, statisticBase" +
                    " where CONVERT(VARCHAR,logindeets.username)='" + incCommand.getUsername() + "'" +
                    " and statisticData.UserID=logindeets.id" +
                    " and statisticBase.id=statisticData.StatisticID" +
                    " and CONVERT(VARCHAR,statisticBase.statName)='" + incCommand.statName + "'", myConnection);

                myReader = myCommand.ExecuteReader();
                AchievementChecKStart(incCommand.getUsername());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Registration(Registration incCommand)
        {
            if (incCommand.getUsername() == "")
            {
                messageSender.SendLoginState("failure", "blankname", incCommand.getIpAddress());
                return;
            }
            if (incCommand.password == "")
            {
                messageSender.SendLoginState("failure", "blankpass", incCommand.getIpAddress());
                return;
            }
            if (incCommand.email == "")
            {
                messageSender.SendLoginState("failure", "blankemail", incCommand.getIpAddress());
                return;
            }
            if (ExistsUser(incCommand.getUsername()))
            {
                messageSender.SendLoginState("failure", "userexsists", incCommand.getIpAddress());
                return;
            }
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                         ";password=" + sqlPassword +
                         ";server=" + sqlServer +
                         ";Trusted_Connection=yes" +
                         ";database=" + sqlDatabase +
                         ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //Add logindeets
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("insert into logindeets" +
                    " values ('" + incCommand.getUsername() + "','" + incCommand.password + "','0'," + "'" + incCommand.email + "')", myConnection);

                myReader = myCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
            for (int x = 1; x <= 9; x++)
            {
                AddStatistic(GetUserID(incCommand.getUsername()), x);
            }
            messageSender.SendLoginState("success", "regcomp", incCommand.getIpAddress());
        }
        public void Forgotpass(Forgotpass incCommand)
        {
            if (incCommand.getUsername() == "")
            {
                messageSender.SendLoginState("failure", "blankname", incCommand.getIpAddress());
                return;
            }
            if (!ExistsUser(incCommand.getUsername()))
            {
                messageSender.SendLoginState("failure", "exists", incCommand.getIpAddress());
                return;
            }
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                         ";password=" + sqlPassword +
                         ";server=" + sqlServer +
                         ";Trusted_Connection=yes" +
                         ";database=" + sqlDatabase +
                         ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            string email = "";
            string password = "";
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from logindeets where CONVERT(VARCHAR,username)='" +
                    incCommand.getUsername() + "'", myConnection);

                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    password = myReader["password"].ToString();
                    email = myReader["email"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Forgotten password -> " + email);
            SendEmail(incCommand.getUsername(), password, email);
            messageSender.SendLoginState("success", "checkmail", incCommand.getIpAddress());
        }
        public void DataRequest(DataRequest incCommand)
        {
            if (incCommand.getUsername() == "")
            {
                messageSender.SendLoginState("failure", "blankname", incCommand.getIpAddress());
                return;
            }
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                       ";password=" + sqlPassword +
                                       ";server=" + sqlServer +
                                       ";Trusted_Connection=yes" +
                                       ";database=" + sqlDatabase +
                                       ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                List<string> messageToSend = new List<string>();

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from logindeets where CONVERT(VARCHAR,username)='" +
                    incCommand.getUsername() + "'", myConnection);
                myReader = myCommand.ExecuteReader();
                string password = "";
                while (myReader.Read())
                {
                    messageToSend.Add(myReader["banned"].ToString());
                    password = myReader["banned"].ToString();
                    messageToSend.Add(myReader["password"].ToString());
                    messageToSend.Add(myReader["email"].ToString());
                }
                if (password == "")
                {
                    messageSender.SendLoginState("failure", "exists", incCommand.getIpAddress());
                }
                else
                {
                    messageSender.SendDataRequest(messageToSend, incCommand.getIpAddress());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void DataUpdate(DataUpdate incCommand)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                      ";password=" + sqlPassword +
                                      ";server=" + sqlServer +
                                      ";Trusted_Connection=yes" +
                                      ";database=" + sqlDatabase +
                                      ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                List<string> messageToSend = new List<string>();

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("update logindeets" +
                    " set banned='" + incCommand.banned + "', password='" + incCommand.password + "', email='" + incCommand.email + "'" +
                    " from logindeets" +
                    " where CONVERT(VARCHAR,username)='" + incCommand.username + "'",  myConnection);
                myReader = myCommand.ExecuteReader();
                messageSender.SendLoginState("success", "dataupdate", incCommand.getIpAddress());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void KillRequest(KillRequest incCommand)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                           ";password=" + sqlPassword +
                           ";server=" + sqlServer +
                           ";Trusted_Connection=yes" +
                           ";database=" + sqlDatabase +
                           ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlDataReader myReader = null;
                Console.WriteLine(incCommand.getUsername());
                SqlCommand myCommand = new SqlCommand("select logindeets.username, statisticBase.statName, statisticData.CurTally" +
                    " from statisticBase, statisticData, logindeets" +
                    " where CONVERT(VARCHAR,logindeets.username)='" + incCommand.getUsername() + "'" +
                    " and statisticData.UserID=logindeets.id" +
                    " and statisticBase.id=statisticData.StatisticID" +
                    " and statisticData.StatisticID='1'", myConnection);

                myReader = myCommand.ExecuteReader();
                string killCount = "";
                while (myReader.Read())
                {
                     killCount = myReader["CurTally"].ToString();
                }
                //Send Data to client
                messageSender.SendKillCount(killCount, incCommand.getIpAddress());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //INTERNAL FUNCTIONS
        private void AchievementChecKStart(string username)
        {
            AchievementCheck(username, "kills");
            AchievementCheck(username, "matches");
            AchievementCheck(username, "spellsused");
            AchievementCheck(username, "missionsdone");
            AchievementCheck(username, "totalwins");
            AchievementCheck(username, "totallosses");
            AchievementCheck(username, "firelevel");
            AchievementCheck(username, "waterlevel");
            AchievementCheck(username, "badvoodoo");
        }
        private void AchievementCheck(string username, string statName)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                          ";password=" + sqlPassword +
                          ";server=" + sqlServer +
                          ";Trusted_Connection=yes" +
                          ";database=" + sqlDatabase +
                          ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlDataReader myReader = null;

                SqlCommand myCommand = new SqlCommand("select logindeets.id, logindeets.username, statisticData.curTally, statisticBase.statName" +
                    " from statisticData, logindeets, statisticBase" +
                    " where CONVERT(VARCHAR,logindeets.username)='" + username + "'" +
                    " and statisticData.UserID=logindeets.id" +
                    " and statisticBase.id=statisticData.StatisticID" +
                    " and CONVERT(VARCHAR,statisticBase.statName)='" + statName + "'", myConnection);

                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    string userID = myReader["id"].ToString();
                    switch (statName)
                    {
                        case "kills":
                            {
                            }
                            break;
                        case "matches":
                            {
                            }
                            break;
                        case "spellsused":
                            {
                            }
                            break;
                        case "missionsdone":
                            {
                            }
                            break;
                        case "totalwins":
                            {
                                Int32.TryParse(myReader["CurTally"].ToString(), out int conTally);
                                Console.WriteLine(myReader["CurTally"].ToString());
                                if (conTally == 1 && ExistsAchievement(userID, "1"))
                                    GiveAchievement(userID, "1");
                                if (conTally == 10 && ExistsAchievement(userID, "2"))
                                    GiveAchievement(userID, "2");
                                if (conTally == 20 && ExistsAchievement(userID, "3"))
                                    GiveAchievement(userID, "3");
                                if (conTally == 40 && ExistsAchievement(userID, "4"))
                                    GiveAchievement(userID, "4");
                                if (conTally == 80 && ExistsAchievement(userID, "5"))
                                    GiveAchievement(userID, "5");
                                if (conTally == 160 && ExistsAchievement(userID, "6"))
                                    GiveAchievement(userID, "6");
                                if (conTally == 200 && ExistsAchievement(userID, "7"))
                                    GiveAchievement(userID, "7");
                            }
                            break;
                        case "totallosses":
                            {
                            }
                            break;
                        case "firelevel":
                            {
                                Int32.TryParse(myReader["CurTally"].ToString(), out int conTally);
                                if (conTally > 0 && ExistsAchievement(userID, "8"))
                                {
                                    GiveAchievement(userID, "8");
                                }
                            }
                            break;
                        case "waterlevel":
                            {
                                Int32.TryParse(myReader["CurTally"].ToString(), out int conTally);
                                if (conTally > 0 && ExistsAchievement(userID, "9"))
                                {
                                    GiveAchievement(userID, "9");
                                }
                            }
                            break;
                        case "badvoodoo":
                            {
                                Int32.TryParse(myReader["CurTally"].ToString(), out int conTally);
                                if (conTally > 0 && ExistsAchievement(userID, "10"))
                                {
                                    GiveAchievement(userID, "10");
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private bool ExistsAchievement(string userID, string achievementID)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                          ";password=" + sqlPassword +
                          ";server=" + sqlServer +
                          ";Trusted_Connection=yes" +
                          ";database=" + sqlDatabase +
                          ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                /*
                SELECT achievementData.UserID, achievementData.AchievementID
                FROM achievementData
                WHERE achievementData.UserID = '1'
                AND achievementData.AchievementID = '4'
                */
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select achievementData.UserID, achievementData.achievementID" +
                    " from achievementData" +
                    " where achievementData.UserID='" + userID + "'" + 
                    " and achievementData.AchievementID='" + achievementID + "'", myConnection);

                myReader = myCommand.ExecuteReader();
                string temp = "";
                while (myReader.Read())
                {
                    temp = myReader["UserID"].ToString();
                }
                if (temp != "")
                {
                    if (temp == "")
                    {
                        //Doesnt exist yet
                        Console.WriteLine("Adding achievement");
                        return true;
                    }
                    else
                    {
                        //Exists
                        Console.WriteLine("Achievement exists");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }
        private void GiveAchievement(string userID, string achievementID)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                         ";password=" + sqlPassword +
                         ";server=" + sqlServer +
                         ";Trusted_Connection=yes" +
                         ";database=" + sqlDatabase +
                         ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                SqlDataReader myReader = null;

                SqlCommand myCommand = new SqlCommand("insert into achievementData" +
                    " values ('" + userID + "', '" + achievementID + "')", myConnection);

                myReader = myCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private bool ExistsUser(string username)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                       ";password=" + sqlPassword +
                                       ";server=" + sqlServer +
                                       ";Trusted_Connection=yes" +
                                       ";database=" + sqlDatabase +
                                       ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            string dbPassword = "";
            string banned = "";
            try
            {
                SqlDataReader myReader = null;
                //CONVERT(VARCHAR, empname)
                //SqlCommand myCommand = new SqlCommand("select * from logindeets where username ='%" + incCommand.getUsername() + "%'" , myConnection);
                SqlCommand myCommand = new SqlCommand("select * from logindeets where CONVERT(VARCHAR,username)='" + username + "'", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    //Console.WriteLine(myReader["id"].ToString());
                    //Console.WriteLine(myReader["username"].ToString());
                    //Console.WriteLine(myReader["password"].ToString());
                    dbPassword = myReader["password"].ToString();
                    banned = myReader["banned"].ToString();
                }
                if (dbPassword != "")
                {
                    //User Doesnt Exist
                    return true;
                }
                else
                {
                    //User Exists
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }
        private string GetUserID(string username)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                                      ";password=" + sqlPassword +
                                      ";server=" + sqlServer +
                                      ";Trusted_Connection=yes" +
                                      ";database=" + sqlDatabase +
                                      ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            string userID = "";
            try
            {
                SqlDataReader myReader = null;
                //CONVERT(VARCHAR, empname)
                //SqlCommand myCommand = new SqlCommand("select * from logindeets where username ='%" + incCommand.getUsername() + "%'" , myConnection);
                SqlCommand myCommand = new SqlCommand("select * from logindeets where CONVERT(VARCHAR,username)='" + username + "'", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    //Console.WriteLine(myReader["id"].ToString());
                    //Console.WriteLine(myReader["username"].ToString());
                    //Console.WriteLine(myReader["password"].ToString());
                    userID = myReader["id"].ToString();
                }
                return userID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }
        private void AddStatistic(string userID, int statID)
        {
            SqlConnection myConnection = new SqlConnection("user id=" + sqlUsername +
                         ";password=" + sqlPassword +
                         ";server=" + sqlServer +
                         ";Trusted_Connection=yes" +
                         ";database=" + sqlDatabase +
                         ";connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //Add statistics
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("insert into statisticData" +
                    " values ('" + userID + "','" + statID + "','" + "0')", myConnection);

                myReader = myCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void SendEmail(string username, string password, string email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("runefighterz@gmail.com");
                mail.To.Add(email);
                mail.Subject = "RuneFighterz - Lost Password";
                mail.Body = @"Hi there " + username + "!\n" +
                    "\n" +
                    "You are recieving this email because someone has requsted a lost password\n" +
                    "that is linked to this email.\n" +
                    "If you did not request this please ignore this email.\n" +
                    "\n" +
                    "USERNAME :" + username +
                    "\n" +
                    "PASSWORD :" + password +
                    "\n" +
                    "\n" +
                    "Good hunting stalker!\n";

                  SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("runefighterz", "TestMyNet22");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Console.WriteLine("Email sent to :" + email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
 