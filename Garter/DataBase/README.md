# SQL Test Assignment

Attached is a mysqldump of a database to be used during the test.

Below are the questions for this test. Please enter a full, complete, working SQL statement under each question. We do not want the answer to the question. We want the SQL command to derive the answer. We will copy/paste these commands to test the validity of the answer.

**Example:**

_Q. Select all users_

- Please return at least first_name and last_name

SELECT first_name, last_name FROM users;


------

**— Test Starts Here —**

1. Select users whose id is either 3,2 or 4
- Please return at least: all user fields
-----------------------------------------------------------------------------------
select * from users where Id in (3,2,4)
------------------------------------------------------------------------------------

2. Count how many basic and premium listings each active user has
- Please return at least: first_name, last_name, basic, premium
---------------------------------------------------------------------------------
select count(list.status), users.first_name, users.last_name from users inner join listing on listing.user_id = users.id group by listing.status 
----------------------------------------------------------------------------------

3. Show the same count as before but only if they have at least ONE premium listing
- Please return at least: first_name, last_name, basic, premium
--------------------------------------------------------------------------------------- 
select count(list.status), users.first_name, users.last_name from users inner join listing on listing.user_id = users.id group by listing.status having(Count(listing.status)) = 1
----------------------------------------------------------------------------------

4. How much revenue has each active vendor made in 2013
- Please return at least: first_name, last_name, currency, revenue
-------------------------------------------------------------------------------------------------------
select first_name, last_name, currency, price as revenue from users u inner join listing l on inner join l.user_id on u.id inner join clicks c on c.listing_id on l.Id where YEAR(c.Created) = 2013
-------------------------------------------------------------------------------------------------------


5. Insert a new click for listing id 3, at $4.00
- Find out the id of this new click. Please return at least: id
--------------------------------------------------------------------------------------------------
INSERT INTO `clicks` VALUES (33,3,4.00,'USD',GetDate())


select top 1 id from Clicks where listing_id = 3 and price = 4.00 order by created desc
---------------------------------------------------------------------------------------------------


6. Show listings that have not received a click in 2013
- Please return at least: listing_name

--------------------------------------------------------------------------------------------
select name as listing_name from listing where id not in (select listing_id from clicks)
---------------------------------------------------------------------------------------------

7. For each year show number of listings clicked and number of vendors who owned these listings
- Please return at least: date, total_listings_clicked, total_vendors_affected
--------------------------------------------------------------------------------------------
select  Count(name)  from listing where id in (select listing_id from clicks) group by name
-----------------------------------------------------------------------------------------------------------

8. Return a comma separated string of listing names for all active vendors
- Please return at least: first_name, last_name, listing_names
----------------------------------------------------------------------------------------------------------------
select first_Name, last_name, STRING_AGG(name,',') from users inner join listing on listing.user_id = users.id group by name
--------------------------------------------------------------------------------------------------------------------------------