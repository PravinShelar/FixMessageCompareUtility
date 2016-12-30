using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace LogFileTestApplication
{
    class Program
    {
        #region Constants

        public const Char sohValue = '\u0001';
        public const Char keyValuePairSeparator = '=';
        public const Char configValueSeparator = ',';

        #endregion

        static void Main(string[] args)
        {
            //string filepath = @"C:\Users\Priyanka\Desktop\stg01_dswp3M historic 3Y_Pay1_20161219\stg01_dswp3M historic 3Y_Pay1_20161219.txt";
            //string filepath2 = @"C:\Users\Priyanka\Desktop\stg01_dswp3M historic 3Y_Pay1_20161219\stg51_dswp3M historic 3Y_Pay1_20161219.txt";

            string filepath = @"C:\Users\Priyanka\Desktop\stg01_dswp3M historic 3Y_Pay1_20161219\stg01_dswp6M historic 3Y_Pay2_20161219.txt";
            string filepath2 = @"C:\Users\Priyanka\Desktop\stg01_dswp3M historic 3Y_Pay1_20161219\stg51_dswp6M historic 3Y_Pay2_20161219.txt";

            string[] keyValuePairOfFirstFile = null, keyValuePairOfSecondFile = null;
            var contentsOne = File.ReadAllText(filepath);
            if (!string.IsNullOrEmpty(contentsOne))
            {
                keyValuePairOfFirstFile = contentsOne.Split(sohValue);
            }

            var contentsTwo = File.ReadAllText(filepath2);
            if (!string.IsNullOrEmpty(contentsTwo))
            {
                keyValuePairOfSecondFile = contentsTwo.Split(sohValue);
            }

            if (keyValuePairOfFirstFile != null && keyValuePairOfSecondFile != null)
            {
                Console.WriteLine(string.Format("Number of key value pairs in File - {0} : {1}", Path.GetFileName(filepath), keyValuePairOfFirstFile.Length));
                Console.WriteLine(string.Format("Number of key value pairs in File - {0} : {1}", Path.GetFileName(filepath2), keyValuePairOfSecondFile.Length));
                Console.WriteLine();

                int numberOfDifferences = 0;
                List<string> keysToIgnor = new List<string>(ConfigurationManager.AppSettings["keysToIngnor"].Split(configValueSeparator));

                // Number of key value pairs in both files should be same.

                if (keyValuePairOfFirstFile.Length == keyValuePairOfSecondFile.Length)
                {
                    for (int i = 1; i < keyValuePairOfFirstFile.Length; i++)
                    {
                        //Sequence of key value pairs in both files should be same.

                        if (string.Equals(keyValuePairOfFirstFile[i].Split(keyValuePairSeparator)[0].Trim(), keyValuePairOfSecondFile[i].Split(keyValuePairSeparator)[0].Trim()))
                        {
                            if (!keysToIgnor.Contains(keyValuePairOfFirstFile[i].Split(keyValuePairSeparator)[0].Trim()))
                            {
                                if (!string.Equals(keyValuePairOfFirstFile[i], keyValuePairOfSecondFile[i]))
                                {
                                    numberOfDifferences++;
                                    Console.WriteLine(string.Format("{0}. First File - [{1}], Second File - [{2}]", numberOfDifferences, keyValuePairOfFirstFile[i], keyValuePairOfSecondFile[i]));
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Files are not in proper sequence: {0}, {1}", keyValuePairOfFirstFile[i].Split(keyValuePairSeparator)[0].Trim(), keyValuePairOfSecondFile[i].Split(keyValuePairSeparator)[0].Trim()));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Need to check if this is valid scenario");
                }

                Console.WriteLine();
                Console.WriteLine(string.Format("Number of differences in both Files = {0}", numberOfDifferences));
            }

            Console.ReadLine();

        }
    }
}
