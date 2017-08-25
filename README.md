# Band and Venue Tracker

#### C# Advanced Database Project, 8/25/2017

#### By _Ben Budinger_

## Description

_This site uses a SQL database to track bands that have played at venues using a join table_
* Bands can be added to the site
* Venues can be added to the site
* Bands can be added to venues where they have played
* Venues can be removed
* Venues can be edited/updated
* Venues can add bands that have played there
* All bands that have played at a venue can be shown
* All venues that a band has played at can be shown

### Technical Specifications

|Behavior|Expected|Actual|
|-|-|-|
|Home page view|List of venues|All venues from DB|
|Click on venue|Venue name and band list|Venue name and band list from DB|
|Click on Add Band button|Add band to specific venue|Add band/venue id to join table|
|Click on band|Band name and venue list|Band name and venue list from DB|
|Click on Add Venue button|Add venue to specific band|Add band/venue id to join table|
|Click on Delete Venue button|Removes venue from venue list page|Deletes venue id from venues and band_venues tables|
|Click on Edit Venue button|Updates venue information|Updates venue name in DB|


## Setup

* This website will be hosted on GitHub
* https://github.com/budingerbc/BandTracker

##### Database Creation/Setup - Commands
1. CREATE DATABASE band_tracker;
2. USE band_tracker;
3. CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR (255));
4. CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR (255));
5. CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id int, venue_id int);

* The DB band_tracker_test is used for MST Unit testing

## Technologies Used

* HTML
* CSS
* Bootstrap
* C#
* Razor

### License

* Copyright (c) 2017 Ben Budinger
* This software is licensed under the MIT license.
