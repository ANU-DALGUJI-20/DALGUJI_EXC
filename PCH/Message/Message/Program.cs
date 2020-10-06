using System;

namespace Message
{
    class msgid_1
    {
        public string msgid;
        public string cmd;
    }

    class msgid_2
    {
        public string msgid;
        public string cmd;
        public string qrcode;
    }

    class msgid_3
    {
        public string msgid;
        public string cmd;
        public string x;
        public string y;
    }
    class Program
    {
        static void Main(string[] args)
        {
            string message = "{{$MSGID=0001[!]CMD=INPUT_ORDER$}}{{$MSGID=0002[!]CMD=PROD_QRCODE[!]QRCODE=ABCDEFG$}}{{$MSGID=0003[!]CMD=INPUT_POS[!]X=1[!]Y=3$}}";

            string[] words = message.Split(new string[] { "{{$", "=", "[!]", "$}}", "MSGID", "CMD", "X", "Y" }, StringSplitOptions.RemoveEmptyEntries);

            msgid_1 ms1 = new msgid_1();
            msgid_2 ms2 = new msgid_2();
            msgid_3 ms3 = new msgid_3();

            //foreach (var word in words)
            //{
            //    Console.WriteLine($"{word}");
            //}

            ms1.msgid = words[0];
            ms1.cmd = words[1];

            Console.WriteLine("message number - 1");
            Console.WriteLine($"msgid : {ms1.msgid}, command : {ms1.cmd}");

            ms2.msgid = words[2];
            ms2.cmd = words[3];
            ms2.qrcode = words[5];

            Console.WriteLine("message number - 2");
            Console.WriteLine($"msgid : {ms2.msgid}, command : {ms2.cmd}, QRCODE : {ms2.qrcode}");

            ms3.msgid = words[6];
            ms3.cmd = words[7];
            ms3.x = words[8];
            ms3.y = words[9];

            Console.WriteLine("message number - 3");
            Console.WriteLine($"msgid : {ms3.msgid}, command : {ms3.cmd}, X : {ms3.x}, Y : {ms3.y}");
        }
    }

}
