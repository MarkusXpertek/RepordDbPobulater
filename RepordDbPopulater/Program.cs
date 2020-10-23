
using RepordDbPopulater.DataBase;
using System;
using System.IO;
using System.Linq;

namespace RepordDbPopulater
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configiration
            
            string folderLocation =@"";
            // read config file
            string[] lines = File.ReadAllLines(@"./../../../config.txt");

            // debuging
            Console.WriteLine("Contents of config.txt = ");
            // iterate through lines in config file
            foreach (string line in lines)
            {
                // debuging
                Console.WriteLine("\t" + line);

                // seperate line into usable information
                string[] info = line.Split('>');
                // checks for Reportlacation in line
                if (info[0] == "ReportsLocation")
                {
                    // set folder location to lacation specified on the confic.txt file
                    folderLocation += info[1];

                    Console.WriteLine(folderLocation);
                }
             
            }



            // Process

            // array of folders named after branchId
            string[] BranchFolders = Directory.GetDirectories(folderLocation);

            // iterate through directory 
            foreach (var branch in BranchFolders)
            {
                // seperator for path split 
                char[] seperator = { '\\' };
                // debuging
                Console.WriteLine(branch.Split(seperator).Last());

                // get api User from db according to branchid
                ApiUser user = new ApiUser();
                using (var context = new DBContext())
                {
                    user = context.ApiUsers
                            .Where(b => b.BranchId == branch.Split(seperator).Last()).First();
                }


                // array of reports in folder
                string[] reports = Directory.GetFiles(Path.Combine(folderLocation, Path.GetFileNameWithoutExtension(branch)));
                // array of creditdatateport objects to be added to database
                CreditDataReport[] reportsDb = new CreditDataReport[reports.Length];

                // counter
                int index = 0;
                // iterate through report files
                foreach (var report in reports)
                {
                    // debuging
                    Console.WriteLine(report);
                    // new CreditDataReport object to be added to reportsDb array
                    CreditDataReport reportDb = new CreditDataReport();
                    reportDb.ApiUserId = user.id;
                    reportDb.PdfReport = File.ReadAllBytes(report);
                    reportDb.ReportName = Path.GetFileNameWithoutExtension(report);
                    reportDb.DateAdded = File.GetLastWriteTime(report);
                    reportDb.ActorCode = "";
                    reportDb.ConsumerFirstname = "";
                    reportDb.ConsumerIdentifier = "";
                    reportDb.SysUserId = "";
                    // add object to array
                    reportsDb[index] = reportDb;

                    index++;
                }


                // get xml reports folder
                string xmlDirectory = Directory.GetDirectories(Path.Combine(folderLocation, branch)).First();
                // array of reports in xml folder
                string[] xmlReports = Directory.GetFiles(Path.Combine(folderLocation, branch, Path.GetFileNameWithoutExtension(xmlDirectory)));
              
                // counter
                index = 0;
                // iterate through xml report files
                foreach (var xmlreport in xmlReports)
                {
                    // debuging
                    Console.WriteLine(xmlreport);

                    // adding report byte array from xml file to report object in reportsDb
                    reportsDb[index].XmlReport = File.ReadAllBytes(xmlreport);

                    // adding and saving database changes
                    using (var context = new DBContext())
                    {
                        context.Reports.Add(reportsDb[index]);
                        context.SaveChanges();
                    }
                    index++;
                }

            }
        }
    }
}