using Assets.Scripts.Placholders;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    class ClientManager : MonoBehaviour
    {
        public bool loggedIn;
        public Queue<string> networkActionQ;

        //Statistic updates
        public Queue<string> statNameQ;
        public Queue<string> statChangeQ;

        //Admin - updating player data
        public string newName;
        public string newBanned;
        public string newPassword;
        public string newEmail;

        //Current map for arena
        public int mapIndex;

        //Rank sprites
        public Sprite[] rankSprites;

        private MessageSender messageSender;
        private MenuManager menuManager;

        void Awake()
        {
            networkActionQ = new Queue<string>();
            statNameQ = new Queue<string>();
            statChangeQ = new Queue<string>();
        }
        void Start()
        {
            messageSender = (MessageSender)GetComponentInParent(typeof(MessageSender));
            menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        }
        //After an available server is sent back to the client execute the current network action
        public void ServerRequest(ServerRequest incCommand)
        {
            menuManager.currentStatus = "Connected";
            //All servers are offline but loadbalancer is online
            if (incCommand.getAvalIP() == "none")
            {
                menuManager.SetLoginStatus("FAILED - No online servers");
                return;
            }
            while (networkActionQ.Count != 0)
            {
                string networkAction = networkActionQ.Dequeue();
                switch (networkAction)
                {
                    case "login":
                        {
                            Debug.Log("Sending login details to " + incCommand.getAvalIP());
                            messageSender.SendLogin(menuManager.username, menuManager.password, incCommand.getAvalIP());
                        }
                        break;
                    case "savedata":
                        {
                            //Singleplayer menu - Need to get last played hero, map and score
                            Debug.Log("Retrieving save game data");
                            messageSender.GetSaveData(menuManager.username, incCommand.getAvalIP());
                        }
                        break;
                    case "achievementdata":
                        {
                            //Achievements - Retrieve all achievements
                            Debug.Log("Retrieving achievement data");
                            messageSender.GetAchievementData(menuManager.username, incCommand.getAvalIP());
                        }
                        break;
                    case "statdata":
                        {
                            //Statistics - Get this players stats
                            Debug.Log("Retrieving statistic data");
                            messageSender.GetStatData(menuManager.username, incCommand.getAvalIP());
                        }
                        break;
                    case "statupdate":
                        {
                            //Statistic - Sent when playing completes an action (winning/losing)
                            Debug.Log("Sending statistic data");
                            messageSender.SendStatData(menuManager.username, statNameQ.Dequeue(), statChangeQ.Dequeue(), incCommand.getAvalIP());
                        }
                        break;
                    case "registration":
                        {
                            //Register - Sending username/password/email
                            Debug.Log("Sending new registration");
                            messageSender.SendRegister(menuManager.username, menuManager.password, menuManager.email, incCommand.getAvalIP());
                        }
                        break;
                    case "forgotpass":
                        {
                            //Forgotpass - Sending email to which the password must be emailed to
                            Debug.Log("Retrieving lost password");
                            messageSender.SendForgotpass(menuManager.username, incCommand.getAvalIP());
                        }
                        break;
                    case "datarequest":
                        {
                            Debug.Log("Retrieving data");
                            messageSender.GetData(GameObject.Find("playernameToSearch").GetComponent<Text>().text, incCommand.getAvalIP());
                        }
                        break;
                    case "dataupdate":
                        {
                            Debug.Log("Updating data");
                            messageSender.UpdateData(newName, newBanned, newPassword, newEmail, incCommand.getAvalIP());
                        }
                        break;
                    case "killrequest":
                        {
                            Debug.Log("Retrieving kill count");
                            messageSender.GetKillCount(menuManager.username, incCommand.getAvalIP());
                        }
                        break;
                    default:
                        {
                            Debug.Log("ERROR: Not reciever for incoming message");
                        }
                        break;
                }
            }
        }

        public void LoginStatus(LoginStatus incCommand)
        {
            if (incCommand.getStatus() == "success")
            {
                //Retrieve data from server
                switch (incCommand.getError())
                {
                    case "none":
                        {
                            Debug.Log("Login succesful");
                            GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username = menuManager.username;
                            Debug.Log(GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username);
                            menuManager.SetLoginStatus("");
                            menuManager.EnableMenu("main");
                            loggedIn = true;
                        }
                        break;
                    case "regcomp":
                        {
                            Debug.Log("Registration succesful");
                            menuManager.SetLoginStatus("SUCCESS - Try logging in");
                        }
                        break;
                    case "checkmail":
                        {
                            Debug.Log("Check email");
                            menuManager.SetLoginStatus("SUCCESS - Check your email");
                        }
                        break;
                    case "dataupdate":
                        {
                            Debug.Log("Data successfully updated");
                            menuManager.SetLoginStatus("SUCCESS - Data has been updated");
                            GetAvalServer("datarequest");
                        }
                        break;
                }
            }
            else
            {
                switch (incCommand.getError())
                {
                    case "password":
                        {
                            menuManager.SetLoginStatus("FAILED - Password incorrect");
                            Debug.Log("Password incorrect");
                        }
                        break;
                    case "exists":
                        {
                            menuManager.SetLoginStatus("FAILED - User doesnt exist");
                            Debug.Log("User doesnt exist");
                        }
                        break;
                    case "banned":
                        {
                            menuManager.SetLoginStatus("FAILED - Banned");
                            Debug.Log("User is banned");
                        }
                        break;
                    case "userexsists":
                        {
                            menuManager.SetLoginStatus("FAILED - User already exists");
                            Debug.Log("Try another username");
                        }
                        break;
                    case "blankname":
                        {
                            menuManager.SetLoginStatus("FAILED - Username cant be blank");
                            Debug.Log("Blank username");
                        }
                        break;
                    case "blankpass":
                        {
                            menuManager.SetLoginStatus("FAILED - Password cant be blank");
                            Debug.Log("Blank password");
                        }
                        break;
                    case "blankemail":
                        {
                            menuManager.SetLoginStatus("FAILED - Email cant be blank");
                            Debug.Log("Blank email");
                        }
                        break;
                }

            }
        }
        //Process incoming server data
        public void SaveData(SaveData incCommand)
        {
            //Setup recieved save data
            GameObject.Find("ResourceManager").GetComponent<ResourceManager>().selectedMap = incCommand.lastMap;
            GameObject.Find("ResourceManager").GetComponent<ResourceManager>().selectedHero = incCommand.lastHero;
            menuManager.currentMenu = "";
            SceneManager.LoadScene(incCommand.lastMap);
        }
        public void AchievementData(AchievementData incCommand)
        {
            menuManager.SetLoginStatus("");
            GameObject achievementPanel = null;
            string achievementName = "";
            //Setup recieved achievement data
            for (int x = 1; x < incCommand.splitMessage.Length; x++)
            {
                achievementName = incCommand.splitMessage[x];
                try
                {
                    achievementPanel = GameObject.Find(achievementName + "_panel");
                    achievementPanel.transform.Find("color_achievement").GetComponent<Image>().enabled = true;

                    if (achievementName == "firelevel")
                    {
                        PlayerPrefs.SetInt("fire", 1);
                    }
                    if (achievementName == "waterlevel")
                    {
                        PlayerPrefs.SetInt("water", 1);
                    }
                }
                catch
                {
                    Debug.Log("ERROR: Cant find panel :" + achievementName + "_panel");
                }
            }
        }
        public void StatData(StatData incCommand)
        {
            menuManager.SetLoginStatus("");
            GameObject statPanel = null;
            string statName = "";
            //Setup recieved achievement data
            for (int x = 1; x < incCommand.splitMessage.Length; x++)
            {
                if (x % 2 != 0)
                {
                    statName = incCommand.splitMessage[x];
                }
                else
                {
                    statPanel = GameObject.Find("statistic_" + statName);
                    statPanel.transform.Find("text_statistic_description").GetComponent<Text>().text = incCommand.splitMessage[x];
                }
            }
        }
        public void DataRequest(DataRequest incCommand)
        {
            GameObject.Find("data_banned").GetComponent<Text>().text = incCommand.banned;
            GameObject.Find("data_password").GetComponent<Text>().text = incCommand.password;
            GameObject.Find("data_email").GetComponent<Text>().text = incCommand.email;
            menuManager.SetLoginStatus("SUCCESS - Player data retrieved");
        }
        public void KillRequest(KillRequest incCommand)
        {
            int kills = incCommand.killCount;
            if (kills == 0)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[0];
                GameObject.Find("player_rank").GetComponent<Text>().text = "House Elf";
            }
            if (kills > 0 && kills <= 10)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[1];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Wand Washer";
            }
            if (kills > 10 && kills <= 20)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[2];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Flying Pig";
            }
            if (kills > 20 && kills <= 30)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[3];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Apprentice";
            }
            if (kills > 30 && kills <= 40)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[4];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Student";
            }
            if (kills > 40 && kills <= 50)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[5];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Wizard";
            }
            if (kills > 50 && kills <= 60)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[6];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Grand Wizard";
            }
            if (kills > 60 && kills <= 70)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[7];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Archmage";
            }
            if (kills > 70 && kills <= 80)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[8];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Dumbledore";
            }
            if (kills > 80)
            {
                GameObject.Find("player_portrait").GetComponent<Image>().sprite = rankSprites[9];
                GameObject.Find("player_rank").GetComponent<Text>().text = "Gandalf";
            }
            menuManager.SetLoginStatus("");
        }

        //Utility Functions
        public void GetAvalServer(string networkAction)
        {
            if (networkAction == null)
            {
                return;
            }
            Debug.Log("Executing network action :" + networkAction);
            networkActionQ.Enqueue(networkAction);
            GameObject.Find("NetworkManager").GetComponent<MessageSender>().GetAvalServer();
        }
        private void StatChange(string statName)
        {
            statNameQ.Enqueue(statName);
            statChangeQ.Enqueue("1");
            GetAvalServer("statupdate");
        }

        //Stat update functions
        public void AddKill()
        {
            StatChange("kills");
        }
        public void AddMatch()
        {
            StatChange("matches");
        }
        public void AddSpell()
        {
            StatChange("spellsused");
        }
        public void AddMission()
        {
            StatChange("missionsdone");
        }
        public void AddWin()
        {
            StatChange("totalwins");
        }
        public void AddLoss()
        {
            StatChange("totallosses");
        }
        public void AddFireLevel()
        {
            StatChange("firelevel");
            PlayerPrefs.SetInt("fire", 1);
        }
        public void AddWaterLevel()
        {
            StatChange("waterlevel");
            PlayerPrefs.SetInt("water", 1);
        }
        public void AddVoodooLevel()
        {
            StatChange("badvoodoo");
        }
    }
}
