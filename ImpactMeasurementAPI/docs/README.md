# Impact Measurement in Urbansports Back end

## Contents

### Components
### Docker commands
### Migrations
### Algorithm
### Endpoints
<br>
<br>

## Components

<p>As seen in the <i>docker-compose.yml</i> the docker comtainer consists of three different components</p>

<p><b>1. </b> impact_measurement_api, this container consists of the code in this repository</p>
<p><b>2. </b> db, this container is the MYSQL database, it is built off of migrations made in the <i>impact_measurement_api</i></p>
<p><b>3. </b> pma, this container runs php my admin, this provides a gui for interacting with the database</p>
<p><b>4. Currently Removed</b> metabase, this container is a Metabase (open source PowerBI) instance and used for quickly visualizing data stored in the <i>db container</i></p>
<br>

## Docker commands
<p> When you open a terminal in the project folder the following commands can be used</p>
<p><i>docker-compose up --build</i></p>
<p><i>docker-compose up -d --build
</i> this command builds and runs the docker container silently in the background</p>
<p><i>docker-compose build  </i> this command creates a new build of the docker container <b>Attention</b> you will need to use <i>docker-compose up </i> to actually use the container</p>
<p><i>docker-compose down  </i> this command stops the docker container</p>
<p><i>docker system prune --volumes  </i> use this command after the down command to remove all saved docker data <b>Warning this removes all data only use when you know what youre doing</b> This command makes it possible to create a clean docker container</p>
<br>

## One-Time setup after setting up docker environment

<p> Go to localhost:8081 in your browser.</p>
<p>Click on the database "test"</p?
<p>Go to the table "Athletes" and click insert</p>
<p> Fill the field id with the number 1 and the rest of the fields to your own desire </p>
<p> Click "Go" </p>
<p> On the new page you are on click "Go" again </p>
<p> The application should be ready to use now </p>

## Migrations -may be OSX biased-
<p> Migrations are what build the database during the build phase of the docker instance.
When you open a terminal in the project folder the following command can be used</p>
<p><i>dotnet ef migrations add migration</i> use this command to create a new migration or edit existing ones. <b>Attention</b> When changing an existing table either manually delete the table first using MYSQL terminal or PHPMyAdmin or when making multipl changes to existing tables it is easier to prune the docker volume.</p>
<br>

## Algorithm
<p>For information about the algorithm used to calculate the impact during training sessions please look at the document <i>Impact Measurement Algorithm</i></p>
<br>

## Swagger
<
