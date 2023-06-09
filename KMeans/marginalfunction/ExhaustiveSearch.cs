using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using marginalfunc;
namespace Exhaust
{
    public class ExhaustiveSearch
    {
        public static List<int> order = new List<int>();
        public static double distance;
        //     

          static List<int> cloneintlist(List<int> source)
        {
            var dummylist = new List<int>();
             
            foreach (int c in source)
            {
                 
                dummylist.Add(c);
            }
            return dummylist;
        }

          public static void nthLengthCombo(List<int> remaining, List<int> constructed, int k, int maxk, ref List<List<int>> output)
          {if (maxk<k)return;
          int startcount = remaining.Count-1;
          int didx = 0;
          while (didx <= startcount) {
              int d=remaining[didx];
              List<int> oldremainiing = cloneintlist(remaining);
              constructed.Add(d);
              remaining.Remove(d);
              List<int> C = cloneintlist(constructed);
              output.Add(C);
              nthLengthCombo(remaining,constructed,k+1,maxk,ref output);
              constructed.Remove(d);
              didx++;
            remaining=cloneintlist(oldremainiing);

          
          }
        
        
        }

        public static void appearOnce(List<List<int>>input,ref List<List<int>>output){
            List<List<int>> accumulated = new List<List<int>>();
            accumulated.Add(input[0]);
            bool hasit;
            foreach (List<int> cl in  input)
            {
                hasit = false;
                foreach (List<int> vl in accumulated)
                {
                    if (HasAllVals(cl,vl)) {
                        hasit=true;
                        break;
                    }

                }
                if (!hasit) accumulated.Add(cl);
            }

            output = accumulated;
        }

           public static bool HasAllVals(List<int> cl, List<int> vl)
        {
            int cumval = 0; 
            List<int> c2 = new List<int>();
            foreach (int c in cl) { c2.Add(1); }

            List<int> v2 = new List<int>();
            foreach (int v in vl) { v2.Add (1);}


               for (int i = 0; i < cl.Count; i++)
            {
                for (int j = 0; j< vl.Count; j++)
                {
                    if (cl[i] == vl[j]) {
                        c2[i] = 0;
                        v2[j] = 0;
                    }
                }
            }
        for(int i=0;i<c2.Count;i++) {cumval+=c2[i];}
        for (int i = 0; i < v2.Count; i++) { cumval += v2[i]; }

        return cumval == 0;
           }
           public static void Combinations(int cnt, ref List<int> pathCompleted, ref List<List<int>> Combs)
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
