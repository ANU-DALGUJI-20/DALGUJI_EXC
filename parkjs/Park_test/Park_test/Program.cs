using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Park_test
{

    class order
    {
        public void exam(int num)
        {
            string str = "{{$MSGID=0001[!]CMD=INPUT_ORDER$}}{{$MSGID=0002[!]CMD=PROD_QRCODE[!]QRCODE=ABCDEFG$}}{{$MSGID=0003[!]CMD=INPUT_POS[!]X=1[!]Y=3$}}";
            string[] e = str.Split(new string[] { "{{$", "$}}" }, StringSplitOptions.None);

            string[] e1 = e[num].Split(new string[] { "[!]", "=" }, StringSplitOptions.None);


            if (num == 1)
            {
                Console.WriteLine($"message number-1");
                Console.WriteLine($"msgid : {e1[1]}, command : {e1[3]}");
            }
            else if (num == 3)
            {
                Console.WriteLine($"message number-2");
                Console.WriteLine($"msgid : {e1[1]} , command : {e1[3]}, QRCODE : {e1[5]}");

            }
            else
            {
                Console.WriteLine($"message number-3");
                Console.WriteLine($"msgid : {e1[1]}, command : {e1[3]}, X : {e1[5]}, Y : {e1[7]}");
            }
        }



    }

    class Input_Order : order
    {
        order q = new order();

        public void print()
        {
            q.exam(1);
        }
    }

    class PROD_QRCODE : order
    {

        order q = new order();

        public void print()
        {
            q.exam(3);
        }
    }


    class INPUT_POS : order
    {

        order q = new order();

        public void print()
        {
            q.exam(5);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Input_Order input = new Input_Order();
            PROD_QRCODE qrcode = new PROD_QRCODE();
            INPUT_POS pos = new INPUT_POS();
            input.print();
            qrcode.print();
            pos.print();
        }
    }
}


