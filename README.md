# BAE CMS-Backend

The Backend solution to our CMS project

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

You will require the following tools and software to use the system:

* Access to [PlantUML web server](https://plantuml.com/starting) to view and update UML diagrams;
* A VM on the cloud running Ubuntu 18.04. This VM needs to allow ports 5000 and 8080, as well as allow http and https traffic.



### Installing

SSH into the VM, and then install the following three items:


**1) DotNet**

First, you need to install [dotnet](https://dotnet.microsoft.com/). To do this, run the following:

```
wget https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```


This will register the Microsoft Key, and install the required dependencies.
Then, install .NET core SDK using the following:

```
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-3.1
```


Next, install the ASP.NET Core runtime using the following:

```
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install aspnetcore-runtime-3.1
```


Lastly, install the .NET Core runtime using the following:

```
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-runtime-3.1
```


To confirm that everything has installed, run the following command:

```
dotnet run
```


An error message should appear saying that there is no project to run. We will host the project later; we are just checking that Ubuntu knows about Dotnet!




**2) Jenkins**

Then, you will need to install [Jenkins](https://jenkins.io/). The first step to this is to install Java 8. This is done by typing in

```
sudo apt-get install openjdk-8-jdk
```


Then, you can install Jenkins proper. To do this, type out the following:

```
wget -q -O - https://pkg.jenkins.io/debian/jenkins-ci.org.key | sudo apt-key add -
```

```
sudo sh -c 'echo deb http://pkg.jenkins.io/debian-stable binary/ > /etc/apt/sources.list.d/jenkins.list'
```

```
sudo apt-get update
```

```
sudo apt-get install jenkins
```


Jenkins will now be installed on the VM. To check that it is working, type the command

```
systemctl status jenkins.service
```


In order to use Jenkins, you must go to the IP of your VM, and add the suffix ":8080". This will take you to the Jenkins landing page.
It will ask for the initial admin password. To find this, run the following:

```
cat /var/lib/jenkins/secrets/initialAdminPassword
```


Copy the password that appears and paste it into Jenkins. Then, install the recommended plugins, and set up a user account.



**3) Docker**

Thirdly, you will need to install [Docker](https://www.docker.com/) on the VM. To do this, first run

```
curl https://get.docker.com | sudo bash
```


Then, clone our git repository into the VM, by using the command

```
git clone https://github.com/christophperrins/CMS-backend.git
```


Then, run the command 

```
docker build -t backend:latest .
```


This will build the project on the local VM, ready for hosting. To do that, run the following:

```
docker run -d -p 80:80 --name backendContainer backend
```


The backend server should now be up and running. To confirm, run the followingcommand:

```
curl localhost/api/weather?lat=55&lon=-2
```

**4) MySQL Database**
Finally we need a database to interate with.

First create a cloud based database and create the user profile you want the CMS to use. (Make sute this user has permission to Read and Write to tables.)

Next connect to your database through MySQL workbench to name your database:
```
CREATE DATABASE	DATABASE_NAME;
```
Then create the following tables:
```
CREATE TABLE DATABASE_NAME.PlottableEntities(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Identifier VARCHAR(100) UNIQUE NOT NULL,
    Classification VARCHAR(50),
    Subclassification VARCHAR(50),
    Longitude FLOAT,
    Latitude FLOAT,
    Elevation INT,
    LastUpdate INT,
    Armed BOOL,
    Hostility VARCHAR(10)
);

CREATE TABLE DATABASE_NAME.MoveableEntities(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PlottableEntityId INT UNIQUE NOT NULL,
    Bearing FLOAT,
    Speed FLOAT,
    Heading FLOAT,
    FOREIGN KEY(PlottableEntityId) REFERENCES database_name.PlottableEntities(Id)
);

CREATE TABLE DATABASE_NAME.Platform(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Identifier VARCHAR(100) UNIQUE NOT NULL,
    Longitude FLOAT,
    Latitude FLOAT,
    Elevation INT,
    Bearing FLOAT,
    Speed FLOAT
);
```
Finally go into csharp/src/appsettings.json and update the "MySql" key value to match your database:
```
"MySql": "server=YOUR_DATBASE_IP_ADDRESS;database=DATABASE_NAME;uid=USER_PROFILE_NAME;pwd=USER_PASSWORD;"
```

## Running the tests


In order to run tests quickly and easily, navigate to the location of the solution file (csharp.sln), which by default is in the /csharp folder.

Open a command prompt at this location, and run the command:

```
dotnet test /p:CollectCoverage=True
```


This will automatically run all tests, and inform you of the test coverage and pass rate.


## Deployment

In order to automate the process, Jenkins needs to be configured to build the software automatically. To achieve this, on your Jenkins page create a new freestyle project. In your Source Code Management, select a Git project, and paste in the following:

```
git@github.com:christophperrins/CMS-backend.git
```

Next, in Build Triggers, you can set how frequently you want the project to build using the Poll SCM option. We reccomend typing in

```
* * * * *
```


Then, in Build Environment, ensure that the Tickbox *Delete workspace before build starts* is ticked.
Lastly, add a build step and execute a shell. Paste in the following commands:

```
cd csharp/
docker build -t backend:latest .
docker run -d -p 9000:80 --name backendContainer backend
```


Then, if you click apply and save, the build should commence.
After the first build, it is imperative that you click *Configure* in the jobs list, and replace the build shell options with the following:

```
cd csharp/
docker stop backendContainer 
docker rm backendContainer
docker build -t backend:latest .
docker run -d -p 9000:80 --name backendContainer backend
```


This will clean out any previous docker containers, and ensure you have a clean install.


After cloning the Git repo go into your command line and install NewtonsoftJson.
1. Open the command line in the project folder /src
2. Use command dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 3.0.0



## Built With

*  Microsoft Visual Studio

## Authors

* **Chris Perrins** - *Senior Dev* - [christophperrins](https://github.com/christophperrins)
* **Tadas Vaidotas** - *Senior Dev*


* **Tyler Eddy** - *Backend Team Leader* - [tylerjohneddy](https://github.com/tylerjohneddy)
* **Haziq Mahmood** - *Backend Junior Dev* - [HaziqMahmood](https://github.com/HaziqMahmood)
* **Charlie Reddin** - *Backend Junior Dev* - [ccgr1121](https://github.com/ccgr1121)
* **Tapiwa Tiyemba** - *Backend Junior Dev* - [ttiyemba](https://github.com/ttiyemba)
* **Josh Young-Cannon** - *Backend Junior Dev* - [JoshYoung-Cannon](https://github.com/JoshYoung-Cannon)

## License

This project is licensed under the MIT license - see the [LICENSE.md](LICENSE.md) file for details 



## Acknowledgments

* Chris Perrins, Rhys Thompson and Tadas Vaidotas for assisting and training us;
* All QA trainers for their invaluable tuition and assistance;
