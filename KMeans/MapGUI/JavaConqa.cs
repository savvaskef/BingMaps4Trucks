using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using classesJv;

namespace MapGUI
{
    public class JavaConqa
    {
        public static bool WriteLnInSw(StreamWriter sw, string ln)
        {
            // Create an instance of StreamWriter to write text to a file.
            // The using statement also closes the StreamWriter.
            try
            {
                sw.WriteLine(ln);
                return true;
            }
            catch (System.Exception sex)
            { return false; }

        }
        public static bool ReadLnInSr(StreamReader sr, ref string ln)
        {
            // Create an instance of StreamWriter to write text to a file.
            // The using statement also closes the StreamWriter.
            try
            {
                ln = sr.ReadLine();
                return true;
            }
            catch (System.Exception sex)
            { return false; }

        }
        
        public static void JavaBuilder(List<string>route,cxy home,List<cxy>points,cxy fin){
            string cumPoints = ""; cxy cxypoint = new cxy();
            string cumFlightPoints = ""; int sidx = 0;
            int Pcounter=0;double Psumx=0;double Psumy=0;
            List<List<string>> stops=new List<List<string>>();
            List<string>onestop=new List<string>();


            cumPoints +="points=["+ (char)34 + home.x.ToString().Replace(",", ".") + "," + home.y.ToString().Replace(",", ".") + (char)34 + ",";
            cumFlightPoints += "  var flightPlanCoordinates = [new GLatLng( " + home.x.ToString().Replace(",", ".") + "," + home.y.ToString().Replace(",", ".") + "),";
            onestop.Add("0"); onestop.Add("0"); onestop.Add("0"); onestop.Add(home.x.ToString().Replace(",", ".")); onestop.Add(home.y.ToString().Replace(",", "."));
            onestop.Add("Αφετηρία");onestop.Add("start.jpg");
            stops.Add(onestop);
            
            
            
            foreach(string point in route){
                cxypoint = GetPoint(point, points);
                Psumx+=cxypoint.x;Psumy+=cxypoint.y;Pcounter+=1;
                sidx++;


                cumPoints += (char)34 + cxypoint.x.ToString().Replace(",", ".") + "," + cxypoint.y.ToString().Replace(",", ".") + (char)34 + ",";
                cumFlightPoints += "new GLatLng( " + cxypoint.x.ToString().Replace(",", ".") + "," + cxypoint.y.ToString().Replace(",", ".") + "),";
                onestop = new List<string>();
                onestop.Add("0"); onestop.Add("0"); onestop.Add("0"); onestop.Add(cxypoint.x.ToString().Replace(",", ".")); onestop.Add(cxypoint.y.ToString().Replace(",", "."));
                onestop.Add("στάση " +sidx); onestop.Add("cow.png");
                stops.Add(onestop);
            


            }

            cumPoints += (char)34 + fin.x.ToString().Replace(",", ".") + "," + fin.y.ToString().Replace(",", ".") + (char)34+"];";
            cumFlightPoints += "new GLatLng( " + fin.x.ToString().Replace(",", ".") + "," + fin.y.ToString().Replace(",", ".") + ")];";
            onestop = new List<string>();
            onestop.Add("0"); onestop.Add("0"); onestop.Add("0"); onestop.Add(fin.x.ToString().Replace(",", ".")); onestop.Add(fin.y.ToString().Replace(",", "."));
            onestop.Add("τερμα"); onestop.Add("fin.jpg");
            stops.Add(onestop);


            string avgstring = (Psumx / Pcounter).ToString().Replace(",", ".") + "," + (Psumy / Pcounter).ToString().Replace(",", ".");
            Console.WriteLine(cumPoints); Console.WriteLine(cumFlightPoints);
            bool ok=WrapPoints(cumPoints,"Δρομολόγιο φορτηγού",avgstring,stops,cumFlightPoints,false);
                }

        public static cxy GetPoint(string withID, List<cxy> FromPoints)
        {
            cxy dummy = new cxy();

            foreach (cxy p in FromPoints)
            {
                if (p.ID.ToUpper() == withID.ToUpper())
                {
                    dummy = p;
                    break;
                }
            }

            return dummy;
        }



        public static bool WrapPoints(string pntString, string TitleString, string centerString, List<List<String>> stopArr, string polypathString, bool snap2Road)
        {
            string p = Application.StartupPath;
            StreamWriter sw = new StreamWriter(p + "\\RoadMap3.htm");
            bool proceed;
            string ln; string selected;

            try
            {

                ln = "notnull";
                using (StreamReader sr = new StreamReader(p + "\\before3.a.txt"))
                {

                    while (ln != null)
                    {
                        proceed = ReadLnInSr(sr, ref ln);
                        if (ln == "    <title>Route & Course</title>")
                        {
                            ln = "    <title>" + TitleString + "</title>";
                        }

                        proceed = WriteLnInSw(sw, ln);
                    }
                    sr.Close();
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i <= stopArr.Count - 1; i++)
                {
                    ln = "notnull";
                    using (StreamReader sr = new StreamReader(p + "\\infowindow3.b.txt"))
                    {

                        while (ln != null)
                        {
                            proceed = ReadLnInSr(sr, ref ln);
                            if (ln == null) { break; }
                            ln = ln.Replace("retDir1(myLL)", "retDir" + i + "(myLL)");
                            ln = ln.Replace("imagefile", stopArr[i][6]);
                            ln = ln.Replace("stopdesc", stopArr[i][5]);
                            ln = ln.Replace("Address", "Διεύθυνση");
                            ln = ln.Replace("timeWthn", "<b>Στάση " + stopArr[i][2] + " λεπτών</b>");
                            ln = ln.Replace("tillhereLabel", "Μέχρι εδώ:");
                            ln = ln.Replace("timeto", "<b>Διαδρομή " + stopArr[i][0] + " λεπτών<b/>");
                            ln = ln.Replace("speedto", "<b>Μ.ταχύτητα:</b>" + stopArr[i][1]);


                            proceed = WriteLnInSw(sw, ln);
                        }
                        sr.Close();
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                ln = "notnull";
                using (StreamReader sr = new StreamReader(p + "\\map5Line3.c.txt"))
                {

                    while (ln != null)
                    {
                        proceed = ReadLnInSr(sr, ref ln);
                        proceed = WriteLnInSw(sw, ln);
                    }
                    sr.Close();
                }

                /////////////////////////////////////////////////////////////////////////////////////////
                proceed = WriteLnInSw(sw, pntString);
                proceed = WriteLnInSw(sw, polypathString);

                /////////////////////////////////////////////////////////////////////////////////////////
                ln = "notnull";
                using (StreamReader sr = new StreamReader(p + "\\map&dirn3.e.txt"))
                {

                    while (ln != null)
                    {
                        proceed = ReadLnInSr(sr, ref ln);

                        if (ln == "            map5.setCenter(new GLatLng(40.5909262318841,22.5343763768116), 10)")
                        {
                            ln = "            map5.setCenter(new GLatLng(" + centerString + "), 12);";
                        }

                        proceed = WriteLnInSw(sw, ln);
                    }
                    sr.Close();
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i <= stopArr.Count - 1; i++)
                {
                    ln = "notnull";
                    using (StreamReader sr = new StreamReader(p + "\\coordinates3.f.txt"))
                    {

                        while (ln != null)
                        {
                            proceed = ReadLnInSr(sr, ref ln);
                            if (ln == null) { break; }
                            ln = ln.Replace("retDir1", "retDir" + i);
                            /*
                               if(i == 0)
                                {
                                           ln = ln.Replace("latitude", "p.y");
                                           ln = ln.Replace("longtitude", "p.x");
                                }
                                if (i == stopArr.Count-1)
                                {
                                    ln = ln.Replace("latitude", "plast.y");
                                    ln = ln.Replace("longtitude", "plast.x");
                                }

                                */
                            ln = ln.Replace("latitude", stopArr[i][3]);
                            ln = ln.Replace("longtitude", stopArr[i][4]);


                            proceed = WriteLnInSw(sw, ln);
                        }
                        sr.Close();
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                string roadApprox = ""; string roadApproxCmnt = "";
                if (snap2Road)
                {
                    roadApprox = "map5.addOverlay(dirn.getPolyline());";
                    roadApproxCmnt = "//roadApproximation ";
                }
                else
                {
                    roadApprox = " map5.addOverlay(new GPolyline(flightPlanCoordinates , " + (char)34 + "#ff0000" + (char)34 + ", 5, 0.7));";
                    roadApproxCmnt = "//roadApproximation2";
                }
                ln = "notnull";
                using (StreamReader sr = new StreamReader(p + "\\after3.g.txt"))
                {
                    while (ln != null)
                    {
                        proceed = ReadLnInSr(sr, ref ln);
                        if (ln == null) { break; }
                        ln = ln.Replace(roadApproxCmnt, roadApprox);
                        proceed = WriteLnInSw(sw, ln);
                    }
                    sr.Close();
                }
                /////////////////////////////////////////////////////////////////////////////////////////

                sw.Close();
                Process.Start(p + "\\RoadMap3.htm");
            }
            catch (System.Exception sex)
            { return false; }

            return true;
        }
    }
}
