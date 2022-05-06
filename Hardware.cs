using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha_WindanginnAssist
{
    class Hardware
    {
        #region driving belt
        public const uint com_number = 5;
        public const uint speed_BaudRate = 9600;
        public const byte address = 15;
        public static uint Temp_issuer = 0; // a port, a function will give value to this 
        public static int drivingbeltConnection = 0;

        [DllImport("F1API.dll", EntryPoint = "F1_Connect")]
        public static extern int F1_Connect(uint dwPortnumber, uint BaudRate, Byte Address, ref uint issuer);

        [DllImport("F1API.dll", EntryPoint = "F1_Disconnect")]
        public static extern int F1_Disconnect(uint Issue);

        [DllImport("F1API.dll", EntryPoint = "F1_Reset")]
        public static extern int F1_Reset(uint Issue);

        [DllImport("F1API.dll", EntryPoint = "F1_SetBaudRate")]
        public static extern int F1_SetBaudRate(uint Issue, uint BaudRate);

        [DllImport("F1API.dll", EntryPoint = "F1_SetCommAddress")]
        public static extern int F1_SetCommAddress(uint Issue, Byte Address);

        [DllImport("F1API.dll", EntryPoint = "F1_GetCommAddress")]
        public static extern int F1_GetCommAddress(uint Issue, ref Byte highAddress);

        [DllImport("F1API.dll", EntryPoint = "F1_GetStatus")]
        public static extern int F1_GetStatus(uint Issue, ref uint Status);

        [DllImport("F1API.dll", EntryPoint = "F1_Dispense")]
        public static extern int F1_Dispense(uint Issue, Byte TheModel);

        [DllImport("F1API.dll", EntryPoint = "F1_Capture")]
        public static extern int F1_Capture(uint Issue);

        [DllImport("F1API.dll", EntryPoint = "F1_SetEntryMode")]
        public static extern int F1_SetEntryMode(uint Issue, Byte TheModel);

        [DllImport("F1API.dll", EntryPoint = "F1_GetEntryMode")]
        public static extern int F1_GetEntryMode(uint Issue, Byte TheModel);
        #endregion


        #region key cut

        const Int16 locktype = 5;// the locktype is  RF50 
        static int KeycutConnection = 0; // return error, 1 is successful access

        public static string RoomNumber = "001.002.00028";
        public string CheckInTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public static string CheckoutTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 12:00:00";


        [DllImport("LockSDK.dll", EntryPoint = "TP_Configuration")]
        public static extern int TP_Configuration(Int16 LockType);

        [DllImport("LockSDK.dll", EntryPoint = "TP_MakeGuestCardEx")]
        public static extern int TP_MakeGuestCardEx(StringBuilder card_snr, string room_no, string checkin_time, string checkout_time, Int16 iflags);

        [DllImport("LockSDK.dll", EntryPoint = "TP_ReadGuestCard")]
        public static extern int TP_ReadGuestCard(StringBuilder card_snr, StringBuilder room_no, StringBuilder checkin_time, StringBuilder checkout_time);

        [DllImport("LockSDK.dll", EntryPoint = "TP_ReadGuestCardEx")]
        public static extern int TP_ReadGuestCardEx(StringBuilder card_snr, StringBuilder room_no, StringBuilder checkin_time, StringBuilder checkout_time, ref int iflags);

        [DllImport("LockSDK.dll", EntryPoint = "TP_CancelCard")]
        public static extern int TP_CancelCard(StringBuilder card_snr);

        [DllImport("LockSDK.dll", EntryPoint = "TP_GetCardSnr")]
        public static extern int TP_GetCardSnr(StringBuilder card_snr);

        #endregion

        public void connection()
        {
            //// driving delt machine
            drivingbeltConnection = F1_Connect(com_number, speed_BaudRate, address, ref Temp_issuer);
            drivingbeltConnection = F1_Reset(Temp_issuer);
            drivingbeltConnection = F1_SetBaudRate(Temp_issuer, speed_BaudRate);
            drivingbeltConnection = F1_SetEntryMode(Temp_issuer, 0x32);

            if(drivingbeltConnection != 0)
            {
                MessageBox.Show("initialize the belt machine, Fail!");
            }

            //// keycut machine
            KeycutConnection = TP_Configuration(locktype);

            if (KeycutConnection != 1)
            {
                MessageBox.Show("initialize the key-cut machine, Fail!");
            }

        }

        public void disconnect ()
        {
            drivingbeltConnection = F1_Reset(Temp_issuer);
            drivingbeltConnection = F1_Disconnect(Temp_issuer);

        }

        public int keycut(string RoomNumber, int Addingday)
        {

            ////////// variable setting
            StringBuilder card_snr = new StringBuilder(100); //value setups
            string roomno = RoomNumber;
            string intime = CheckInTime;
            string outtime = DateTime.Now.AddDays(Addingday).ToString("yyyy-MM-dd") + " 12:00:00";
            short iflags = 9; // some features of lock system

            //////////
            drivingbeltConnection = F1_Capture(Temp_issuer);
            Thread.Sleep(2000);

            drivingbeltConnection = F1_Dispense(Temp_issuer, 0x36); // deliver to card to read pos

            Thread.Sleep(2000); // wait for 2 second

            int KeycutConnection = TP_MakeGuestCardEx(card_snr, roomno, intime, outtime, iflags); // cut the key

            if (KeycutConnection != 1)
            {
                System.Windows.Forms.MessageBox.Show("Key-cut machine, Fail!");
       
                return 177; //error return 
            }

            Thread.Sleep(1000); // wait for 1 second

            drivingbeltConnection = F1_Dispense(Temp_issuer, 0x34);//DISP_RETURN_TO_FRONT	0x34

            Thread.Sleep(800);

            drivingbeltConnection = F1_Dispense(Temp_issuer, 0x30);//eject card

            return 1;

        }

        ////////////////// support functions 
        ///
        public void notification_assist()
        {
            MessageBox.Show("please ring number 0416186308 to assistant");
        }




    }
}
