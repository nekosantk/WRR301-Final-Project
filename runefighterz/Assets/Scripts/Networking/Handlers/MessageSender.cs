using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class MessageSender : MonoBehaviour
    {
        private ConnectionManager connectionManager;
        private MenuManager menuManager;
        void Start()
        {
            connectionManager = (ConnectionManager)GetComponentInParent(typeof(ConnectionManager));
            menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        }
        public void GetAvalServer()
        {
            if (connectionManager == null)
            {
                return;
            }
            string command = "serverrequest";
            string clientIP = GameObject.Find("ResourceManager").GetComponent<ResourceManager>().loadBalancerIP;
            connectionManager.SendUnconnected(command, clientIP);
            menuManager.SetLoginStatus("Connecting...");
        }
        public void SendLogin(string username, string password, string avalServerIP)
        {
            string command = "loginattempt";
            connectionManager.SendUnconnected(command + "∰" + username + "∰" + password, avalServerIP);
            menuManager.SetLoginStatus("Sending login details...");
        }
        public void GetSaveData(string username, string avalServerIP)
        {
            string command = "savedata";
            connectionManager.SendUnconnected(command + "∰" + username, avalServerIP);
            menuManager.SetLoginStatus("Retrieving save data...");
        }
        public void GetAchievementData(string username, string avalServerIP)
        {
            string command = "achievementdata";
            connectionManager.SendUnconnected(command + "∰" + username, avalServerIP);
            menuManager.SetLoginStatus("Retrieving achievement data...");
        }
        public void GetStatData(string username, string avalServerIP)
        {
            string command = "statdata"; 
            connectionManager.SendUnconnected(command + "∰" + username, avalServerIP);
            menuManager.SetLoginStatus("Retrieving statistic data...");
        }
        public void SendStatData(string username, string statName, string statChange, string avalServerIP)
        {
            string command = "statupdate";
            connectionManager.SendUnconnected(command + "∰" + username + "∰" + statName + "∰" + statChange, avalServerIP);
        }
        public void SendRegister(string username, string password, string email, string avalServerIP)
        {
            string command = "register";
            connectionManager.SendUnconnected(command + "∰" + username + "∰" + password + "∰" + email, avalServerIP);
            menuManager.SetLoginStatus("Sending new registration...");
        }
        public void SendForgotpass(string username, string avalServerIP)
        {
            string command = "forgotpass";
            connectionManager.SendUnconnected(command + "∰" + username, avalServerIP);
            menuManager.SetLoginStatus("Sending lost password request...");
        }
        public void GetData(string username, string avalServerIP)
        {
            string command = "datarequest";
            connectionManager.SendUnconnected(command + "∰" + username, avalServerIP);
            menuManager.SetLoginStatus("Retrieving data...");
        }
        public void UpdateData(string username, string banned, string password, string email, string avalServerIP)
        {
            string command = "dataupdate";
            connectionManager.SendUnconnected(command + "∰" + username + "∰" + banned + "∰" + password + "∰" + email, avalServerIP);
            menuManager.SetLoginStatus("Updating data...");
        }
        public void GetKillCount(string username, string avalServerIP)
        {
            string command = "killrequest";
            connectionManager.SendUnconnected(command + "∰" + username, avalServerIP);
            menuManager.SetLoginStatus("Retrieving kill count...");
        }
    }
}