using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;                // Get these here https://aws.amazon.com/fr/sdk-for-net/
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Amazon;


namespace Excel2DynamoDb
{
    class Program
    {
        // Change the name of your DynamoDb Table here
        private static string tableName = "Sortilege";

        static void Main(string[] args)
        {
            try
            {
                AmazonDynamoDBClient client = Connect();
                Table SortilegeDb = Table.LoadTable(client, tableName);

                ReadCSVAndPopulateDb(SortilegeDb);
                
            }
            catch (AmazonDynamoDBException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Error To continue, press Enter");
                    Console.ReadLine();
                }   
            catch (AmazonServiceException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Error To continue, press Enter");
                    Console.ReadLine();
                }
            catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Error To continue, press Enter");
                    Console.ReadLine();
            }           
        }
        private static AmazonDynamoDBClient Connect()
        {
            // You need to install the AWS Tools for VS to manage this connect and add you secret keys
            // Check here: https://aws.amazon.com/fr/visualstudio/
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // This client will access the Paris region (please adapt)
            clientConfig.RegionEndpoint = RegionEndpoint.EUWest3;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);
            return client;
        }

        //Read CSV - Think about saving CSV as UTF8 !!
        private static void ReadCSVAndPopulateDb(Table SortilegeDb)
        {
            using (var rd = new StreamReader(@"C:\code\HarryWiki\spell_utf8.csv", Encoding.UTF8))
            {
                int i = 1;

                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(';');
                    // increment counter
                    i = i++;
                    // I have 3 columns in my CSV file, hence Splits 0,1,2. Adapt to your situation
                    CreateSpellItem(SortilegeDb, splits[0], splits[1], splits[2], i);
                }
            }
         }



        // Creates a Spell item
        private static void CreateSpellItem(Table SortilegeDb, string strID, string strSort, string strEffet, int iCounter)
        {
            Console.WriteLine(String.Format("\n*** Executing CreateSpellItem(). Record N°{0} ***",iCounter));
            var book = new Document();
            book["ID"] = Convert.ToInt32(strID); // ID is my Partition Key and type is Number (adapt)
            book["sort"] = strSort;
            book["effet"] = strEffet;

            SortilegeDb.PutItem(book);

            //System.Threading.Thread.Sleep(1000);
        }       

    }
}
