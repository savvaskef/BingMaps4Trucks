using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using program;
namespace Exhaust
{
    public class ExhaustiveSearch
    {
        public static List<int> order = new List<int>();
        public static double distance;
        //     
        public static void Combinations(int cnt, ref List<int> pathCompleted , ref List<List<int>> Combs)
        {
            int IthData;int node;int cnter;bool skipint;
            if (pathCompleted .Count ==1+ cnt)
            {
                List<int> mylist = new List<int>();
                foreach (int j in pathCompleted) mylist.Add(j);
                Combs.Add(mylist);
                return;
            }
            for (int i = 0; i <= cnt; i++)
            {
                 IthData=i+1;
                 skipint=false;
                 foreach(int j in pathCompleted){
                     if (j == IthData)
                     {
                         skipint = true;
                         break;
                     }
                 }           
                if (!skipint){
                pathCompleted.Add(IthData);
                    Combinations(cnt, ref pathCompleted,  ref Combs);
                    pathCompleted.Remove(IthData);
                }

            }
        }//end combinations

        public static void CombinationsRemainingtail(int cnt, int incl,ref List<int> pathCompleted, ref List<List<int>> Combs)
        {
            int IthData; int node; int cnter; bool skipint;
            if (pathCompleted.Count == cnt)
            {
                List<int> mylist = new List<int>();
                foreach (int j in pathCompleted) mylist.Add(j);
                Combs.Add(mylist);
                return;
            }
            for (int i = 0; i < incl; i++)
            {
                IthData = i + 1;
                skipint = false;
                foreach (int j in pathCompleted)
                {
                    if (j == IthData)
                    {
                        skipint = true;
                        break;
                    }
                }
                if (!skipint)
                {
                    pathCompleted.Add(IthData);
                    CombinationsRemainingtail(cnt,incl, ref pathCompleted, ref Combs);
                    pathCompleted.Remove(IthData);
                }

            }
        }//end combinations


        public static void GetPath(int cnt, List<List<int>> combs, double[,] distances)
        {
            foreach (List<int> combo in combs)
            {
                combo.Insert(0, 0);
                combo.Insert(cnt, combo.Count);
            }   


            distance = Math.Pow(10, 10);
            foreach (List<int> combo in combs) {
                
                if (GetTotalDist(combo, distances)<distance){
                    distance=GetTotalDist(combo, distances);
                    order = combo;
                }
        }

        }//end getPath


        private static double GetTotalDist(List<int> order, double[,] distances)
        {
            double distance = 0;

            for (int i = 0; i < order.Count - 1; i++)
            {
                distance += distances[order[i], order[i + 1]];
            }

            //if (order.Count > 0)
            //{
            //    distance += distances[order[order.Count - 1], 0];
            //}

            return distance;
        }
    
    } 

}
