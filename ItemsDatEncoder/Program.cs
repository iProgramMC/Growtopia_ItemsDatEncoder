using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ItemsDatEncoder
{
    static class Program
    {
        static void WriteShort(this Stream stream, short value)
        {
            stream.WriteByte((byte)((value >> 0) & 0xff));
            stream.WriteByte((byte)((value >> 8) & 0xff));
        }
        static void WriteInt(this Stream stream, int value)
        {
            stream.WriteByte((byte)((value >>  0) & 0xff));
            stream.WriteByte((byte)((value >>  8) & 0xff));
            stream.WriteByte((byte)((value >> 16) & 0xff));
            stream.WriteByte((byte)((value >> 24) & 0xff));
        }
        static byte[] EncodeName(string name, int id)
        {
            byte[] b = new byte[name.Length];
            string key = "PBG892FXX982ABC*";
            for(int i = 0; i < name.Length; i++)
            {
                b[i] = (byte)((byte)name[i] ^ (byte)key[(i + id) % 16]);
            }
            return b;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Growtopia items.dat decoder (C) 2019 iProgramInCpp");
            Console.WriteLine("This program is licensed under the MIT license.");
            Console.WriteLine("View https://opensource.org/licenses/MIT for more info.");
            string fileRes = "items.dat";

            Stream stream = new FileStream(fileRes, FileMode.OpenOrCreate);
            //string[] a = File.ReadAllLines(folder + "base.txt");

            //byte unused = byte.Parse(a[0].Split('|')[1]);
            //int itemCount = int.Parse(a[1].Split('|')[1]);
            if (File.Exists("item_defs.txt"))
            {
                Console.WriteLine("item_defs.txt Found! Encoding...");
            }
            else
            {
                Console.WriteLine("item_defs.txt Not Found!\nPress any key to Quit");
                Console.ReadKey();
                Environment.Exit(0);
            }
            byte unused = 0x11;
            int itemCount = 0;

            char separator = '|';
            string[] lines = File.ReadAllLines("item_defs.txt");
            for(int ccc = 0; ccc < lines.Length; ccc++)
            {
                if (lines[ccc].Length < 2) continue;
                if (lines[ccc].StartsWith("//")) continue;
                string[] datas = lines[ccc].Split('\\');
                if (datas[0] != "add_item")
                {
                    if (datas[0] == "item_db_ver")
                    {
                        unused = byte.Parse(datas[1]);
                        // write them to the items.dat :upside_down:
                        stream.WriteByte(unused);
                        stream.WriteByte(0);
                    }
                    if (datas[0] == "item_count")
                    {
                        itemCount = int.Parse(datas[1]);
                        // write them to the items.dat :upside_down:
                        stream.WriteInt(itemCount);
                    }
                    continue;
                }
                if (datas.Length < 46) continue;
                // first, get all the item properties
                short itemID = 0;
                byte editableType = 0;
                byte itemCategory = 0;
                byte actionType = 0;
                byte hitSound = 0;
                short lengthItemName = 0;
                string decodedname = "";
                byte[] encodedName = new byte[0];
                short lengthFileName = 0;
                string filename = "";
                int texturehash = 0;
                byte itemKind = 0;
                int negativeOne = -1;
                byte textureX = 0;
                byte textureY = 0;
                byte spreadType = 0;
                byte isStripeyWallpaper = 0;
                byte collisionType = 0;
                byte hitsDestroy = 0;
                byte dropChance = 0;
                int clothingType = 0;
                short rarity = 0;
                byte toolKind = 0;
                short audioFileLen = 0;
                string audioFile = "";
                int audioHash = 0;
                short audioVolume = 0;
                byte seedBase = 0;
                byte seedOverlay = 0;
                byte treeBase = 0;
                byte treeLeaves = 0;

                byte[] seedColor = new byte[4];
                byte[] seedOverlayColor = new byte[4];
                short ingredient1 = 0;
                short ingredient2 = 0;
                int growTime = 0;
                int extraFieldInt1 = 0;

                string extraFieldUnk1 = "";
                string extraFieldUnk2 = "";
                string extraFieldUnk3 = "";
                string extraFieldUnk4 = "";
                string extraFieldUnk5 = "";

                string extraFieldUnk_1 = "";
                string extraFieldUnk_2 = "";
                string extraFieldUnk_3 = "";
                string extraFieldUnk_4 = "";
                string extraFieldUnk_5 = "";

                short unkValueShort1 = 0;
                short unkValueShort2 = 0;

                short value = 0;
                short value2 = 0;

                itemID = short.Parse(datas[1]);
                editableType = byte.Parse(datas[2]);
                itemCategory = byte.Parse(datas[3]);
                actionType = byte.Parse(datas[4]);
                hitSound = byte.Parse(datas[5]);
                decodedname = datas[6];
                encodedName = EncodeName(decodedname, itemID);
                lengthItemName = (short)encodedName.Length;
                filename = datas[7];
                lengthFileName = (short)filename.Length;
                texturehash = int.Parse(datas[8]);
                itemKind = byte.Parse(datas[9]);
                textureX = byte.Parse(datas[10]);
                textureY = byte.Parse(datas[11]);
                spreadType = byte.Parse(datas[12]);
                isStripeyWallpaper = byte.Parse(datas[13]);
                collisionType = byte.Parse(datas[14]);
                hitsDestroy = byte.Parse(datas[15]);
                dropChance = byte.Parse(datas[16]);
                clothingType = int.Parse(datas[17]);
                rarity = short.Parse(datas[18]);
                toolKind = byte.Parse(datas[19]);
                audioFile = datas[20];
                audioFileLen = (short)audioFile.Length;
                audioHash = int.Parse(datas[21]);
                audioVolume = short.Parse(datas[22]);
                seedBase = byte.Parse(datas[23]);
                seedOverlay = byte.Parse(datas[24]);
                treeBase = byte.Parse(datas[25]);
                treeLeaves = byte.Parse(datas[26]);

                // parse the colors
                seedColor = new byte[4];
                string[] datas2 = datas[27].Split(',');
                seedColor[0] = byte.Parse(datas2[0]);
                seedColor[1] = byte.Parse(datas2[1]);
                seedColor[2] = byte.Parse(datas2[2]);
                seedColor[3] = byte.Parse(datas2[3]);

                seedOverlayColor = new byte[4];
                string[] datas3 = datas[28].Split(',');
                seedOverlayColor[0] = byte.Parse(datas3[0]);
                seedOverlayColor[1] = byte.Parse(datas3[1]);
                seedOverlayColor[2] = byte.Parse(datas3[2]);
                seedOverlayColor[3] = byte.Parse(datas3[3]);

                ingredient1 = short.Parse(datas[29]);
                ingredient2 = short.Parse(datas[30]);
                growTime = int.Parse(datas[31]);

                extraFieldUnk1 = datas[32];
                extraFieldUnk2 = datas[33];
                extraFieldUnk3 = datas[34];
                extraFieldUnk4 = datas[35];
                extraFieldUnk5 = datas[36];

                extraFieldUnk_1 = datas[37];
                extraFieldUnk_2 = datas[38];
                extraFieldUnk_3 = datas[39];
                extraFieldUnk_4 = datas[40];
                extraFieldUnk_5 = datas[41];

                unkValueShort1 = short.Parse(datas[42]);
                unkValueShort2 = short.Parse(datas[43]);

                extraFieldInt1 = int.Parse(datas[44]);

                value = short.Parse(datas[45]);
                if (datas.Length > 46) value2 = short.Parse(datas[46]);

                // Now that we're done, write everything to the stream
                stream.WriteShort(itemID);
                stream.WriteByte(0);
                stream.WriteByte(0);
                stream.WriteByte(editableType);
                stream.WriteByte(itemCategory);
                stream.WriteByte(actionType);
                stream.WriteByte(hitSound);
                stream.WriteShort(lengthItemName);
                stream.Write(encodedName, 0, lengthItemName);
                stream.WriteShort(lengthFileName);
                stream.Write(Encoding.ASCII.GetBytes(filename), 0, lengthFileName);
                stream.WriteInt(texturehash);
                stream.WriteByte(itemKind);
                stream.WriteInt(-1);
                stream.WriteByte(textureX);
                stream.WriteByte(textureY);
                stream.WriteByte(spreadType);
                stream.WriteByte(isStripeyWallpaper);
                stream.WriteByte(collisionType);
                stream.WriteByte(hitsDestroy);
                stream.WriteByte(dropChance);
                stream.WriteByte((byte)((clothingType >> 24) & 0xff));
                stream.WriteByte((byte)((clothingType >> 16) & 0xff));
                stream.WriteByte((byte)((clothingType >>  8) & 0xff));
                stream.WriteByte((byte)((clothingType >>  0) & 0xff));
                stream.WriteShort(rarity);
                stream.WriteByte(toolKind);
                stream.WriteShort(audioFileLen);
                stream.Write(Encoding.ASCII.GetBytes(audioFile), 0, audioFileLen);
                stream.WriteInt(audioHash);
                stream.WriteShort(audioVolume);
                stream.WriteShort((short)extraFieldUnk1.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk1), 0, extraFieldUnk1.Length);
                stream.WriteShort((short)extraFieldUnk2.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk2), 0, extraFieldUnk2.Length);
                stream.WriteShort((short)extraFieldUnk3.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk3), 0, extraFieldUnk3.Length);
                stream.WriteShort((short)extraFieldUnk4.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk4), 0, extraFieldUnk4.Length);
                stream.WriteShort((short)extraFieldUnk5.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk5), 0, extraFieldUnk5.Length);
                stream.WriteByte(seedBase);
                stream.WriteByte(seedOverlay);
                stream.WriteByte(treeBase);
                stream.WriteByte(treeLeaves);
                stream.WriteByte(seedColor[0]);
                stream.WriteByte(seedColor[1]);
                stream.WriteByte(seedColor[2]);
                stream.WriteByte(seedColor[3]);
                stream.WriteByte(seedOverlayColor[0]);
                stream.WriteByte(seedOverlayColor[1]);
                stream.WriteByte(seedOverlayColor[2]);
                stream.WriteByte(seedOverlayColor[3]);
                stream.WriteShort(ingredient1);
                stream.WriteShort(ingredient2);
                stream.WriteInt(growTime);
                stream.WriteInt(extraFieldInt1);
                stream.WriteShort((short)extraFieldUnk_1.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk_1), 0, extraFieldUnk_1.Length);
                stream.WriteShort((short)extraFieldUnk_2.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk_2), 0, extraFieldUnk_2.Length);
                stream.WriteShort((short)extraFieldUnk_3.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk_3), 0, extraFieldUnk_3.Length);
                stream.WriteShort((short)extraFieldUnk_4.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk_4), 0, extraFieldUnk_4.Length);
                stream.WriteInt(0);
                stream.WriteShort(value);
                stream.WriteShort(value2);
                stream.WriteShort(unkValueShort1);
                for (int i = 0; i < 16 - value; i++) stream.WriteByte(0);
                stream.WriteShort(unkValueShort2);
                for (int i = 0; i < 50; i++) stream.WriteByte(0);
                stream.WriteShort((short)extraFieldUnk_5.Length);
                stream.Write(Encoding.ASCII.GetBytes(extraFieldUnk_5), 0, extraFieldUnk_5.Length);
                //for (int i = 0; i < 92; i++) stream.WriteByte(0);
                Console.WriteLine("Wrote item ID " + itemID + ".");
            }
            stream.Close();
        }
    }
}
