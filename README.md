# BingMaps4Trucks
The project utilizes bing maps, SQL Compact edition(needs installation), K-means clustering 
using line-distances and exhaustive search.
in order to cluster stops, calculate routes and present them on a map.
<br>
The form that opens by doubleclicking <br>
.\Truck routes\Truck routes\bin\Debug\Truck routes.exe
<br>
has three options.<br>
1)Creating starts and ends of routes and stops
<br>
2)Solving for the best exaustive search
<br>
3)Presenting the routes to be taken
<br>

Since the software is under upgrade. ie the bing *directions service* that  has been made obsolete (resulting in linear routes rather than with waypoints) and needs to be re-introduced.
but also truck routes have been augmented with constrains (ie a truck carrying flamable content may need to avoid some roads) and traffic is expected
<br>
So before these are appeneded, I
suggest you go directly for the report (that is option 3)
and if you seek to test it rename one of the few databases(11\15 stops)
at the folder to Routing.sdf
<br>

However, peeking the interface of option 1, you may grasp meaningfull parameters
so as to comprehend the business logic .The model I used to deal with it is
a shadow of the next version plan.
<br>
If the stops are few... no problem.
Provided they are more than few, newer algorithms may need to be recruited.
