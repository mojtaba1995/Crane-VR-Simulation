# Crane VR Simulation
Many industrial projects involve the use of prefabricated modules built offsite, and installation on-site using mobile cranes. Due to their costly operation and safety concerns, utilization of such heavy lift mobile cranes requires a precise lift planning. Traditional lift planning methods on congested industrial job sites are ineffective, time-consuming and non-precise in many cases, whereas computer-based simulation can be a better tool. By providing a VR environment in which the crane crew can experience lifting process. Providing such VR simulation not only facilitates planning for lifts, but also provides a training environment to enhance safe climate prior to the actual lift. The developed VR model is implemented successfully on an actual construction site of a petrochemical plant on a modular basis in which heavy lift mobile cranes are employed. 

## Getting Started

To compile the program you should run the [Source.cpp](src/Source.cpp) code and it is possible to modify the code using the following guidelines.


### Descriptions 

In order to simplify the usage of this code, a mathematical question was proposed. In this question "Z" is fitness function and X_0 and X_1 are the input variables.


<br>
<b>
Fitness function:
</b>
<br>
<br>
<div align="left">
<img src="https://github.com/mojtaba1995/Genetic-Algorithm/blob/master/images/fitness.png" width="160">
</div>
<br>
<b>Constraints:</b>
<br>
<br>
<div align="left">
<img src="https://github.com/mojtaba1995/Genetic-Algorithm/blob/master/images/C1.png" width="180">
</div>
<br>
<div align="left">
<img src="https://github.com/mojtaba1995/Genetic-Algorithm/blob/master/images/C2.png" width="180">
</div>
<br>
<div align="left">
<img src="https://github.com/mojtaba1995/Genetic-Algorithm/blob/master/images/C3.png" width="80">
</div>
<br>
<div align="left">
<img src="https://github.com/mojtaba1995/Genetic-Algorithm/blob/master/images/C4.png" width="60">
</div>
<br>
<div align="left">
<img src="https://github.com/mojtaba1995/Genetic-Algorithm/blob/master/images/C5.png" width="60">
</div>
<br>



---------------------------------------------------------------------------------

Using defines that comes below you can control genetic algorithm.
* The variable genenumber is the number of inputs for your function which in this question is equal to two. 
* The variable choromozomenumber is the number of chromosomes in each generation. 
* The variable generationnumber is the number of total generations. 
* The variable percent and tops are related to tournament section in GA. 
* The variable mutationnumber is the number of total mutations.
* The variable mutationrange is the range of mutations.
<br>

<pre><code>
define genenumber 2

define choromozomenumber 22

define generationnumber 200

define percent 5

define tops 2

define mutationnumber 2

define mutationrange 10
</code></pre>
---------------------------------------------------------------------------------
In the code below, the "func" is the fitness function that was mentioned earlier.
<b>input.getgene(0).getvalue()</b> means the first input. It is easy to change this function based on the needs.

<pre><code>
float func(choromozome input) {

	float result;
	
	int i;
	
	result = 0;
	
	result = <b>input.getgene(0).getvalue() + 3 * input.getgene(1).getvalue();</b>
	
  	return result;
  	
}
</code></pre>
---------------------------------------------------------------------------------
By changing these lines it is possible change constraints. The 100000 is the penalty value and can be changed from problem to problem.

<pre><code>
if (<b>chorome[i].getgene(0).getvalue() > 9</b>) {
	z[i][0] = z[i][0] - 100000;
}
			
if (<b>2 * chorome[i].getgene(0).getvalue() + chorome[i].getgene(1).getvalue() > 22</b>) {
	z[i][0] = z[i][0] - 100000;
}
			
if (<b>chorome[i].getgene(0).getvalue() + 4 * chorome[i].getgene(1).getvalue() > 32</b>) {
	z[i][0] = z[i][0] - 100000;
}
			
if (<b>chorome[i].getgene(0).getvalue() <= 0</b>) {
	z[i][0] = z[i][0] - 100000;
}
			
if (<b>chorome[i].getgene(1).getvalue() <= 0</b>) {
	z[i][0] = z[i][0] - 100000;
}
</code></pre>	


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details


