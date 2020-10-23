
using RepordDbPopulater.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RepordDbPopulater
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configurable
            string folderLocation = @"C:\Users\MarkusMadeleyn\source\repos\XpertekAcquire\CreditDataWebAPI\quickstart\docker-compose\reports";


            string[] Branches = Directory.GetDirectories(folderLocation);

            foreach (var branch in Branches)
            {
                    char[] seperator = { '\\' };
                Console.WriteLine(branch.Split(seperator).Last());

                ApiUser user = new ApiUser();
                using (var context = new DBContext())
                {
                        user = context.ApiUsers
                                .Where(b => b.BranchId == branch.Split(seperator).Last()).First();
                }

                string[] reports = Directory.GetFiles(Path.Combine(folderLocation, Path.GetFileNameWithoutExtension(branch)));

                CreditDataReport[] reportsDb = new CreditDataReport[reports.Length];
                int index = 0;
                foreach (var report in reports)
                {

                    Console.WriteLine(report);
                    // 
                    CreditDataReport reportDb = new CreditDataReport();
                    reportDb.ApiUserId = user.id;
                    reportDb.PdfReport = File.ReadAllBytes(report);
                    reportDb.ReportName = Path.GetFileNameWithoutExtension(report);
                    reportDb.DateAdded = File.GetLastWriteTime(report);
                    reportDb.ActorCode = "";
                    reportDb.ConsumerFirstname = "";
                    reportDb.ConsumerIdentifier = "";
                    reportDb.SysUserId = "";
                    reportsDb[index] = reportDb;

                    index++;
                }


                string xmlDirectory = Directory.GetDirectories(Path.Combine(folderLocation, branch)).First();
                string[] xmlReports = Directory.GetFiles(Path.Combine(folderLocation, branch, Path.GetFileNameWithoutExtension(xmlDirectory)));
                CreditDataReport tempRepport = new CreditDataReport();
                index = 0;
                foreach (var xmlreport in xmlReports)
                {
                    // debuging
                    Console.WriteLine(xmlreport);

                    reportsDb[index].XmlReport = File.ReadAllBytes(xmlreport);
                   
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