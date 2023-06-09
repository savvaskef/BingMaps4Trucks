using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
 using System.IO;
using System.Xml;
using System.Text;

namespace distanceMatrix
{
    public static class Matrix
    {
        public static double[,] getMatrix(List<string> stringcoordinates,string prop)
        {
 

       
            int pageEntries = 6;
            bool modEqZero=false;
            string points=""; int cnt=0;
            foreach (string s in stringcoordinates) {
                points += s.Substring(2)+"|";
                cnt++;
            }
            points = points.Substring(0, points.Length - 1);
            string[] pointslist = points.Split(new Char[] { '|' });
            string pagePoints; List<string> pagePointsList = new List<string>();
            for (int ii = 0; ii < Math.Truncate(Convert.ToDouble(pointslist.Length) / pageEntries) + 1; ii++)
            {
                modEqZero = false;
                pagePoints = "";
                for (int jj = 0; jj < pageEntries; jj++)
                {
                    int jjidx = ii * pageEntries + jj;
                    if (jjidx==pointslist.Length)  modEqZero=true;
                    if (!modEqZero) pagePoints += pointslist[jjidx] + "|";
                
                }
                if (pagePoints!="")
                {
                    pagePoints = pagePoints.Substring(0, pagePoints.Length - 1);
                    pagePointsList.Add(pagePoints);
                }
            }

            string origins;string destinations;
            double[,] tabular = new double[cnt, cnt];
            
            for (int ii=0;ii<pagePointsList.Count;ii++){
                int iibase = ii * pageEntries;
                origins=pagePointsList[ii];
                for (int jj=0;jj<pagePointsList.Count;jj++){
                    int jjbase = jj * pageEntries; destinations = pagePointsList[jj];

                    Uri myUri = new Uri("http://maps.googleapis.com/maps/api/distancematrix/xml?origins="+origins+"&destinations="+destinations+"&mode=driving&language=en-en&sensor=false");
                   //    myUri = new Uri("http://maps.googleapis.com/maps/api/distancematrix/json?origins="+points+"&destinations="+points+"&mode=driving&language=en-en&sensor=false");
                    WebRequest myWebRequest = WebRequest.Create(myUri);
                    // Assign the response object of 'WebRequest' to a 'WebResponse' variable.
                    WebResponse myWebResponse = myWebRequest.GetResponse();
                    StreamReader reader = new StreamReader(myWebResponse.GetResponseStream()) ;
                    string obj=reader.ReadToEnd();
                    myWebResponse.Close();

                    Console.WriteLine(obj);
                    using (XmlReader XMLreader = XmlReader.Create(new StringReader(obj)))
                    {

                        for (int i = 0; i < origins.Split(new Char[] { '|' }).Length; i++)
                        {
                            XMLreader.ReadToFollowing("row");
                            for (int j = 0; j < destinations.Split(new Char[] { '|' }).Length; j++)
                            {
                                XMLreader.ReadToFollowing("element");
                                XMLreader.ReadToFollowing(prop);
                                string a= XMLreader.ReadInnerXml();
                                tabular[i + iibase, j + jjbase] = Convert.ToDouble(a.Substring(12, -12 + a.LastIndexOf("</value>")));
                            }
                         }
          
                    }//endValuesEntry


                }//next jj
            }//next ii
            
            // Create a new request to the above mentioned URL.	
            
            //System.Net.WebClient().
 


          return tabular;

             }//end getmatrix

        public static double  googledist(double x1, double y1, double x2, double y2, string prop)
        {



        
            string origins=x1.ToString().Replace(",",".")+","+y1.ToString().Replace(",",".");

            string destinations = x2.ToString().Replace(",", ".") + "," + y2.ToString().Replace(",", ".");

            double distance=0;
        

                    Uri myUri = new Uri("http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origins + "&destinations=" + destinations + "&mode=driving&language=en-en&sensor=false");
                    //    myUri = new Uri("http://maps.googleapis.com/maps/api/distancematrix/json?origins="+points+"&destinations="+points+"&mode=driving&language=en-en&sensor=false");
                    WebRequest myWebRequest = WebRequest.Create(myUri);
                    // Assign the response object of 'WebRequest' to a 'WebResponse' variable.
                    WebResponse myWebResponse = myWebRequest.GetResponse();
                    StreamReader reader = new StreamReader(myWebResponse.GetResponseStream());
                    string obj = reader.ReadToEnd();
                    myWebResponse.Close();

                    Console.WriteLine(obj);
                    using (XmlReader XMLreader = XmlReader.Create(new StringReader(obj)))
                    {

                        for (int i = 0; i < origins.Split(new Char[] { '|' }).Length; i++)
                        {
                            XMLreader.ReadToFollowing("row");
                            for (int j = 0; j < destinations.Split(new Char[] { '|' }).Length; j++)
                            {
                                XMLreader.ReadToFollowing("element");
                                XMLreader.ReadToFollowing(prop);
                                string a = XMLreader.ReadInnerXml();
                                distance = Convert.ToDouble(a.Substring(12, -12 + a.LastIndexOf("</value>")));
                            }
                        }

                    }//endValuesEntry





                    return distance;

        }//end getmatrix
  
    
    
    }
}
